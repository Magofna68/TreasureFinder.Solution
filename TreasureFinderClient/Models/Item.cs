using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;


namespace TreasureFinder.Models
{
  public class Item
  {
  
    public int ItemId {get; set; }
    [Required]
    public string Title {get; set;}
    [Required]
    public string Description {get; set;}
    [Required]
    public string Address {get; set;}
    [Required]
    public string Condition {get; set;}
    public DateTime CreatedAt {get; set;}
    public string Url {get; set;}
    public string Dimensions {get; set;}
    public string Weight {get; set;}
    public int UserId {get; set;}
    public ICollection<Image> Images {get; set;}

    public string dateString()
    {
      return CreatedAt.ToString("MMMM dd, yyyy");
    }
     public static List<Item> GetItems(string title, string description, string address, string startdate, string enddate, string condition, bool images)
    {
    var task = ApiHelper.GetAll(title, description, address, startdate, enddate, condition, images);
    var result = task.Result;
    JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result);
    List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonResponse.ToString());
    return items;
    }

    public static Item GetWithId(int id)
    {
    var task = ApiHelper.GetWithId(id);
    var result = task.Result;
    JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
    Item item = JsonConvert.DeserializeObject<Item>(jsonResponse.ToString());
    return item;
    }

    public static Item Post(Item item)
    {
    string jsonItem = JsonConvert.SerializeObject(item);
    var postItem = ApiHelper.Post(jsonItem);
    var result = postItem.Result;
    JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
    Item newItem = JsonConvert.DeserializeObject<Item>(jsonResponse.ToString());
    return newItem;
    }

    public static void Delete(int id)
    {
    var _=ApiHelper.Delete(id);
    }
  }
}