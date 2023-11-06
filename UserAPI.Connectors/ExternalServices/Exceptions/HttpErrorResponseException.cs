namespace UserAPI.Connectors.ExternalServices.Exceptions
{
    public class HttpErrorResponseException : Exception
    {
        public HttpResponseMessage Response { get; }

        public HttpErrorResponseException(HttpResponseMessage response) : base($"Erro HTTP: {response.StatusCode} - {response.ReasonPhrase}")
        {
            Response = response;
        }
    }

}
