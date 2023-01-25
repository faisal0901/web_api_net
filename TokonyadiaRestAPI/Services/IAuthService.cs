using TokonyadiaRestAPI.Controllers;
using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Entities;

namespace TokonyadiaRestAPI.Services;

public interface IAuthService
{
    Task<UserCredential> LoadByEmail(string email);
    Task<RegisterResponse> Register(AuthRequest request,string route);
    Task<LoginResponse> Login(AuthRequest request);


}