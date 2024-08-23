using DevFreela.API.Models;
using DevFreela.API.Persistence;
using DevFreela.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        //private readonly FreelancerTotalCostingConfig _config;
        //private readonly IConfigService _configService;
        //public ProjectsController(
        //    IOptions<FreelancerTotalCostingConfig> options,
        //    IConfigService configService
        //    ) 
        //{
        //    _config = options.Value;
        //    _configService = configService;
        //}

        private readonly DevFreelaDbContext _context;

        public ProjectsController(DevFreelaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(string search = "")
        {
            //return Ok(_configService.GetValue());

            // Configurações personalizadas do appsettings

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            //if(model.TotalCost < _config.Minimum ||  model.TotalCost > _config.Maximum)
            //{
            //    return BadRequest("Custo fora dos limites");
            //}
            //return CreatedAtAction(nameof(GetById), new { id = 1}, model);

            //Funciona para gerenciamento de exceções

            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
        {
            return Ok();
        }
    }
}
