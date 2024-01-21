using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Application.Models.CategoriaModel;
using Application.Models.ClienteModel;
using Application.Models.PedidoModel;
using Application.Models.ProdutoModel;
using Application.UseCases;
using Application.UseCases.CategoriaUseCase;
using Application.UseCases.ClienteUseCase;
using Application.UseCases.PedidoUseCase;
using Application.UseCases.ProdutoUseCase;
using Domain.Gateways;
using Infrastructure.DataProviders;
using Infrastructure.DataProviders.Repositories;
using Domain.Gateways.External;
using Infrastructure.DataProviders.Repositories.External;
using Application.Models.PagamentoModel;
using Application.UseCases.PagamentoUseCase;
using System;
using Application.Services;
using Domain.Services;

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
            services.AddTransient<IUseCaseIEnumerableAsync<IEnumerable<ClienteResponse>>, GetAllClienteUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<ClienteRequest, ClienteResponse>, GetClienteByCPFUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<ClientePostRequest>, PostClienteUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<ClienteDeleteRequest>, DeleteClienteUseCaseAsync>();
            services.AddTransient<IUseCaseIEnumerableAsync<IEnumerable<CategoriaResponse>>, GetAllCategoriaUseCaseAsync>();
            services.AddTransient<IUseCaseIEnumerableAsync<IEnumerable<ProdutoResponse>>, GetAllProdutoUseCaseAsync>();
            services.AddTransient<IUseCaseIEnumerableAsync<ProdutoRequest, IEnumerable<ProdutoResponse>>, GetProdutoByCategoriaIdUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<ProdutoPostRequest>, PostProdutoUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<ProdutoPutRequest>, PutProdutoUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<ProdutoDeleteRequest>, DeleteProdutoUseCaseAsync>();

            services.AddTransient<IUseCaseIEnumerableAsync<IEnumerable<PedidoDetalhadoResponse>>, GetAllPedidoUseCaseAsync>();
            services.AddTransient<IUseCaseIEnumerableAsync<PedidoRequest, PedidoDetalhadoPorSenhaResponse>, GetPedidoBySenhaUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<PedidoPostRequest, Tuple<int, Guid>>, PostPedidoUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<PedidoDeleteRequest>, DeletePedidoUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<PedidoPutRequest>, PutPedidoUseCaseAsync>();

            services.AddTransient<IUseCaseIEnumerableAsync<IEnumerable<HistoricoClienteResponse>>, GetHistoricoClienteUseCaseAsync>();

            services.AddTransient<IUseCaseAsync<PagamentoPutRequest>, PutPagamentoUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<PagamentoGetRequest, Tuple<string, Guid>>, GetPagamentoUseCaseAsync>();

            services.AddTransient<ICognitoGateway, CognitoService>();
            services.AddTransient<IIdentityService, IdentityService>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IClienteGateway, ClienteRepository>();
            services.AddTransient<ICategoriaGateway, CategoriaRepository>();
            services.AddTransient<IProdutoGateway, ProdutoRepository>();
            services.AddTransient<IPedidoGateway, PedidoRepository>();
            services.AddTransient<IPagamentoGateway, PagamentoRepository>();
        }

        private static void AddOthers(IServiceCollection services)
        {
            services.AddTransient<DBContext>();
        }
    }
}
