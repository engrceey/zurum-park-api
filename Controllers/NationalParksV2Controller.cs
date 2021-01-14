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
    // [ApiExplorerSettings(GroupName="parkv1")]
    public class NationalParksV2Controller : Controller
    {
        private readonly IMapper _mapper;
        private readonly INationalParkRepository _repository;

        public NationalParksV2Controller(IMapper mapper,
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
    }
}