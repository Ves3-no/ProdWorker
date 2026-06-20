using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ProductsAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly object FileLock = new object();
        const string ApiKey = "ves-sada423424dawadw23";

        [HttpGet]
        public IActionResult Get() {
            if (!Request.Headers.TryGetValue("My-Custom-Header", out var headerValue))
            {
                return (Unauthorized("Invalid API key"));
            }
            if (!string.Equals(headerValue, ValuesController.ApiKey))
            {
                return (Unauthorized("Invalid API key"));
            }
            var jsonContent = JsonNode.Parse(System.IO.File.ReadAllText("file.json"));
            return Ok(jsonContent);
        }

        [HttpGet("{id}")]
        public IActionResult GetID([FromRoute] int id)
        {
            if (!Request.Headers.TryGetValue("My-Custom-Header", out var headerValue))
            {
                return (Unauthorized("Invalid API key"));
            }
            if (!string.Equals(headerValue, ValuesController.ApiKey))
            {
                return (Unauthorized("Invalid API key"));
            }

            var jsonContent = JsonNode.Parse(System.IO.File.ReadAllText("file.json"));
            var jsonObject = jsonContent!.AsObject();
            if (jsonObject.ContainsKey(id.ToString())) {
                return (Ok(jsonContent[id.ToString()]));
            } else {
                return (Ok("Object Doesent Exisist"));
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] ContentModel content) {
            if (!Request.Headers.TryGetValue("My-Custom-Header", out var headerValue))
            {
                return (Unauthorized("Invalid API key"));
            }
            if (!string.Equals(headerValue, ValuesController.ApiKey))
            {
                return (Unauthorized("Invalid API key"));
            }
            string key = content.Id.ToString();
            Product prod = new Product() { Id = content.Id, Category = content.Category, Name = content.Name };
            prod.UptatePrice(content.Price);

            var options = new JsonSerializerOptions { WriteIndented = true };
            
            lock (FileLock)
            {
                var jsonContent = JsonNode.Parse(System.IO.File.ReadAllText("file.json"));
                jsonContent[key] = JsonSerializer.SerializeToNode(prod);
                string jsonString = JsonSerializer.Serialize(jsonContent, options);
                System.IO.File.WriteAllText("file.json", jsonString);
            }
            return Ok("Sucsess on creating the prod");


        }
        
        public record ContentModel(int? Id, string? Name, string? Category, int? Price);
        public class Product
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public string? Category { get; set; }
            public int? Price { get; private set; }

            public void UptatePrice(int? NewPrice)
            {
                Price = NewPrice;
            }
        }
    } 
}
