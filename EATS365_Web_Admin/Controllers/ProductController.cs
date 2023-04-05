using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using EATS365_Library.DTO;
using EATS365_Library.Paging;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EATS365_Web_Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _client = null;
        private string _productApiUrl;
        public ProductController()
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
            _productApiUrl = config["ApiUrls:ProductApiUrl"];
        }
        // GET: ProductController
        public async Task<ActionResult> Index(string searchTitle, string searchOrder, string searchName, string searchCategory, int? pageNumber = 1)
        {
            HttpResponseMessage response = await _client.GetAsync(_productApiUrl + "?searchTitle=" + searchTitle
                + "&searchOrder=" + searchOrder + "&searchName=" + searchName + "&searchCategory=" + searchCategory + "&pageNumber=" + pageNumber);
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            PagingReponse apiResponse = JsonSerializer.Deserialize<PagingReponse>(stringData, options);

            if (apiResponse.Success)
            {
                string json = apiResponse.List.ToString();
                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ConstructorHandling = Newtonsoft.Json.ConstructorHandling.AllowNonPublicDefaultConstructor
                };
                PaginatedList<ProductDTO> listProducts = Newtonsoft.Json.JsonConvert.DeserializeObject<PaginatedList<ProductDTO>>(json, settings);
                listProducts.TotalPages = apiResponse.TotalPages;
                listProducts.PageIndex = apiResponse.PageIndex;

                List<SelectListItem> categories = new List<SelectListItem>
                {
                    new SelectListItem { Value = "FOOD", Text = "Food" },
                    new SelectListItem { Value = "DRINK", Text = "Drink" },
                    new SelectListItem { Value = "SOUP", Text = "Soup" },
                    new SelectListItem { Value = "COMBO", Text = "Combo" }
                };

                ViewBag.CatId = categories;
                ViewBag.SelectedCategory = searchCategory;
                ViewBag.SearchOrder = searchOrder;
                ViewBag.TotalPage = listProducts.TotalPages;
                ViewBag.PageIndex = listProducts.PageIndex;
                ViewBag.PageSize = 10;
                ViewBag.SearchTitle = searchTitle;
                ViewBag.SearchName = searchName;
                return View(listProducts);
            }

            return View();
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
