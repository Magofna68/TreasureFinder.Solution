using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreasureFinder.Models;
using System;
using Newtonsoft.Json;

namespace TreasureFinder.Controllers
{
  public class ItemsController : Controller
  {
    public IActionResult Index(string title, string description, string address, string startdate, string enddate, string condition, bool images)
    {
      var items = Item.GetItems(title, description, address, startdate, enddate, condition, images);
      return View(items);
    }
    public IActionResult Details(int id)
    {
      var item = Item.GetWithId(id);
      return View(item);
    }

    public IActionResult Create() => View();
    [HttpPost]
    public IActionResult Create(Item item)
    {
      Item.Post(item);
      return RedirectToAction("Index");
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