using AutoMapper;
using MagicVilla_WebPage.Models;
using MagicVilla_WebPage.Models.Dto;
using MagicVilla_WebPage.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicVilla_WebPage.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaService _service;
        private readonly IMapper _mapper;

        public HomeController(IVillaService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }


        public async Task<IActionResult> Index()
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