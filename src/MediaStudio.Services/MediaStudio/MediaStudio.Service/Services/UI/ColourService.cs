using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudioService.Core.Enums;
using MediaStudioService.Models.Input;
using MediaStudioService.Service.ResourceService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Services
{
    public class ColourService
    {
        private readonly MediaStudioContext _postgres;

        public ColourService(MediaStudioContext postgres)
        {
            _postgres = postgres;
        }

        public async Task<List<Colour>> GetListAsync()
        {
            return await _postgres.Colour
                .AsNoTracking()
                .ToListAsync();
        }

        public Colour GetById(int idColour)
        {
            CheckColourExists(idColour);
            return _postgres.Colour
                .AsNoTracking()
                .Where(colour => colour.IdColour == idColour)
                .FirstOrDefault();
        }
   
        private void CheckColourExists(int id)
        {
            if (!_postgres.Colour.Any(e => e.IdColour == id))
                throw new MyNotFoundException($"Ошибка! В базе данных отсуствует Colour с id {id}!");
        }
    }
}
