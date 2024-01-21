using Microsoft.AspNetCore.Mvc;
using System;

namespace Application.Models.ClienteModel
{
    public class ClienteDeleteRequest
    {
        public ClienteDeleteRequest()
        {
            Id = Guid.Empty;
        }

        [FromRoute]
        public Guid Id { get; set; }
    }
}
