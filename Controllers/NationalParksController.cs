using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZurumPark.Dtos;
using ZurumPark.Entities;
using ZurumPark.Repository.IRepository;

namespace ZurumPark.Controllers
{

    [Route("api/NationalParks")]
    [ApiController]
    // [ApiExplorerSettings(GroupName="parkv1")]
    public class NationalParksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INationalParkRepository _repository;

        public NationalParksController(IMapper mapper,
                                    INationalParkRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<NationalParkDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetNationalParks()
        {
            var values = _repository.GetNationalParks();

            var objDto = new List<NationalParkDto>();

            foreach (var obj in values)
            {
                objDto.Add(_mapper.Map<NationalParkDto>(obj));
            }

            return Ok(objDto);
        }

        [HttpGet("{id}", Name = "GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(NationalParkDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalPark(int id)
        {
            var value = _repository.GetNationalPark(id);

            if (value == null)
            {
                return NotFound();
            }
            var park = _mapper.Map<NationalParkDto>(value);
            return Ok(park);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(NationalParkDto))]
        [ProducesResponseType(400)] 
        [ProducesResponseType(500)]
        [ProducesDefaultResponseType]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalPark)
        {
            if (nationalPark == null)
            {
                return BadRequest(ModelState);
            }

            if (_repository.NationalParkExists(nationalPark.Name))
            {
                ModelState.AddModelError("", "National Park Exist");
                return StatusCode(404, ModelState);
            }

            var park = _mapper.Map<NationalPark>(nationalPark);

            if (!_repository.CreateNationalPark(park))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {park.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark", new { id = park.Id }, park);
        }


        [HttpPatch("{id}", Name = "UpdateNationalPark")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult UpdateNationalPark(int id, [FromBody] NationalParkDto nationalPark)
        {

            if (nationalPark == null || id != nationalPark.Id)
            {
                return BadRequest(ModelState);
            }

            var park = _mapper.Map<NationalPark>(nationalPark);

            if (!_repository.UpdateNationalPark(park))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {park.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeleteNationalPark")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult DeleteNationalPark(int id)
        {

            if (!_repository.NationalParkExists(id))
            {
                return NotFound();
            }

            var nationalPark = _repository.GetNationalPark(id);

            if (!_repository.DeleteNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {nationalPark.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}