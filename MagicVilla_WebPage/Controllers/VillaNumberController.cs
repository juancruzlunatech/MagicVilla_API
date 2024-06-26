using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MagicVilla_WebPage.Services.IServices;
using MagicVilla_WebPage.Models.Dto;
using MagicVilla_WebPage.Models;
using Newtonsoft.Json;

namespace MagicVilla_WebPage.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villanumberservice;
        private readonly IMapper _mapper;

        public VillaNumberController(IVillaNumberService villanumberservice, IMapper mapper)
        {
            _mapper = mapper;
            _villanumberservice = villanumberservice;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> VillaNumberIndex()
        {
            List<VillaNumberDTO> list = new();

            var response = await _villanumberservice.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }
    }
}
