using Microsoft.AspNetCore.Mvc;
using PruebaRiwi.Models;
using PruebaRiwi.Services;

namespace PruebaRiwi.Controllers;

public class UserController : Controller
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {
        var response = _userService.GetAll();

        if (!response.Success)
        {
            ViewBag.Error = response.Message; 
        }

        return View(response.Data);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        var response = _userService.Register(user);

        if (response.Success)
        {
            TempData["Success"] = response.Message; 
            return RedirectToAction("Index");
        }

        ViewBag.Error = response.Message; 
        return View(user);
    }

    public IActionResult Edit(int id)
    {
        var response = _userService.GetById(id);

        if (response.Success)
        {
            return View(response.Data);
        }

        TempData["Error"] = response.Message; 
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Edit(User user)
    {
        var response = _userService.Edit(user);

        if (response.Success)
        {
            TempData["Success"] = response.Message; 
            return RedirectToAction("Index");
        }

        ViewBag.Error = response.Message; 
        return View(user);
    }
}