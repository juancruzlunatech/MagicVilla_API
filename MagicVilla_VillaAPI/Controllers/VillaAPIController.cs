using Microsoft.AspNetCore.Mvc;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;

namespace MagicVilla_VillaAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villalist);

        }

        [HttpGet("{id:int}", Name = "GetVillaName")]
        //[ProducesResponseType(200, Type = typeof(VillaDTO))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var Villa = VillaStore.villalist.FirstOrDefault(u => u.Id == id);
            if (Villa == null)
            {
                return NotFound();
            }
            return Ok(Villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDto)
        {
            //add a custorm error message in the model state if the name is already in the list
            if (VillaStore.villalist.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already Exist");
                return BadRequest(ModelState);

            }

            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDto.Id = VillaStore.villalist.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VillaStore.villalist.Add(villaDto);
            //return Ok(villaDto);
            return CreatedAtRoute("GetVillaName", new { id = villaDto.Id }, villaDto);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var Villa = VillaStore.villalist.FirstOrDefault(u => u.Id == id);
            if (Villa == null)
            {
                return NotFound();
            }
            VillaStore.villalist.Remove(Villa);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO )
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                    return BadRequest();
            }   
            var villa = VillaStore.villalist.FirstOrDefault(u=>u.Id == id); 
            villa.Name = villaDTO.Name; 
            villa.Sqft= villaDTO.Sqft;  
            villa.Ocuupancy = villaDTO.Ocuupancy;
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        public IActionResult UpdatePartalVilla(int id, JsonPatchDocument<VillaDTO> patchDto )
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villalist.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }
            patchDto.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }


    }
}
