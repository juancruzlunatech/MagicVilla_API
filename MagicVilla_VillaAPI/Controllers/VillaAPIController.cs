using Microsoft.AspNetCore.Mvc;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas() 
        {
            return VillaStore.villalist;
            
        }

        [HttpGet("id")]
        public VillaDTO GetVilla( int id)
        {
            return VillaStore.villalist.FirstOrDefault(u=>u.Id==id);

        }


    }
}
