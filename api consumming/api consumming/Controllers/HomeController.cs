using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using api_consumming.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace api_consumming.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(IFormCollection tk)
        {
            string temp = tk["id"] + ":" + tk["pass"];
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://demo-sinvoice.viettel.vn:8443/InvoiceAPI/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(temp)); //("Username:Password")  
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
            var response = await client.GetAsync(@"InvoiceWS/getCustomFields");
            if (response.IsSuccessStatusCode)
            {
                ViewBag.alo = "Đăng nhâp thành công";
                return View();
            }
            else
            {
                ViewBag.alo = "Đăng nhập lỗi";
                return View();
            }
        }
        
public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
