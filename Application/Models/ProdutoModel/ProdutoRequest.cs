using Microsoft.AspNetCore.Mvc;
using System;

namespace Application.Models.ProdutoModel
{
    public class ProdutoRequest
    {
        public ProdutoRequest()
        {
            CategoriaId = Guid.Empty;
        }

        [FromRoute]
        public Guid CategoriaId { get; set; }
    }
}
