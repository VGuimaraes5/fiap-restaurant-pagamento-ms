using Domain.Entities;
using Domain.Gateways;
using System;

namespace Infrastructure.DataProviders.Repositories
{
    public class CategoriaRepository : RepositoryBase<Categoria, Guid>, ICategoriaGateway
    {
        public CategoriaRepository(DBContext dbContext) : base(dbContext) { }
    }
}
