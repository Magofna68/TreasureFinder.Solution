using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreasureFinder.Models;
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
namespace TreasureFinder.Controllers
{
  public class ImagesController : Controller
  {
    public IActionResult Upload(int id)
    {
      Console.WriteLine($"itemId in index is: {id}");
      var item = Item.GetWithId(id);
      return View(item);
    }
  
    [HttpPost]

    public async Task<ActionResult> Upload(IFormFile file , int itemId)
    {
        // long size = images.Sum(i => i.Length);

      // foreach (var formFile in images)
      // {
      if (file.Length > 0)
        {
          var filePath = Path.GetTempFileName();
          using var stream = System.IO.File.Create(filePath);
          await file.CopyToAsync(stream);
        }
      // }
     
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        var fileBytes = ms.ToArray();
        var newImage = new Image();
        string s = Convert.ToBase64String(fileBytes);
        newImage.ImageString = s;
        newImage.ItemId = itemId;
    
      Image.Post(newImage);
      return RedirectToAction("Details", "Items", new {id = itemId });
    }
  }
}