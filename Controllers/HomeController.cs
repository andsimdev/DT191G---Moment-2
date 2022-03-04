using DT191G___Moment_2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DT191G___Moment_2.Controllers
{
    public class HomeController : Controller
    {
        // Inloggningssidan
        [HttpGet]
        public IActionResult Login()
        {
            // Skicka med dagens datum
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewData["Date"] = date;

            return View();
        }

        // Logga in användare (ta emot formulärdata)
        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
            // Skicka med dagens datum
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewData["Date"] = date;

            // Hämta användarnamn
            string username = form["username"];

            // Hämta användarnivå
            string userlevel = form["userlevel"];

            // Hämta användartyp
            string usertype = form["usertype"];

            // Skapa användarsträng för lagring i sessionsvariabel
            string user = username + ", " + userlevel + usertype;

            // Lagra användaren i sessionsvariabel
            HttpContext.Session.SetString("user", user);

            // Skicka vidare till startsidan
            return RedirectToAction("Index", "Home");
        }

        // Huvudsidan
        [Route("/startsida")]
        public IActionResult Index()
        {
            // Skicka med dagens datum
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewData["Date"] = date;

            // Hämta användare från sessionsvariabeln och skicka med
            ViewBag.user = HttpContext.Session.GetString("user");
            return View();
        }

        // Huvudsidan om formulär är medskickat
        // Huvudsidan
        [Route("/startsida")]
        [HttpPost]
        public IActionResult Index(Post post)
        {
            // Skicka med dagens datum
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewData["Date"] = date;

            // Hämta användare från sessionsvariabeln och skicka med
            ViewBag.user = HttpContext.Session.GetString("user");

            // Kontrollera om formuläret är korrekt ifyllt
            if (ModelState.IsValid)
            {
                // Skapa posten och lagra till json-fil
                // Hämta befintliga poster
                var JsonStr = System.IO.File.ReadAllText("posts.json");

                // Konvertera till lista med objekt
                var JsonObj = JsonConvert.DeserializeObject<List<Post>>(JsonStr);

                // Lägg till
                if (JsonObj != null)
                {
                    JsonObj.Add(post);
                }

                // Konvertera tillbaka till JSON-sträng, spara
                System.IO.File.WriteAllText("posts.json", JsonConvert.SerializeObject(JsonObj, Formatting.Indented));

                // Rensa formuläret
                ModelState.Clear();

                return View(post);
            }
            else
            {
                // Hämta användare från sessionsvariabeln och skicka med
                ViewBag.user = HttpContext.Session.GetString("user");
                return View();
            }
        }

        // Alla poster-sidan
        [Route("/poster")]
        public IActionResult Posts()
        {
            // Skicka med dagens datum
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewData["Date"] = date;

            // Hämta användare från sessionsvariabeln och skicka med
            ViewBag.user = HttpContext.Session.GetString("user");

            // Läs in alla poster från JSON-filen och gör om till lista
            var JsonStr = System.IO.File.ReadAllText("posts.json");
            var JsonObj = JsonConvert.DeserializeObject<List<Post>>(JsonStr);

            // Returnera vyn och skicka med listan med poster
            return View(JsonObj);
        }
    }
}