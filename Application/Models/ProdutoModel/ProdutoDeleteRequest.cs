using Microsoft.AspNetCore.Mvc;
using System;

namespace Application.Models.ProdutoModel
{
    public class ProdutoDeleteRequest
    {
        public ProdutoDeleteRequest()
        {
            Id = Guid.Empty;
        }

        [FromRoute]
        public Guid Id { get; set; }
    }
}
