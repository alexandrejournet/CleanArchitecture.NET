using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers
{
    public class MotController : Controller
    {
        MotService _motService;

        public MotController(MotService motService)
        {
            _motService = motService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                ActionResult result = StatusCode(500);
                List<Mot> mots = await _motService.GetAllAsync();
                if(mots.IsNotEmpty())
                {
                    result = Ok(mots);
                }
                return result;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.StackTrace);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById()
        {
            try
            {
                ActionResult result = StatusCode(500);
                Mot mot = await _motService.GetById(1);
                if (mot != null)
                {
                    result = Ok(mot);
                }
                return result;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.StackTrace);
            }
        }
    }
}