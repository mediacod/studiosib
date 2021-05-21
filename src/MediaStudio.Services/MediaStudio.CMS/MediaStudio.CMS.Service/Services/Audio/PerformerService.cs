using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudioService.Core.Enums;
using MediaStudioService.Services.audit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Services
{
    public class PerformerService
    {
        private readonly MediaStudioContext postgres;
        private readonly AuditPerformerService audit;

        public PerformerService(MediaStudioContext context, AuditPerformerService performerService)
        {
            postgres = context;
            audit = performerService;
        }

        public async Task<List<Performer>> GetListAsync()
        {
            return await postgres.Performer
                .OrderBy(a => a.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        public string Create(Performer performer, string executorLogin)
        {
            audit.Add(LogOperaion.Добавление, performer.Name, executorLogin);
            CheckPerformerExist(performer.Name);
            postgres.Performer.Add(performer);
            postgres.SaveChanges();
            audit.MarkSucces(performer.IdPerformer);
            return $"Исполнитель {performer.Name} успешно добавлен!";
        }

        public string Rename(int idPerformer, string newName, string executorLogin)
        {
            CheckPerformerExist(idPerformer);
            var performer = postgres.Performer.Find(idPerformer);

            audit.Add(LogOperaion.Изменение, newName, executorLogin, $"{performer.Name} => {newName}");

            performer.Name = newName;
            postgres.SaveChanges();

            audit.MarkSucces();
            return $"Исполнитель {performer.Name} успешно переименован!"; ;
        }

        private void CheckPerformerExist(int idPerformer)
        {
            if (!postgres.Performer.Any(e => e.IdPerformer == idPerformer))
                throw new MyNotFoundException($"Ошибка! В базе данных не найден исполнитель с id {idPerformer}!"); ;
        }

        private void CheckPerformerExist(string newName)
        {
            if (postgres.Performer.Any(e => e.Name == newName))
                throw new MyBadRequestException($"Исполнитель {newName} уже существует!");
        }
    }
}
