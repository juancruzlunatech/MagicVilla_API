using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MagicVilla_WebPage.Services.IServices;
using MagicVilla_WebPage.Models.Dto;
using MagicVilla_WebPage.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using MagicVilla_WebPage.Services;

namespace MagicVilla_WebPage.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villanumberservice;
        private readonly IMapper _mapper;
        private readonly IVillaService _villaService;

        public VillaNumberController(IVillaNumberService villanumberservice, IMapper mapper,IVillaService villaService)
        {
            _mapper = mapper;
            _villanumberservice = villanumberservice;
            _villaService = villaService;
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

            var villaListResponse = await _villaService.GetAllAsync<APIResponse>();
            if (villaListResponse != null && villaListResponse.IsSuccess)
            {
                var villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(villaListResponse.Result));
                ViewBag.Villas = villaList.Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }).ToList();
            }


            return View(list);
        }

        public async Task<IActionResult> CreateVillaNumber()
        {
            var villaListResponse = await _villaService.GetAllAsync<APIResponse>();
            if (villaListResponse != null && villaListResponse.IsSuccess)
            {
                var villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(villaListResponse.Result));
                ViewBag.Villas = villaList.Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }).ToList();
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villanumberservice.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(VillaNumberIndex));
                }

            }
            return View(model);
        }

        public async Task<IActionResult> UpdateVillaNumber(int VillaId)
        {

            var response = await _villanumberservice.GetAsync<APIResponse>(VillaId);
            if (response != null && response.IsSuccess)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<VillaNumberUpdateDTO>(model));
            }

            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villanumberservice.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(VillaNumberIndex));
                }

            }
            return View(model);
        }

        public async Task<IActionResult> DeleteVillaNumber(int VillaId)
        {

            var response = await _villanumberservice.GetAsync<APIResponse>(VillaId);
            if (response != null && response.IsSuccess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<VillaNumberDTO>(model));
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDTO model)
        {

            var response = await _villanumberservice.DeleteAsync<APIResponse>(model.VillaID);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(VillaNumberIndex));
            }

            return View(model);
        }

    }
}
