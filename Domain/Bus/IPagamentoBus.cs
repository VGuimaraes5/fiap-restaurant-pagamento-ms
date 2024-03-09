using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Bus
{
    public interface IPagamentoBus
    {
        Task SendPaymentSuccessAsync(PagamentoStatusModel model);
        Task SendPaymentErrorAsync(PagamentoStatusModel model);
    }
}