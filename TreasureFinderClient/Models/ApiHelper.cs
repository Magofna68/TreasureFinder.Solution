using System.Threading.Tasks;
using RestSharp;
using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace TreasureFinder.Models
{
  class ApiHelper
  {
    public static async Task<string> GetAll(string title, string description, string address, string startdate, string enddate, string condition, bool images)
    {
      RestClient client = new("http://localhost:4000/api");
      RestRequest request = new($"items", Method.GET);
      var response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }


    public static async Task<string> GetWithId(int id)
    {
      RestClient client = new("http://localhost:4000/api");
      RestRequest request = new($"items/{id}", Method.GET);
      var response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }

    public static async Task<string> Post(string newItem)
    {
      RestClient client = new("http://localhost:4000/api");
      RestRequest request = new($"items", Method.POST);
      request.AddHeader("Content-Type", "application/json");
      request.AddJsonBody(newItem);
      var response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }

    public static async Task Delete(int id)
    {
      RestClient client = new("http://localhost:4000/api");
      RestRequest request = new($"items/{id}", Method.DELETE);
      request.AddHeader("Content-Type", "application/json");
      var _ = await client.ExecuteTaskAsync(request);
    }
    public static async Task Put(int id, string newItem)
    {
      RestClient client = new("http://localhost:4000/api");
      RestRequest request = new($"items/{id}", Method.PUT);
      request.AddHeader("Content-Type", "application/json");
      request.AddJsonBody(newItem);
      _ = await client.ExecuteTaskAsync(request);
    }

    // Images
    public static async Task<string> PostImage(int itemId, IFormFile file)
    {
     
      var newImage = await ConvertFileToImage(itemId, file);
      string jsonFile = JsonConvert.SerializeObject(newImage);
      RestClient client = new("http://localhost:4000/api");
      RestRequest request = new($"images/{itemId}", Method.POST);
      request.AddHeader("Content-Type", "application/json");
      Console.WriteLine($"jsonFile {jsonFile}");
      request.AddJsonBody(jsonFile);
      var response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }

     private static async Task<Image> ConvertFileToImage(int itemId, IFormFile file)
    {
      if (file.Length > 0)
        {
          var filePath = Path.GetTempFileName();
          using var stream = System.IO.File.Create(filePath);
          await file.CopyToAsync(stream);
        }
      
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        var fileBytes = ms.ToArray();
        var newImage = new Image();
        string s = Convert.ToBase64String(fileBytes);
        newImage.ImageString = s;
        newImage.ItemId = itemId;
      return newImage;
    }
  }
}