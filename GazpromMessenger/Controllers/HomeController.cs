using GazpromMessenger.Helpers;
using GazpromMessenger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace GazpromMessenger.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext db;

        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IConfiguration configuration)
        {
            this.configuration = configuration;
            db = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new MessageCombinedViewModel();
            model.Display = db.Messages.Select(s => new MessageDisplayViewModel
            {
                CreateTime = s.CreateTime,
                Description = s.Description,
                TaskID = s.TaskID
            }).ToList();

            model.Create = new MessageCreateViewModel();

            return View(model);
        }

        public IActionResult Display(MessageDisplayViewModel model)
        {
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MessageCreateViewModel model)
        {
            bool success = false;
            using (MSMQManager msmq = new MSMQManager(configuration))
            {
                success = msmq.SendMessage(model.Description);
                
            }
            return Json(success);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
