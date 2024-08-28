using DevFreela.Core.Entities;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Get(string search = "", int page = 0, int size = 3)
        {
            //return Ok(_configService.GetValue());

            // Configurações personalizadas do appsettings

            var projects = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                .Skip(page * size)
                .Take(size)
                .ToList();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            var project = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == id);

            var model = ProjectItemViewModel.FromEntity(project);

            return Ok(model);
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

            var project = model.ToEntity();

            _context.Projects.Add(project);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
                return NotFound();

            project.Update(model.Title, model.Description, model.TotalCost);

            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
                return NotFound();

            project.SetAsDeleted();
            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
                return NotFound();

            project.Start();
            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
                return NotFound();

            project.Complete();
            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
                return NotFound();

            var comment = new ProjectComment(model.Content, model.IdProject, model.IdUser);

            _context.ProjectComments.Add(comment);
            _context.SaveChanges();

            return Ok();
        }
    }
}
