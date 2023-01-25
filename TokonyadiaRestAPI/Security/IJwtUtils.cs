using TokonyadiaRestAPI.Entities;

namespace TokonyadiaRestAPI.Security;

public interface IJwtUtils
{
    string GenerateToken(UserCredential credential);
}