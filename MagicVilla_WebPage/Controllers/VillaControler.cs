using AutoMapper;
using MagicVilla_WebPage.Models;
using MagicVilla_WebPage.Models.Dto;
using MagicVilla_WebPage.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_WebPage.Controllers
{
    public class VillaControler : Controller
    {
        private readonly IVillaService _service;
        private readonly IMapper _mapper;

        public VillaControler(IVillaService service, IMapper mapper)
        {
             _mapper = mapper;   
            _service = service;
        }


        public async Task<IActionResult> VillaIndex()
        {
            List<VillaDTO> list = new();

            var response = await _service.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess) 
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            
            return View(list);
        }
    }
}
