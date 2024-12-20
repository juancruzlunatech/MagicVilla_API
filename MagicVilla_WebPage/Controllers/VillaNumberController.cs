using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MagicVilla_WebPage.Services.IServices;
using MagicVilla_WebPage.Models.Dto;
using MagicVilla_WebPage.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using MagicVilla_WebPage.Services;
using MagicVilla_WebPage.Models.VM;
using Microsoft.AspNetCore.Authorization;
using MagicVilla_utility;
//using MagicVilla_VillaAPI.Models;

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

            var response = await _villanumberservice.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
            }

            var villaListResponse = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
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

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVM villaNumberVM = new();

            var villaListResponse = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (villaListResponse != null && villaListResponse.IsSuccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(villaListResponse.Result)).Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }); ;
            }

            return View(villaNumberVM);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villanumberservice.CreateAsync<APIResponse>(model.VillaNumber, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Created Succesfully";
                    return RedirectToAction(nameof(VillaNumberIndex));
                }
                else 
                { 
                    if (response.ErrorMessages.Count>0) 
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }

            }
            var villaListResponse = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (villaListResponse != null && villaListResponse.IsSuccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(villaListResponse.Result)).Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }); ;
            }


            TempData["error"] = "Error Encountered.";
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVillaNumber(int VillaNo)
        {

            VillaNumberUpdateVM VillaNumberVM = new();
            var response = await _villanumberservice.GetAsync<APIResponse>(VillaNo, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                VillaNumberVM.VillaNumber = _mapper.Map<VillaNumberUpdateDTO>(model);
            }

            response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                VillaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                 (Convert.ToString(response.Result)).Select(v => new SelectListItem
                 {
                     Text = v.Name,
                     Value = v.Id.ToString()
                 });
                return View(VillaNumberVM);
            }


            return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villanumberservice.UpdateAsync<APIResponse>(model.VillaNumber, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Updated Succesfully";
                    return RedirectToAction(nameof(VillaNumberIndex));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }

            }
            var villaListResponse = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (villaListResponse != null && villaListResponse.IsSuccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(villaListResponse.Result)).Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }); ;
            }


            TempData["error"] = "Error Encountered.";
            return View(model);
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVillaNumber(int VillaNo)
        {

            VillaNumberDeleteVM VillaNumberVM = new();
            var response = await _villanumberservice.GetAsync<APIResponse>(VillaNo, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                VillaNumberVM.VillaNumber = model;
            }

            response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                VillaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                 (Convert.ToString(response.Result)).Select(v => new SelectListItem
                 {
                     Text = v.Name,
                     Value = v.Id.ToString()
                 });
                return View(VillaNumberVM);
            }


            return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteVM model)
        {

            var response = await _villanumberservice.DeleteAsync<APIResponse>(model.VillaNumber.VillaNo, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa Deleted Succesfully";
                return RedirectToAction(nameof(VillaNumberIndex));
            }

            TempData["error"] = "Error Encountered.";
            return View(model);
        }

    }
}
