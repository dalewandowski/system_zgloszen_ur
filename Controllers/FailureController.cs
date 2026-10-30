using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using system_zgloszen_ur.Data;
using system_zgloszen_ur.Entities;
using system_zgloszen_ur.Models;
using System_zgloszen_UR.DTOs.Failures;
using System_zgloszen_UR.Mappers;

namespace system_zgloszen_ur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FailuresController : ControllerBase
    {

        private readonly AppDbContext _context;

        public FailuresController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Failure>>> GetAllTask()
        {
            var allFailures = await _context.Failures.AsNoTracking().OrderByDescending(f => f.CreatedAt).ToListAsync();
            var DtoFailures = allFailures.Select(f => FailureMapper.FailureToDto(f));

           return Ok(DtoFailures);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FailureDto>> GetById([FromRoute] int id)
        {
            var failure = await _context.Failures.FirstOrDefaultAsync(f => f.Id == id);

            if (failure == null)
            {
                return NotFound($"Awaria z ID: ({id}) nie istnieje");
            }

            var failureDto = FailureMapper.FailureToDto(failure);
            return Ok(failureDto);
        }

        [HttpPost("/api/failures")]
        public async Task<ActionResult<CreateFailureDto>> AddNewFailure([FromRoute] int id, [FromBody] CreateFailureDto newFailureDto)
        {
            var newFailure = new Failure
            {
                Title = newFailureDto.Title,
                FailureDescription = newFailureDto.FailureDescription,
                MachineName = newFailureDto.MachineName,
                Location = newFailureDto.Location
            };

            await _context.Failures.AddAsync(newFailure);
            _context.SaveChanges();



            return Ok($"Dodano nową awarię: {newFailure}");
        }

    }
}
