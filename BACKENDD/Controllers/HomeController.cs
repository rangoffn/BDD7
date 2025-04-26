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
            _logger.LogInformation("������ �������� ���������.");
            return View();
        }

        public IActionResult NewTABBB()
        {
            _logger.LogDebug("�������� ������� NewTABBB.");
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("��������� ��������  ������.");
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
                        _logger.LogInformation("�������  ��������.");
                        return RedirectToAction("ShowContacts");
                    }
                    else
                    {
                        _logger.LogError("������ ���������� ��������.");
                        ModelState.AddModelError("", "������ ���������� ������.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"������ ��� ����������: {ex.Message}");
                }
            }

            return View("Index");
        }

        public IActionResult ShowContacts()
        {
            var contacts = _contactService.GetAllContacts();
            _logger.LogInformation("����������� ���� ���������.");
            return View(contacts);
        }
    }
}
