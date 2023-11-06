using Microsoft.AspNetCore.Authorization;


namespace Business.Authorization
{
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        public static string[] Schemas = new string[]
            {
                //InternalAuthenticationSchema.SchemeName,
                AuthorizationBearerDefaults.User,
            };

        private static string schemas = string.Join(',', Schemas);


        public AuthorizationAttribute() : base()
        {
            AuthenticationSchemes = schemas;
        }
    }
}
