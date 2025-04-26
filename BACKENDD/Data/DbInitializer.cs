using BACKENDD.Models;
using Microsoft.Extensions.Logging;

namespace BACKENDD.Data
{
    // Преобразуем класс в нестатический
    public class DbInitializer
    {
        // Метод инициализации базы данных с логированием
        public void Initialize(AppDbContext context, ILogger<DbInitializer> logger)
        {
            // Если таблица Contacts не пуста, то выходим
            if (context.Contacts.Any())
            {
                logger.LogInformation("Таблица Contacts уже создана.");
                return;
            }

            // Иначе добавляем несколько начальных данных
            var contacts = new Contact[]
            {
                new Contact { Name = "Пример", SecName = "Пример", Age = 30, Email = "Пример@example.com", Message = "Пример!!!!" },
            };

            context.Contacts.AddRange(contacts);
            context.SaveChanges();
            logger.LogInformation("Контакты успешно добавлены в базу данных.");
        }
    }
}
