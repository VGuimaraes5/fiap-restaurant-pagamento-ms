using Microsoft.Extensions.DependencyInjection;
using Application.UseCases;
using Infrastructure.DataProviders;
using Domain.Gateways.External;
using Infrastructure.DataProviders.Repositories.External;
using Application.Models.PagamentoModel;
using Application.UseCases.PagamentoUseCase;
using Infrastructure.Bus;
using Domain.Bus;

namespace Infrastructure.Extensions
{
    public static class RegisterServiceExtensions
    {
        public static void RegisterService(this IServiceCollection services)
        {
            AddUseCase(services);
            AddRepositories(services);
            AddOthers(services);
        }
        private static void AddUseCase(IServiceCollection services)
        {
            services.AddTransient<IUseCaseAsync<PagamentoPutRequest>, PutPagamentoUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<PagamentoPostRequest>, PostPagamentoUseCaseAsync>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IPagamentoGateway, PagamentoRepository>();
            services.AddTransient<IPagamentoBus, PagamentoBus>();
        }

        private static void AddOthers(IServiceCollection services)
        {
            services.AddTransient<DBContext>();
        }
    }
}
