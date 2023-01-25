using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TokonyadiaRestAPI.Controllers;
[ApiController]
[Authorize]
public class BaseController:ControllerBase
{
    
}