using Microsoft.AspNetCore.Mvc;
using PruebaRiwi.Enums;
using PruebaRiwi.Models;
using PruebaRiwi.Services;

namespace PruebaRiwi.Controllers;

public class SportPlaceController : Controller
{
    private readonly SportPlaceService _sportPlaceService;

    public SportPlaceController(SportPlaceService sportPlaceService)
    {
        _sportPlaceService = sportPlaceService;
    }
    
    public IActionResult Index()
    {
        var response = _sportPlaceService.GetAll();
        
        return View(response.Data);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(SportPlace sportPlace)
    {
        var response = _sportPlaceService.Register(sportPlace);
        if (response.Success)
        {
            TempData["Success"] = response.Message;
            return RedirectToAction("Index");
        }
        ViewBag.Error = response.Message;
        return View(sportPlace);
    }
    
    public IActionResult Edit(int id)
    {
        var response = _sportPlaceService.GetById(id);
        if (response.Success)
        {
            return View(response.Data);
        }
        TempData["Error"] = response.Message;
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Edit(SportPlace sportPlace)
    {
        var response = _sportPlaceService.Edit(sportPlace);
        if (response.Success)
        {
            TempData["Success"] = response.Message;
            return RedirectToAction("Index");
        }
        return View(sportPlace);
    }
    
    public IActionResult FilterByType(PlaceType type)
    {
        var response = _sportPlaceService.FilterByType(type);
        return View("Index", response.Data);
    }
}