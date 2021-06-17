using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreasureFinder.Models;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TreasureFinder.Controllers
{
  [Authorize]
  public class ItemsController : Controller
  {
    private readonly TreasureFinderContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ItemsController(UserManager<ApplicationUser> userManager, TreasureFinderContext db)
    {
      _db = db;
      _userManager = userManager;
    }
    [AllowAnonymous]
    public IActionResult Index(string title, string description, string address, string startdate, string enddate, string condition, bool images)
    {
      var items = Item.GetItems(title, description, address, startdate, enddate, condition, images);
      return View(items);
    }
    [AllowAnonymous]
    public IActionResult Details(int id)
    {
      var item = Item.GetWithId(id);
      return View(item);
    }

    public IActionResult Edit(int id)
    {
      var item = Item.GetWithId(id);
      return View(item);
    }

    [HttpPost]
    public IActionResult Edit(Item item)
    {
      Item.Put(item);
      return RedirectToAction("Details", new { id = item.ItemId });
    }

  
    public IActionResult Create() => View();

    
    [HttpPost]
    public  ActionResult Create(Item item)
    {
      item.CreatedAt = DateTime.Now;
      var postItem = Item.Post(item);
      return RedirectToAction("Upload", "Images",  new {id = postItem.ItemId});
    }
    public async Task<ActionResult> Delete(int id)
    {
      var jsonItem = await ApiHelper.GetWithId(id);
      var foundItem = JsonConvert.DeserializeObject<Item>(jsonItem);
      return View(foundItem);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Item.Delete(id);
      return RedirectToAction("Index");
    }
  }
}