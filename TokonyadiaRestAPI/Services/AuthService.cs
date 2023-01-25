using TokonyadiaRestAPI.Controllers;
using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Entities;
using TokonyadiaRestAPI.Exception;
using TokonyadiaRestAPI.Exceptions;
using TokonyadiaRestAPI.Repositories;
using TokonyadiaRestAPI.Security;

namespace TokonyadiaRestAPI.Services;

public class AuthService:IAuthService
{
    private readonly IRepository<UserCredential> _repository;
    private readonly IRoleService _roleService;
    private readonly ICustomerService _customerService;
    private readonly IPersistence _persistence;
    private readonly IJwtUtils _jwtUtils;

    public AuthService(IRepository<UserCredential> repository, IRoleService roleService,
        ICustomerService customerService, IPersistence persistence,IJwtUtils jwtUtils)
    {
        _repository = repository;
        _roleService = roleService;
        _customerService = customerService;
        _persistence = persistence;
        _jwtUtils = jwtUtils;

    }
    public async Task<UserCredential> LoadByEmail(string email)
    {
        var user = await _repository.Find(credential => credential.Email.Equals(email));
        if (user is null) throw new NotFoundException("not found");
        return user;
    }

    public async Task<RegisterResponse> Register(AuthRequest request,string route)
    {
        var registerResponse = _persistence.ExecuteTransactionAsync(async () =>
        {
        
            var role = await _roleService.SaveOrGet(route.Contains("admin")?Erole.Admin:Erole.Customer);
            var userCredential = new UserCredential
            {
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = role,
            };
            var SaveUser = await _repository.Save(userCredential);
            // if (!route.Contains("admin"))
            // {
            //     await _customerService.CreateNewCustomer(new Customer { UserCredentials = SaveUser });
            // }
            await _customerService.CreateNewCustomer(new Customer { UserCredentials = SaveUser });

            return new RegisterResponse()
            {
                Email = SaveUser.Email,
                Role = SaveUser.Role.Erole.ToString()
            };

         
        });
        return await registerResponse;
    }
    // public async Task<RegisterResponse> RegisterAdmin(AuthRequest request)
    // {
    //     var registerResponse = _persistence.ExecuteTransactionAsync(async () =>
    //     {
    //         var role = await _roleService.SaveOrGet(Erole.Admin);
    //         var userCredential = new UserCredential
    //         {
    //             Email = request.Email,
    //             Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
    //             Role = role,
    //         };
    //         var SaveUser = await _repository.Save(userCredential);
    //         await _customerService.CreateNewCustomer(new Customer { UserCredentials = SaveUser });
    //
    //         return new RegisterResponse()
    //         {
    //             Email = SaveUser.Email,
    //             Role = SaveUser.Role.Erole.ToString()
    //         };
    //
    //      
    //     });
    //     return await registerResponse;
    // }

    public  async  Task<LoginResponse> Login(AuthRequest request)
    {
      var user=  await _repository.Find(credential => credential.Email.Equals((request.Email)),includes:new []{"Role"});
      if (user is null) throw new UnauthorizedAccessException();
      var verify=BCrypt.Net.BCrypt.Verify(request.Password,user.Password);
      if (!verify) throw new UnauthorizedAccessException();
      var token = _jwtUtils.GenerateToken(user);
      return new LoginResponse()
      {
          Email = user.Email,
          Role = user.Role.Erole.ToString(),
          Token = token
      };
    }
}