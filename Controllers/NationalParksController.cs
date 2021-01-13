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
    public class NationalParksController : Controller
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

        
        [HttpGet("{id}")]
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
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalPark)
        {
            if (nationalPark == null) 
            {
                return BadRequest(ModelState);         
            }

            if(_repository.NationalParkExists(nationalPark.Name))
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

            return Ok();
        }
    }
}