using TokonyadiaRestAPI.Entities;

namespace TokonyadiaRestAPI.Services;

public interface IRoleService
{
    Task<Role> SaveOrGet(Erole role);
}