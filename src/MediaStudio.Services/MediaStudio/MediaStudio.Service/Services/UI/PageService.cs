using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudioService.Core.Enums;
using MediaStudioService.Models.Input;
using MediaStudioService.Models.PageModels;
using MediaStudioService.Models.PageModels.AdminPage;
using MediaStudioService.Service.ResourceService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Services
{
    public class PageService
    {
        private readonly MediaStudioContext _postgres;
        private readonly TrackMinioService _minioService;

        public PageService(MediaStudioContext postgres, TrackMinioService minioService)
        {
            _postgres = postgres;
            _minioService = minioService;
        }

        public async Task<IEnumerable<ClientSection>> GetPageSectionsAsync(int idPage)
        {
            var pageAlbumstCells = await GetPageAlbumsAsync(idPage);
            var pagePlaylistCells = await GetPagePlaylistsAsync(idPage);

            var result = pagePlaylistCells.Union(pageAlbumstCells);
            return result;
        }

        public async Task<List<PageModel>> GetListAsync()
        {
            return await _postgres.Page
                .OrderBy(page => page.Name)
                .AsNoTracking()
                .Select(page => new PageModel
                {
                    IdPage = page.IdPage,
                    Name = page.Name,
                    PageSections = page.PageSection
                        .Where(pg => pg.IdPage == page.IdPage)
                        .Select(pg => new AdminPageSection
                        {
                            IdSection = pg.IdSection,
                            IdSectionPage = pg.IdSectionPage,
                            Name = pg.IdSectionNavigation.Name,
                            Order = pg.OrderSection,
                        }).ToList()
                })
                .ToListAsync();
        }

        public int AddSectionToPage(PageSection page)
        {
            CheckSectionExists(page.IdSection);
            CheckPageExists(page.IdPage);

            _postgres.PageSection.Add(page);
            _postgres.SaveChanges();
            return page.IdSectionPage;
        }

        public string AddSectionsToPage(List<PageSection> pageSections)
        {
            foreach (var sectionOrder in pageSections)
            {
                AddSectionToPage(sectionOrder);
            }
            return $"Секции успешно добавлены!";
        }

        public string UpdateOrders(List<InputSectionOrder> sectionOrders)
        {
            foreach (var sectionOrder in sectionOrders)
            {
                UpdateOrder(sectionOrder);
            }
            return $"Порядок секций успешно изменен";
        }

        public string UpdateOrder(InputSectionOrder sectionOrder)
        {
            CheckSectionInPageExists(sectionOrder.IdPageSection);

            var sectionPage = _postgres.PageSection.Find(sectionOrder.IdPageSection);
            sectionPage.OrderSection = sectionOrder.IdNewOrder;
            _postgres.Update(sectionPage);
            _postgres.SaveChanges();
            return $"Порядок секции успешно изменен";
        }

        public string DeleteSection(int idSectionPage)
        {
            CheckSectionInPageExists(idSectionPage);

            var sectionToPage = _postgres.PageSection.Find(idSectionPage);

            _postgres.PageSection.Remove(sectionToPage);
            _postgres.SaveChanges();
            return $"Секция успешно удалена из страницы";
        }

        private async Task<List<ClientSection>> GetPageAlbumsAsync(int idPage)
        {
            return await _postgres.PageSection
                .Where(pageSection => pageSection.IdPage == idPage && pageSection.IdSectionNavigation.SectionAlbum.Any())
                .AsNoTracking()
                 .Select(pageSection => new ClientSection
                 {
                     NameSection = pageSection.IdSectionNavigation.Name,

                     Cells = pageSection.IdSectionNavigation.SectionAlbum
                     .Where(sectionAlbum => sectionAlbum.IdAlbumNavigation.TrackToAlbum.Any()
                           && !sectionAlbum.IdAlbumNavigation.IsDelete)
                     .Select(sectionAlbum => new ClientSectionCell
                     {
                         IdObject = (long)sectionAlbum.IdAlbumNavigation.IdAlbum,
                         IdTypeCell = (short)CellTypes.Album,
                         OrderSection = pageSection.OrderSection,
                         Name = sectionAlbum.IdAlbumNavigation.Name,
                         CountTrack = sectionAlbum.IdAlbumNavigation.TrackToAlbum
                            .Where(w => w.IdAlbum == sectionAlbum.IdAlbum).Count(),
                         LinkCover = _minioService.MinioURL + sectionAlbum.IdAlbumNavigation.AlbumStorage
                            .Where(albumStorage => albumStorage.IdStorageNavigation.IdBucket == (int)BucketTypes.albumcovermedium
                                && albumStorage.IdAbumNavigation.IsChecked
                                && albumStorage.IdAbumNavigation.PublicationTime < DateTime.Now
                                && !albumStorage.IdAbumNavigation.IsDelete)
                                .Select(albumStorage => albumStorage.IdStorageNavigation.StaticUrl)
                                .FirstOrDefault(),
                     }),
                 }).ToListAsync();
        }

        private async Task<List<ClientSection>> GetPagePlaylistsAsync(int idPage)
        {
            return await _postgres.PageSection
                .Where(pageSection => pageSection.IdPage == idPage
                        && pageSection.IdSectionNavigation.SectionPlaylist.Any())
                .AsNoTracking()
                .Select(pageSection => new ClientSection
                {
                    NameSection = pageSection.IdSectionNavigation.Name,

                    Cells = pageSection.IdSectionNavigation.SectionPlaylist
                    .Where(sectionPlaylist => sectionPlaylist.IdPlaylistNavigation.TrackToPlaylist.Any())

                    .Select(sectionPlaylist => new ClientSectionCell
                    {
                        IdObject = sectionPlaylist.IdPlaylistNavigation.IdPlaylist,
                        IdTypeCell = (short)CellTypes.Playlist,
                        OrderSection = pageSection.OrderSection,
                        Name = sectionPlaylist.IdPlaylistNavigation.Name,
                        ColorCode = sectionPlaylist.IdPlaylistNavigation.IdColourNavigation.Code,
                        CountTrack = sectionPlaylist.IdPlaylistNavigation.TrackToPlaylist
                            .Where(w => w.IdPlaylist == sectionPlaylist.IdPlaylist).Count(),
                        LinkCover = _minioService.MinioURL + sectionPlaylist.IdPlaylistNavigation.PlaylistStorage
                         .Where(playlistStorage => playlistStorage.IdStorageNavigation.IdBucket == (int)BucketTypes.playliscovermedium
                         && playlistStorage.IdPlaylistNavigation.IsPublic.Value)
                         .Select(playlistStorage => playlistStorage.IdStorageNavigation.StaticUrl)
                         .FirstOrDefault(),
                    }),
                }).ToListAsync();
        }

        private void CheckSectionInPageExists(int id)
        {
            if (!_postgres.PageSection.Any(e => e.IdSectionPage == id))
                throw new MyNotFoundException($"Ошибка! В базе данных отсуствует IdSectionPage с id {id}!");
        }

        private void CheckPageExists(int id)
        {
            if (!_postgres.Page.Any(e => e.IdPage == id))
                throw new MyNotFoundException($"Ошибка! В базе данных отсуствует Page с id {id}!");
        }
        private void CheckSectionExists(int id)
        {
            if (!_postgres.Section.Any(e => e.IdSection == id))
                throw new MyNotFoundException($"Ошибка! В базе данных не найдена секция с id {id}!");
        }
    }
}
