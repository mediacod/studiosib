using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudioService.Core.Enums;
using MediaStudioService.Models.PageModels;
using MediaStudioService.Services.audit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Services
{
    public class PropertiesServices
    {
        private readonly MediaStudioContext postgres;
        private readonly AuditPropertiesService audit;

        public PropertiesServices(MediaStudioContext context, AuditPropertiesService auditProp)
        {
            postgres = context;
            audit = auditProp;
        }

        public string Add(Properties prop, string executorLogin)
        {
            audit.Add(LogOperaion.Добавление, prop.Name, executorLogin);
            CheckGroupPropExists(prop.IdGroupProp);

            postgres.Add(prop);
            postgres.SaveChanges();

            audit.MarkSucces(prop.IdProp);
            return $"Свойство {prop.Name} успешно добавлено!";
        }

        public async Task<List<PageAvalibleGroupProp>> GetAvailableGroups(int idTypeAudio)
        {
            return await postgres.GroupPropToTypeAudio
                .Where(g => g.IdTypeAudio == idTypeAudio)
                .AsNoTracking()
                .Select(s => new PageAvalibleGroupProp
                {
                    Key = s.IdPk,
                    IdTypeAudio = s.IdTypeAudio,
                    IdGroupProp = s.IdGroupProp,
                    NameGroupProp = s.IdGroupPropNavigation.Name,
                    IsNecessary = s.IsNecessary,
                    AllowMultiselect = s.IdGroupPropNavigation.AllowMultiselect,
                })
                .ToListAsync();
        }

        public PagePropToGroup GetByGroup(int idGroupProp)
        {
            CheckGroupPropExists(idGroupProp);

            return postgres.GroupProperties
                .AsNoTracking()
                .Select(s => new PagePropToGroup
                {
                    IdGroupProp = s.IdGroupProp,
                    NameGroup = s.Name,
                    Properties = GetGroupProperties(s.IdGroupProp),
                })
                .Where(pr => pr.IdGroupProp == idGroupProp)
               .SingleOrDefault();
        }

        public IEnumerable<PagePropToGroup> GetAllPageProp()
        {
            return postgres.GroupProperties
                .AsNoTracking()
                .ToList()
                .Select(s => new PagePropToGroup
                {
                    IdGroupProp = s.IdGroupProp,
                    NameGroup = s.Name,
                    Properties = GetGroupProperties(s.IdGroupProp)
                });
        }

        private List<PageProperties> GetGroupProperties(int idGroupProp)
        {
            return postgres.Properties
                .AsNoTracking()
                .Where(w => w.IdGroupProp == idGroupProp)
                .Select(s => new PageProperties
                {
                    IdProp = s.IdProp,
                    key = s.IdProp,
                    Name = s.Name,
                    Colour = s.IdColourNavigation.Name,
                })
                .ToList();
        }

        public async Task<List<GroupProperties>> GetGroupList()
        {
            return await postgres.GroupProperties
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public string Rename(int idProp, string newName, string executorLogin)
        {
            CheckPropertiesExists(idProp);
            var prop = postgres.Properties.Find(idProp);
            audit.Add(LogOperaion.Изменение, prop.Name, executorLogin, $"{prop.Name} => {newName}", prop.IdProp);
            prop.Name = newName;
            postgres.SaveChanges();
            audit.MarkSucces();
            return $"Свойство удачно переименовано!";
        }

        public async Task<List<Colour>> AvailableColorAsync()
        {
            return await postgres.Colour
                .AsNoTracking()
                .OrderBy(o => o.Name)
                .ToListAsync();
        }

        public string UpdateColour(int idProp, int idColour, string executorLogin)
        {
            CheckPropertiesExists(idProp);
            CheckColourExists(idColour);

            var prop = postgres.Properties.Find(idProp);
            var oldColour = postgres.Colour.Find(prop.IdColour);
            var newColour = postgres.Colour.Find(idColour);

            audit.Add(LogOperaion.Изменение, prop.Name, executorLogin, $"{oldColour} => {newColour}", prop.IdProp);
            prop.IdColour = idColour;
            postgres.SaveChanges();
            audit.MarkSucces();

            return $"Цвет свойства успешно изменен!";
        }

        private void CheckPropertiesExists(int id)
        {
            if (!postgres.Properties.Any(e => e.IdProp == id))
                throw new MyNotFoundException($"Ошибка! В базе данных не найдено свойство с id {id}!");
        }
        private void CheckColourExists(int id)
        {
            if (!postgres.Colour.Any(e => e.IdColour == id))
                throw new MyNotFoundException($"Ошибка! В базе данных не найден цвет с id {id}!");
        }

        private void CheckGroupPropExists(int id)
        {
            if (!postgres.GroupProperties.Any(e => e.IdGroupProp == id))
                throw new MyNotFoundException($"Ошибка! В базе данных не найден тип свойства с id {id}!");
        }
    }
}
