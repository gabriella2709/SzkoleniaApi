using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SzkoleniaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SzkoleniaController : Controller
    {
        private readonly string _jsonFilePath;
        private readonly IWebHostEnvironment _environment;

        public SzkoleniaController(IWebHostEnvironment environment)
        {
            _environment = environment;
            _jsonFilePath = Path.Combine(_environment.ContentRootPath, "Data", "Szkolenia.json");
        }

        [HttpGet(Name = "GetSzkolenia")]
        public IActionResult GetSzkolenia()
        {
            List<Szkolenia> szkolenia = LoadSzkoleniaFromJson();
            if (szkolenia == null)
            {
                return NotFound();
            }

            return Ok(szkolenia);
        }

        [HttpPost(Name = "AddSzkolenie")]
        public IActionResult AddSzkolenie(Szkolenia szkolenie)
        {
            List<Szkolenia> szkolenia = LoadSzkoleniaFromJson() ?? new List<Szkolenia>();
            szkolenia.Add(szkolenie);
            SaveSzkoleniaToJson(szkolenia);
            return Ok();
        }

        private List<Szkolenia> LoadSzkoleniaFromJson()
        {
            try
            {
                using (StreamReader reader = new StreamReader(_jsonFilePath))
                {
                    string json = reader.ReadToEnd();
                    return JsonSerializer.Deserialize<List<Szkolenia>>(json);
                }
            }
            catch (IOException)
            {
                return null;
            }
        }

        private void SaveSzkoleniaToJson(List<Szkolenia> szkolenia)
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(szkolenia, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(_jsonFilePath, jsonString);
            }
            catch (IOException)
            {

            }
        }
    }
}

