using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("Login")]
    public IActionResult Login(string Name)
    {
        HttpContext.Session.SetString("Name", Name);
        HttpContext.Session.SetInt32("Counter", 22);
        HttpContext.Session.SetInt32("FormCounter", 88);
        return RedirectToAction("Dashboard");
    }

    [HttpGet("Dashboard")]
    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetString("Name") == null)
        {
            return RedirectToAction("Index");
        }
        return View();
        
        
    }

    //passing in a parameter from dashboard (+1, -1, x2, rand)
    [HttpGet("CounterFunction/{operation}")]
    public IActionResult CounterFunction(string operation)
    {
        if (HttpContext.Session.GetString("Name") == null)
        {
            return RedirectToAction("Index");
        }

        //Create variable counter that converts to Int 32
        int counter = Convert.ToInt32(HttpContext.Session.GetInt32("Counter"));
        //Random Instance
        Random rand = new Random();

        if (operation == "+1")
        {
            counter += 1;
            HttpContext.Session.SetInt32("Counter", counter);
        } else if (operation == "-1")
        {
            counter -= 1;
            HttpContext.Session.SetInt32("Counter", counter);
        } else if (operation == "x2")
        {
            counter *= 2;
            HttpContext.Session.SetInt32("Counter", counter);
        } else if (operation == "rand")
        {
            counter += rand.Next(1,11);
            HttpContext.Session.SetInt32("Counter", counter);
        }
        return RedirectToAction("Dashboard");
    }

    [HttpPost("PlusOne")]
    public IActionResult PlusOne()
    {
        int counter = Convert.ToInt32(HttpContext.Session.GetInt32("FormCounter"));
        counter += 1;
        HttpContext.Session.SetInt32("FormCounter", counter);
        return RedirectToAction("Dashboard");
    }

    [HttpPost("MinusOne")]
    public IActionResult MinusOne()
    {
        int counter = Convert.ToInt32(HttpContext.Session.GetInt32("FormCounter"));
        counter -= 1;
        HttpContext.Session.SetInt32("FormCounter", counter);
        return RedirectToAction("Dashboard");
    }
    
    [HttpPost("TimesTwo")]
    public IActionResult TimesTwo()
    {
        int counter = Convert.ToInt32(HttpContext.Session.GetInt32("FormCounter"));
        counter *= 2;
        HttpContext.Session.SetInt32("FormCounter", counter);
        return RedirectToAction("Dashboard");
    }

    [HttpPost("DivideTwo")]
    public IActionResult DivideTwo()
    {
        int counter = Convert.ToInt32(HttpContext.Session.GetInt32("FormCounter"));
        counter /= 2;
        HttpContext.Session.SetInt32("FormCounter", counter);
        return RedirectToAction("Dashboard");
    }

    [HttpPost("PlusRandom")]
    public IActionResult PlusRandom()
    {
        int counter = Convert.ToInt32(HttpContext.Session.GetInt32("FormCounter"));
        Random random = new Random();
        counter += random.Next(1,11);
        HttpContext.Session.SetInt32("FormCounter", counter);
        return RedirectToAction("Dashboard");
    }

    [HttpPost("MinusRandom")]
    public IActionResult MinusRandom()
    {
        int counter = Convert.ToInt32(HttpContext.Session.GetInt32("FormCounter"));
        Random random = new Random();
        counter -= random.Next(1,11);
        HttpContext.Session.SetInt32("FormCounter", counter);
        return RedirectToAction("Dashboard");
    }

    [HttpPost("Reset")]
    public IActionResult Reset()
    {
        HttpContext.Session.SetInt32("FormCounter", 0);
        return RedirectToAction("Dashboard");
    }

    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
