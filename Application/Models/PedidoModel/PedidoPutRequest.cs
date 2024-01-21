using System;

namespace Application.Models.PedidoModel
{
    public class PedidoPutRequest
    {
        public Guid Id { get; set; }
        public short Status { get; set; }
    }
}