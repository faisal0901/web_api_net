using TokonyadiaRestAPI.Entities;
using TokonyadiaRestAPI.Exception;
using TokonyadiaRestAPI.Exceptions;
using TokonyadiaRestAPI.Repositories;

namespace TokonyadiaRestAPI.Services;

public class RoleService:IRoleService
{
    private readonly IRepository<Role> _repository;
    private readonly IPersistence _persistence;

    public RoleService(IRepository<Role> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }
    public async Task<Role> SaveOrGet(Erole role)
    {
        try
        {
            var roleFind = await _repository.Find((p => p.Erole.Equals(role)));
            if (roleFind is null) return roleFind;
            var saveRole = await _repository.Save(new Role{Erole = role});
            await _persistence.SaveChangesAsync();
            return saveRole;
        }
        catch (ArgumentNullException e)
        {
            throw new NotFoundException("role not found");
        }
    }
}