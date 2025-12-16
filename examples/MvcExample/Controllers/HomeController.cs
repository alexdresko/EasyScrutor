using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcExample.Models;
using MvcExample.Services;

namespace MvcExample.Controllers;

/// <summary>
/// Home controller for the MVC example application.
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// The message service instance.
    /// </summary>
    private readonly IMessageService _messageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeController"/> class.
    /// </summary>
    /// <param name="messageService">The message service.</param>
    public HomeController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    /// <summary>
    /// Displays the home page with a message from the message service.
    /// </summary>
    /// <returns>The index view.</returns>
    public IActionResult Index()
    {
        ViewBag.Message = _messageService.GetMessage();
        return View();
    }

    /// <summary>
    /// Displays the privacy policy page.
    /// </summary>
    /// <returns>The privacy view.</returns>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Displays the error page.
    /// </summary>
    /// <returns>The error view with error details.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
