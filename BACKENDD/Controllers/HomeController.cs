using BACKENDD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BACKENDD.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactService _contactService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IContactService contactService, ILogger<HomeController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Главая страница загружена.");
            return View();
        }

        public IActionResult NewTABBB()
        {
            _logger.LogDebug("Загрузка странцы NewTABBB.");
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Загружена страница  конфид.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Check(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool success = await _contactService.SaveContactAsync(contact);
                    if (success)
                    {
                        _logger.LogInformation("Контакт  сохранен.");
                        return RedirectToAction("ShowContacts");
                    }
                    else
                    {
                        _logger.LogError("Ошибка сохранении контакта.");
                        ModelState.AddModelError("", "Ошибка сохранении данных.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Ошибка при сохранении: {ex.Message}");
                }
            }

            return View("Index");
        }

        public IActionResult ShowContacts()
        {
            var contacts = _contactService.GetAllContacts();
            _logger.LogInformation("Отображение всех контактов.");
            return View(contacts);
        }
    }
}
