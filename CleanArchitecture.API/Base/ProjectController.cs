using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Base
{

    [Authorize]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
    }
}
