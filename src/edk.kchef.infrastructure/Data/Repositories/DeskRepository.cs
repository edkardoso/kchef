using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using edk.Kchef.Domain.Contracts.Repositories;
using edk.Kchef.Domain.Ordes;
using edk.Tools.Common;

namespace edk.Kchef.Infrastructure.Data.Repositories;




public class DeskRepository : IDeskRepository
{
    private readonly IGenericRepository<Desk> _repository;

    public DeskRepository(IGenericRepository<Desk> repository)
    {
        _repository = repository;
    }

    public Task<Option<Desk>> FirstOrDefaultAsync() => _repository.FirstOrDefaultAsync();
    public Task<Option<Desk>> FirstOrDefaultAsync(Expression<Func<Desk, bool>> filter) => _repository.FirstOrDefaultAsync();
    public Task<Option<Desk>> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);
    public Task<Option<Desk>> SingleByCodeAsync(string code) => SingleAsync(d => d.InternalCode.Equals(code));
    public Task<Option<Desk>> SingleAsync(Expression<Func<Desk, bool>> filter) => _repository.SingleAsync(filter);
    public Task<Option<List<Desk>>> SearchAsync(Expression<Func<Desk, bool>> filter) => _repository.SearchAsync(filter);
    public Task UpdateAsync(Desk entity) => _repository.UpdateAsync(entity);
}
