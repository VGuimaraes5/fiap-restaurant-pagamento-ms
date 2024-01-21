using Microsoft.AspNetCore.Mvc;

namespace Application.Models.ClienteModel
{
    public class ClienteRequest
    {
        public ClienteRequest()
        {
            Cpf = string.Empty;
        }

        [FromRoute]
        public string Cpf { get; set; }
    }
}
