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
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

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
                string jwtToken = apiResponse.Data.ToString();

                // Create a token handler
                var tokenHandler = new JwtSecurityTokenHandler();

                // Read the token
                var token = tokenHandler.ReadJwtToken(jwtToken);

                // Get the claims
                var claims = token.Claims;

                // Get the value of the 'unique_name' claim
                var accountName = claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;

                // Get the value of the 'email' claim
                var accountEmail = claims.FirstOrDefault(c => c.Type == "email")?.Value;

                // Get the value of the 'ID' claim
                var accountID = claims.FirstOrDefault(c => c.Type == "ID")?.Value;

                // Get the value of the 'TokenID' claim
                var tokenId = claims.FirstOrDefault(c => c.Type == "TokenID")?.Value;

                AccountDTO account = new AccountDTO
                {
                    AccountId = accountID,
                    AccountEmail = accountEmail,
                    AccountName = accountName
                };

                if (account != null && account.AccountId.Equals("AD0001"))
                {
                    HttpContext.Session.SetString("AccountID", account.AccountId);
                    HttpContext.Session.SetString("AccountName", account.AccountName);
                    HttpContext.Session.SetString("Token", jwtToken);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Tài khoản của bạn không có quyền truy cập vào hệ thống!";
                    return RedirectToAction("Index", "Login");
                }
            }

            TempData["ErrorMessage"] = apiResponse.UserMessage;
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
