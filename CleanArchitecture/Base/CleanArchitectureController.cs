using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Base
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CleanArchitectureController : ControllerBase
    {
    }
}
