using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZurumPark.Dtos;
using ZurumPark.Entities;
using ZurumPark.Repository.IRepository;

namespace ZurumPark.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TrailController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITrailRepository _repository;

        public TrailController(IMapper mapper,
                                    ITrailRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TrailDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetTrails()
        {
            var values = _repository.GetTrails();

            var objDto = new List<TrailDto>();

            foreach (var obj in values)
            {
                objDto.Add(_mapper.Map<TrailDto>(obj));
            }

            return Ok(objDto);
        }

        [HttpGet("{id}", Name = "GetTrail")]
        [ProducesResponseType(200, Type = typeof(TrailDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrail(int id)
        {
            var value = _repository.GetTrail(id);

            if (value == null)
            {
                return NotFound();
            }
            var park = _mapper.Map<TrailDto>(value);
            return Ok(park);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TrailDto))]
        [ProducesResponseType(400)] 
        [ProducesResponseType(500)]
        [ProducesDefaultResponseType]
        public IActionResult CreateTrail([FromBody] TrailCreateDto trail)
        {
            if (trail == null)
            {
                return BadRequest(ModelState);
            }

            if (_repository.TrailExists(trail.Name))
            {
                ModelState.AddModelError("", "Trail Park Exist");
                return StatusCode(404, ModelState);
            }

            var park = _mapper.Map<Trail>(trail);

            if (!_repository.CreateTrail(park))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {park.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("Trail", new { id = park.Id }, park);
        }


        [HttpPatch("{id}", Name = "UpdateTrail")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult UpdateTrail(int id, [FromBody] TrailUpdateDto trail)
        {

            if (trail == null || id != trail.Id)
            {
                return BadRequest(ModelState);
            }

            var park = _mapper.Map<Trail>(trail);

            if (!_repository.UpdateTrail(park))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {park.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeleteTrail")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult DeleteTrail(int id)
        {

            if (!_repository.TrailExists(id))
            {
                return NotFound();
            }

            var trail = _repository.GetTrail(id);

            if (!_repository.DeleteTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {trail.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}