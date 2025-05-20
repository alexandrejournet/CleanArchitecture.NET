using CleanArchitecture.API.Base;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.BO;
using CleanArchitecture.Domain.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    public class EntityController : ProjectController
    {
        private readonly EntityService _entityService;

        public EntityController(EntityService entityService)
        {
            _entityService = entityService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            ActionResult result = StatusCode(StatusCodes.Status500InternalServerError);
            List<Entity> mots = await _entityService.GetAllAsync();
            if (mots.IsNotEmpty())
            {
                result = Ok(mots);
            }
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            ActionResult result = StatusCode(StatusCodes.Status500InternalServerError);
            Entity mot = await _entityService.GetById(id);
            if (mot != null)
            {
                result = Ok(mot);
            }
            return result;
        }
    }
}