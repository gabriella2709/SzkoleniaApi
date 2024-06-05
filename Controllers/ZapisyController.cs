using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml;
using SzkoleniaAPI;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SzkoleniaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZapisyController : Controller
    {
        private readonly string _jsonFilePath;
        private readonly IWebHostEnvironment _enviromental;

        public ZapisyController(IWebHostEnvironment enviromental)
        {
            _enviromental = enviromental;
            _jsonFilePath = Path.Combine(_enviromental.ContentRootPath, "Data", "Zapisy.json");
        }

        [HttpGet(Name = "GetZapisy")]
        public IActionResult GetZapisy()
        {
            List<Zapisy> ZapisSzkolenia = LoadZapisyFromJson();
            if (ZapisSzkolenia == null)
            {
                return NotFound();
            }

            return Ok(ZapisSzkolenia);
        }

        [HttpPost(Name = "AddZapis")]
        public IActionResult AddZapis(Zapisy zapisy)
        {
            List<Zapisy> ZapisSzkolenia = LoadZapisyFromJson() ?? new List<Zapisy>();
            ZapisSzkolenia.Add(zapisy);
            SaveZapisyToJson(ZapisSzkolenia);
            return Ok();
        }

        private List<Zapisy> LoadZapisyFromJson()
        {
            try
            {
                using (StreamReader reader = new StreamReader(_jsonFilePath))
                {
                    string json = reader.ReadToEnd();
                    return JsonSerializer.Deserialize<List<Zapisy>>(json);
                }
            }
            catch (IOException)
            {
                return null;
            }
        }

        private void SaveZapisyToJson(List<Zapisy> zapisSzkolenia)
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(zapisSzkolenia, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(_jsonFilePath, jsonString);
            }
            catch (IOException)
            {

            }
        }
    }
}

