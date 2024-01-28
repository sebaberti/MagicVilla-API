using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;
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
        private readonly ApplicationDbContext _db;


        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<VillaDto>> GetVilla() 
        {
            _logger.LogInformation("Obtener las villas");
            return Ok(_db.Villas.ToList());
        }

        [HttpGet("id:int", Name = "GetVilla")]  //  le asigno un nombre al endpoint 
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id) 
        {
            if(id == 0)
            {
                _logger.LogError("Error al traer la Villa con Id " + id);
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);

            if(villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            if (_db.Villas.FirstOrDefault(v => v.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("ExistName", "La villa con ese nombre ya existe ");
                return BadRequest(ModelState);
            }

            if(villaDto == null) 
            {
                return BadRequest(villaDto);
            }
            if(villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Villa model = new()
            {
                
                Name = villaDto.Name,
                Detail = villaDto.Detail,
                ImgageUrl = villaDto.ImageUrl,
                Capacity = villaDto.Capacity,
                Price = villaDto.Price,
                SquareMetres = villaDto.SquareMetres,
                Amenity = villaDto.Amenity

            };

            _db.Villas.Add(model);
            _db.SaveChanges();

           // villaDto.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1 ;
           // VillaStore.villaList.Add(villaDto);

            return CreatedAtRoute("GetVilla", new {id = villaDto.Id}, villaDto );
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
            
        public IActionResult DeleteVilla(int id) 
        { 

            if(id== 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();

            return NoContent();

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto ) 

        {
            if(villaDto == null || id!= villaDto.Id)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            //villa.Name = villaDto.Name;
            //villa.Capacity = villaDto.Capacity;
            //villa.SquareMetres = villaDto.SquareMetres;

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Detail = villaDto.Detail,
                ImgageUrl = villaDto.ImageUrl,
                Capacity = villaDto.Capacity,
                SquareMetres = villaDto.SquareMetres,
                Price = villaDto.Price,
                Amenity = villaDto.Amenity
            };

            _db.Villas.Update(model);
            _db.SaveChanges();

            return NoContent();

        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialVilla(int id,  JsonPatchDocument<VillaDto> patchDto)

        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var villa = _db.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id);

            VillaDto villaDto = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Detail = villa.Detail,
                ImageUrl = villa.ImgageUrl,
                Capacity = villa.Capacity,
                Price = villa.Price,
                SquareMetres = villa.SquareMetres,
                Amenity = villa.Amenity

            };

            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            if(villa == null) return BadRequest();

            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Detail = villaDto.Detail,
                ImgageUrl = villaDto.ImageUrl,
                Capacity = villaDto.Capacity,
                Price = villaDto.Price,
                SquareMetres = villaDto.SquareMetres,
                Amenity = villaDto.Amenity
            };
            
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();

        }

    }
}
