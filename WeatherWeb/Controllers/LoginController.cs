using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherWeb.Models;

namespace WeatherWeb.Controllers
{
    public class LoginController : Controller
    {
        WeatherContext db = new WeatherContext();
        public LoginController(WeatherContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            Users user = db.Users.Where(u => u.Username.Equals(username) && u.Password.Equals(EncodePasswordToBase64(password))).FirstOrDefault();
            if (user != null)
            {
                HttpContext.Session.SetString("LoggedInUser", user.Username);
                TempData["UsernameAsTempData"] = user.Username;
                return RedirectToAction("Index", "FavoriteCities");
            }
            else
            {
                ViewBag.Error = "Incorect Details";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

    }
}
