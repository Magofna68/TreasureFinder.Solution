using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using System;
namespace TreasureFinder.Models
{
  public class Image
  {
    public int ImageId { get; set; }
    public string ImageString { get; set; }
    public int ItemId { get; set; }
    //public virtual  Item Item { get; set; }

    public static void Post(int itemId, IFormFile file)
    {
     _ = ApiHelper.PostImage(itemId, file); 
    }
  }
}