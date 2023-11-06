using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Net;
using UserAPI.Connectors.ExternalServices.Models;
using UserAPI.Connectors.ExternalServices.Exceptions;

namespace UserAPI.Connectors.ExternalServices
{
    public class AuthAPI : IAuthAPI
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IMemoryCache _memoryCache;

        public AuthAPI(HttpClient httpClient, IConfiguration config, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _config = config;
            _memoryCache = memoryCache;
        }

        private async Task<string> GetAdminToken()
        {
            var clientId = _config["KeyCloak:resource"];
            var clientSecret = _config["KeyCloak:credentials:secret"];
            var realm = _config["KeyCloak:realm"];
            var authUri = _config["KeyCloak:auth-server-url"];

            var tokenRequest = new ClientCredentialsTokenRequest
            {
                Address = $"{authUri}/realms/{realm}/protocol/openid-connect/token",
                ClientId = clientId,
                ClientSecret = clientSecret,
                Scope = "openid",
            };

            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(tokenRequest);

            if (tokenResponse.IsError)
            {
                throw new Exception($"Erro ao obter o token de admin: {tokenResponse.Error}");
            }

            var token = tokenResponse.AccessToken;

            // Armazena o token em cache com um tempo de vida (por exemplo, 30 minutos)
            _memoryCache.Set("AdminToken", token, TimeSpan.FromMinutes(30));

            return token;
        }

        public async Task<AuthUserWithRoles> GetAuthUserById(string student_id)
        {
            int renewalAttempts = 0;
            int maxRenewalAttempts = 3;
            AuthUserWithRoles? authUser = null;

            while (renewalAttempts < maxRenewalAttempts)
            {
                try
                {
                    authUser = await TryGetAuthUserById(student_id);
                    break;
                }
                catch (HttpErrorResponseException ex)
                {
                    HttpResponseMessage exceptionResponse = ex.Response;
                    HttpStatusCode statusCode = exceptionResponse.StatusCode;

                    if (statusCode == HttpStatusCode.Unauthorized)
                    {
                        await GetAdminToken();
                        renewalAttempts++;
                    }
                    else
                    {
                        throw new HttpErrorResponseException(ex.Response);
                    }
                }
            }

            if (renewalAttempts >= maxRenewalAttempts)
            {
                throw new Exception("Máximo de tentativas de obtenção do token do client admin");
            }

            return authUser;
        }

        private async Task<AuthUserWithRoles> TryGetAuthUserById(string userId)
        {
            var token = _memoryCache.Get<string>("AdminToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var realm = _config["KeyCloak:realm"];
            var authUri = _config["KeyCloak:auth-server-url"];

            var userEndpoint = $"{authUri}admin/realms/{realm}/users/{userId}";
            var userRolesEndpoint = $"{authUri}admin/realms/{realm}/users/{userId}/role-mappings/realm";

            var responseUser = await _httpClient.GetAsync(userEndpoint);
            var responseUserRoles = await _httpClient.GetAsync(userRolesEndpoint);


            if (responseUser.IsSuccessStatusCode && responseUserRoles.IsSuccessStatusCode)
            {
                var userJson = await responseUser.Content.ReadAsStringAsync();
                var rolesJson = await responseUserRoles.Content.ReadAsStringAsync();

                var rolesData = JsonConvert.DeserializeObject<List<RoleMappingResponse>>(rolesJson);
                var userData = JsonConvert.DeserializeObject<AuthUserResponse>(userJson);

                var filteredRoles = rolesData.Where(role => RoleMappingResponse.RoleNamesToInclude.Contains(role.Name)).ToList();

                return new AuthUserWithRoles
                {
                    FirstName = userData.FirstName,
                    Email = userData.Email,
                    FamilyName = userData.LastName,
                    Role = filteredRoles
                };
            }
            else
            {
                throw new HttpErrorResponseException(responseUser);
            }
        }
    }



    public interface IAuthAPI
    {
        Task<AuthUserWithRoles> GetAuthUserById(string userId);
    }
}
