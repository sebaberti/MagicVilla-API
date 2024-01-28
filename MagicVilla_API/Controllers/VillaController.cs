using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        //private readonly ApplicationDbContext _db;
        private readonly IVillaRepository _villaRepo;
        private readonly IMapper _mapper;
        protected ApiResponse _response;


        public VillaController(ILogger<VillaController> logger, IVillaRepository villaRepo,IMapper mapper )
        {
            _logger = logger;
            //_db = db;
            _villaRepo = villaRepo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVilla() 
        {
            _logger.LogInformation("Obtener las villas");
            IEnumerable<Villa> villaList = await _villaRepo.GetAll();
            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villaList));
        }

        [HttpGet("id:int", Name = "GetVilla")]  //  le asigno un nombre al endpoint 
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id) 
        {
            if(id == 0)
            {
                _logger.LogError("Error al traer la Villa con Id " + id);
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa =await _villaRepo.GetOne(v => v.Id == id);

            if(villa == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto createDto)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            if ( await _villaRepo.GetOne(v => v.Name.ToLower() == createDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("ExistName", "La villa con ese nombre ya existe ");
                return BadRequest(ModelState);
            }

            if(createDto == null) 
            {
                return BadRequest(createDto);
            }
            //if(villaDto.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            Villa model = _mapper.Map<Villa>(createDto);

            //Villa model = new()
            //{
                
            //    Name = villaDto.Name,
            //    Detail = villaDto.Detail,
            //    ImgageUrl = villaDto.ImageUrl,
            //    Capacity = villaDto.Capacity,
            //    Price = villaDto.Price,
            //    SquareMetres = villaDto.SquareMetres,
            //    Amenity = villaDto.Amenity

            //};

             await _villaRepo.Create(model);
             

           // villaDto.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1 ;
           // VillaStore.villaList.Add(villaDto);

            return CreatedAtRoute("GetVilla", new {id = model.Id}, model );
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
            
        public async Task<IActionResult> DeleteVilla(int id) 
        { 

            if(id== 0)
            {
                return BadRequest();
            }
            var villa = await _villaRepo.GetOne(v => v.Id == id);
            if(villa == null)
            {
                return NotFound();
            }

           _villaRepo.Remove(villa);
            

            return NoContent();

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto ) 

        {
            if(updateDto == null || id!= updateDto.Id)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            //villa.Name = villaDto.Name;
            //villa.Capacity = villaDto.Capacity;
            //villa.SquareMetres = villaDto.SquareMetres;


            Villa model = _mapper.Map<Villa>(updateDto);

            //Villa model = new()
            //{
            //    Id = villaDto.Id,
            //    Name = villaDto.Name,
            //    Detail = villaDto.Detail,
            //    ImgageUrl = villaDto.ImageUrl,
            //    Capacity = villaDto.Capacity,
            //    SquareMetres = villaDto.SquareMetres,
            //    Price = villaDto.Price,
            //    Amenity = villaDto.Amenity
            //};

            _villaRepo.Update(model);
          
            return NoContent();

        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdatePartialVilla(int id,  JsonPatchDocument<VillaUpdateDto> patchDto)

        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var villa =await _villaRepo.GetOne(v => v.Id == id,tracked:false);

            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);

            //VillaUpdateDto villaDto = new()
            //{
            //    Id = villa.Id,
            //    Name = villa.Name,
            //    Detail = villa.Detail,
            //    ImageUrl = villa.ImgageUrl,
            //    Capacity = villa.Capacity,
            //    Price = villa.Price,
            //    SquareMetres = villa.SquareMetres,
            //    Amenity = villa.Amenity

            //};

            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            if(villa == null) return BadRequest();

            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa model = _mapper.Map<Villa>(villaDto);

            //Villa model = new()
            //{
            //    Id = villaDto.Id,
            //    Name = villaDto.Name,
            //    Detail = villaDto.Detail,
            //    ImgageUrl = villaDto.ImageUrl,
            //    Capacity = villaDto.Capacity,
            //    Price = villaDto.Price,
            //    SquareMetres = villaDto.SquareMetres,
            //    Amenity = villaDto.Amenity
            //};
            
            _villaRepo.Update(model);
            
            return NoContent();

        }

    }
}
