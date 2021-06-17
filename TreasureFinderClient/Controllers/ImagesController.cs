using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreasureFinder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System;
namespace TreasureFinder.Controllers
{
  //[Authorize]
  public class ImagesController : Controller
  {
    public IActionResult Upload(int id)
    {
      var item = Item.GetWithId(id);
      return View(item);
    }
  
    [HttpPost]

    public  ActionResult Upload( int itemId, IFormFile file)
    {
      Console.WriteLine($"{file}");
      Image.Post(itemId, file);
      return RedirectToAction("Details", "Items", new {id = itemId });
    }
  }
}