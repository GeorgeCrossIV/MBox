using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MBox.Controllers
{
    public class GraphqlController : Controller
    {
        private IConfiguration _config;
        private readonly IWebHostEnvironment _host;
        private string _Token = "";

        public GraphqlController(IWebHostEnvironment host, IConfiguration config)
        {
            _config = config;
            _host = host;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View(Services.Astra.GetUsers(_config, GetToken()));
        }

        public IActionResult Tags()
        {
            ViewBag.Filter = "";
            return View(Services.Astra.GetTags(_config, GetToken()));
        }
        public IActionResult Uploads()
        {
            ViewBag.Filter = "";
            return View(Services.Astra.GetUploads(_config, GetToken()));
        }
        public IActionResult UploadsByUserId(string id)
        {
            ViewBag.Filter = string.Format("Uploads by UserId: {0}", id);
            return View("Uploads", Services.Astra.GetUploadsByUserId(_config, GetToken(), id));
        }
        public IActionResult TagsByUploadId(string id)
        {
            ViewBag.Filter = string.Format("Tags by UploadId: {0}", id);
            return View("Tags",Services.Astra.GetTagsByUploadId(_config, GetToken(), id));
        }

        public IActionResult ApacheLogs()
        {
            return View(Services.Astra.GetApacheLogs(_config, GetToken()));
        }

        private string GetToken()
        {
            if (_Token.Length == 0)
            {
                var authUrl = _config.GetSection("Astra").GetSection("AuthUrl").Value;
                var username = _config.GetSection("Astra").GetSection("Username").Value;
                var password = _config.GetSection("Astra").GetSection("Password").Value;

                _Token = Services.Astra.GetToken(authUrl, username, password);
            }

            return _Token;
        }

    }
}
