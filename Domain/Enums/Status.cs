using System.ComponentModel;

namespace Domain.Enums
{
    public enum Status : short
    {
        [Description("Recebido")]
        Pendente,

        [Description("Em preparação")]
        EmPreparo,

        [Description("Pronto")]
        Pronto,

        [Description("Entregue")]
        Entregue,
    }
}
