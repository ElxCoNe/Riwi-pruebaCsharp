using Microsoft.AspNetCore.Mvc;
using PruebaRiwi.Models;
using PruebaRiwi.Services;

namespace PruebaRiwi.Controllers;

public class ReservationController : Controller
{
    private readonly ReservationService _reservationService;
    private readonly UserService _userService;
    private readonly SportPlaceService _sportPlaceService;

    public ReservationController(ReservationService reservationService, UserService userService, SportPlaceService sportPlaceService)
    {
        _reservationService = reservationService;
        _userService = userService;
        _sportPlaceService = sportPlaceService;
    }

    public IActionResult Index()
    {
        var response = _reservationService.GetAll();
        return View(response.Data);
    }

    public IActionResult Create()
    {
        ViewBag.Users = _userService.GetAll().Data;
        ViewBag.SportPlaces = _sportPlaceService.GetAll().Data;
        return View();
    }

    [HttpPost]
    public IActionResult Create(Reservation reservation)
    {
        var response = _reservationService.Create(reservation);
        if (response.Success)
        {
            return RedirectToAction("Index");
        }
        ViewBag.Error = response.Message;
        ViewBag.Users = _userService.GetAll().Data;
        ViewBag.SportPlaces = _sportPlaceService.GetAll().Data;
        return View(reservation);
    }

    public IActionResult Cancel(int id)
    {
        _reservationService.Cancel(id);
        return RedirectToAction("Index");
    }

    public IActionResult Finish(int id)
    {
        _reservationService.Finish(id);
        return RedirectToAction("Index");
    }

    public IActionResult ByUser(int userId)
    {
        var response = _reservationService.GetByUser(userId);
        ViewBag.Users = _userService.GetAll().Data;
        return View("Index", response.Data);
    }

    public IActionResult ByPlace(int placeId)
    {
        var response = _reservationService.GetByPlace(placeId);
        ViewBag.SportPlaces = _sportPlaceService.GetAll().Data;
        return View("Index", response.Data);
    }
}