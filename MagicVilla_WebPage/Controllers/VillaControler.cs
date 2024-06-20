using AutoMapper;
using MagicVilla_WebPage.Models;
using MagicVilla_WebPage.Models.Dto;
using MagicVilla_WebPage.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

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
        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(CreateVillaDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(VillaIndex));
                }

            }
            return View(model);
        }

        public async Task<IActionResult> UpdateVilla(int VillaId)
        {

            var response = await _service.GetAsync<APIResponse>(VillaId);
            if (response != null && response.IsSuccess)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<UpdateVillaDTO>(model));
            }

            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(UpdateVillaDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(VillaIndex));
                }

            }
            return View(model);
        }

        public async Task<IActionResult> DeleteVilla(int VillaId)
        {

            var response = await _service.GetAsync<APIResponse>(VillaId);
            if (response != null && response.IsSuccess)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<VillaDTO>(model));
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDTO model)
        {
            
            var response = await _service.DeleteAsync<APIResponse>(model.Id);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(VillaIndex));
            }

            return View(model);
        }

    }
}
