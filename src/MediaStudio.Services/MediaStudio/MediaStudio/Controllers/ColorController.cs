namespace MediaStudio.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DBContext.Models;
    using MediaStudioService.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly ColourService colourService;

        public ColorController(ColourService _colourService)
        {
            colourService = _colourService ?? throw new ArgumentNullException(nameof(ColourService));
        }

        /// <summary>
        /// Получить список всех цветов.
        /// </summary>
        /// <returns>Возвращает список цветов.</returns>
        [HttpGet]
        public async Task<List<Colour>> Get()
        {
            return await colourService.GetListAsync();
        }

        /// <summary>
        /// Получить цвет по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор цвета.</param>
        /// <returns>Возвращает цвет.</returns>
        [HttpGet("{id}")]
        public Colour Get(int id)
        {
            return colourService.GetById(id);
        }
    }
}
