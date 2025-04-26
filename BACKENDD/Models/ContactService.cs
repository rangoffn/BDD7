using BACKENDD.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace BACKENDD.Models
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ContactService> _logger;

        public ContactService(AppDbContext context, ILogger<ContactService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> SaveContactAsync(Contact contact)
        {
            try
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Контакт {contact.Name} {contact.SecName} сохранен в бд.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка сохр контакта: {ex.Message}");
                return false;
            }
        }

        public List<Contact> GetAllContacts()
        {
            var contacts = _context.Contacts.OrderBy(c => c.Id).ToList();
            _logger.LogInformation(" контакты загружены.");
            return contacts;
        }
    }
}
