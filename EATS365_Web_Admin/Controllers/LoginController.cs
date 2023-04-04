using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using EATS365_Library.DTO;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;

namespace EATS365_Web_Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _client = null;
        private string _loginApiUrl;
        public LoginController()
        {
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(120); // set timeout to 2 minutes
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);
            GetApiUrls();
        }

        private void GetApiUrls()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
            _loginApiUrl = config["ApiUrls:LoginApiUrl"];
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([FromForm] LoginDTO loginDTO)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(_loginApiUrl, new LoginDTO { AccountEmail = loginDTO.AccountEmail, AccountPassword = loginDTO.AccountPassword });

            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Email hoặc password không đúng! Vui lòng đăng nhập lại!";
                return RedirectToAction("Index", "Login");
            }

            string stringData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            APIResponseDTO apiResponse = JsonSerializer.Deserialize<APIResponseDTO>(stringData, options);

            if (apiResponse.Success)
            {
                string json = apiResponse.Data.ToString();
                AccountDTO account = Newtonsoft.Json.JsonConvert.DeserializeObject<AccountDTO>(json);

                if (account != null && account.AccountId.Equals("AD0001"))
                {
                    HttpContext.Session.SetString("AccountID", account.AccountId);
                    HttpContext.Session.SetString("AccountName", account.AccountName);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Tài khoản của bạn không có quyền truy cập vào hệ thống!";
                    return RedirectToAction("Index", "Login");
                }
            }

            TempData["ErrorMessage"] = apiResponse.Message;
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
