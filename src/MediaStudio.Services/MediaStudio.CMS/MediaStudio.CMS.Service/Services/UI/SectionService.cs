using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudioService.Core.Enums;
using MediaStudioService.Models.PageModels;
using MediaStudioService.Models.PageModels.AdminPage;
using MediaStudioService.Service.ResourceService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Services
{
    public class SectionService
    {
        private readonly MediaStudioContext postgres;
        private readonly TrackMinioService _minioService;

        public SectionService(MediaStudioContext context, TrackMinioService minioService)
        {
            postgres = context;
            _minioService = minioService;
        }

        public async Task<AdminClientSection> GeSectionAsync(int idSection)
        {
            CheckSectionExists(idSection);

            var playlistCells = await GetPlaylistCellsAsync(idSection);
            var albumCells = await GetAlbumCellAsync(idSection);

            return new AdminClientSection
            {
                IdPage = postgres.PageSection
                .Where(pageSection => pageSection.IdSection == idSection)
                .Select(pageSection => pageSection.IdPage)
                .FirstOrDefault(),

                NameSection = postgres.Section.Find(idSection).Name,

                Cells = playlistCells.Union(albumCells),
            };
        }

        private async Task<List<ClientSectionCell>> GetPlaylistCellsAsync(int idSection)
        {
            return await postgres.SectionPlaylist.AsNoTracking()
                .Where(section => section.IdSection == idSection && section.IdPlaylistNavigation.IsPublic.Value)
                .OrderByDescending(section => section.IdSectionPlaylist)
                .Select(sectionPlaylist => new ClientSectionCell
                {
                    IdObject = sectionPlaylist.IdSectionPlaylist,
                    IdTypeCell = (short)CellTypes.Playlist,
                    Name = sectionPlaylist.IdPlaylistNavigation.Name,
                    ColorCode = sectionPlaylist.IdPlaylistNavigation.IdColourNavigation.Code,
                    CountTrack = sectionPlaylist.IdPlaylistNavigation.TrackToPlaylist
                      .Where(w => w.IdPlaylist == sectionPlaylist.IdPlaylist).Count(),
                    LinkCover = _minioService.MinioURL + sectionPlaylist.IdPlaylistNavigation.PlaylistStorage
                      .Where(playlistStorage => playlistStorage.IdStorageNavigation.IdBucket == (int)BucketTypes.playliscovermedium)
                       .Select(playlistStorage => playlistStorage.IdStorageNavigation.StaticUrl)
                       .FirstOrDefault(),
                })
                .ToListAsync();
        }

        private async Task<List<ClientSectionCell>> GetAlbumCellAsync(int idSection)
        {
            return await postgres.SectionAlbum
                .AsNoTracking()
                .Where(section => section.IdSection == idSection
                && !section.IdAlbumNavigation.IsDelete
                && section.IdAlbumNavigation.IsChecked
                && section.IdAlbumNavigation.PublicationTime < DateTime.Now)
                .OrderByDescending(section => section.IdSectionAlbum)
                .Select(sectionAlbum => new ClientSectionCell
                {
                    IdObject = sectionAlbum.IdSectionAlbum,
                    IdTypeCell = (short)CellTypes.Album,
                    Name = sectionAlbum.IdAlbumNavigation.Name,
                    CountTrack = sectionAlbum.IdAlbumNavigation.TrackToAlbum
                        .Where(trackAlbum => trackAlbum.IdAlbum == sectionAlbum.IdAlbum).Count(),
                    LinkCover = _minioService.MinioURL + sectionAlbum.IdAlbumNavigation.AlbumStorage
                        .Where(albumStorage => albumStorage.IdStorageNavigation.IdBucket == (int)BucketTypes.albumcovermedium)
                        .Select(albumStorage => albumStorage.IdStorageNavigation.StaticUrl)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }


        public async Task<List<AdminSection>> GetListSection()
        {
            return await postgres.Section
                .OrderBy(section => section.Name)
                .AsNoTracking()
                .Select(section => new AdminSection
                {
                    IdSection = section.IdSection,
                    NameSection = section.Name,
                    Playlists = section.SectionPlaylist.Where(sectionPlaylist => sectionPlaylist.IdSection == section.IdSection).ToList(),
                    Albums = section.SectionAlbum.Where(sectionAlbum => sectionAlbum.IdSection == section.IdSection).ToList(),
                })
                .ToListAsync();
        }

        public int AddSection(Section newSection)
        {
            postgres.Section.Add(newSection);
            postgres.SaveChanges();
            return newSection.IdSection;
        }

        public string Rename(int idSection, string newName)
        {
            CheckSectionExists(idSection);

            var section = postgres.Section.Find(idSection);
            section.Name = newName;
            postgres.SaveChanges();

            return "Имя секции успешно изменено";
        }

        public async Task<string> DeleteSectionAsync(int idSection)
        {
            CheckSectionExists(idSection);

            var section = postgres.Section.Find(idSection);

            var sectionPlaylists = postgres.SectionPlaylist
                .Where(sectionPlaylist => sectionPlaylist.IdSection == idSection)
                .AsNoTracking();

            var sectionAlbums = postgres.SectionAlbum
                .Where(sectionAlbum => sectionAlbum.IdSection == idSection)
                .AsNoTracking();

            var pageSection = postgres.PageSection
                .Where(ageSection => ageSection.IdSection == idSection)
                .AsNoTracking();

            postgres.SectionPlaylist.RemoveRange(sectionPlaylists.AsEnumerable());
            postgres.SectionAlbum.RemoveRange(sectionAlbums.AsEnumerable());
            postgres.PageSection.RemoveRange(pageSection.AsEnumerable());
            await postgres.SaveChangesAsync();

            postgres.Section.Remove(section);
            await postgres.SaveChangesAsync();

            return "Cекция успешно удалена";
        }

        public async Task<string> DeleteAlbumAsync(int idSectionAlbum)
        {
            CheckSectionAlbum(idSectionAlbum);
            var albumToSection = await postgres.SectionAlbum.FindAsync(idSectionAlbum);

            postgres.SectionAlbum.Remove(albumToSection);
            await postgres.SaveChangesAsync();

            return "Альбом из секции успешно удален";
        }

        public string DeletePlaylist(int idSectionPlaylist)
        {
            CheckSectionPlaylist(idSectionPlaylist);
            var playlistToSection = postgres.SectionPlaylist.Find(idSectionPlaylist);
            postgres.SectionPlaylist.Remove(playlistToSection);
            postgres.SaveChanges();

            return "Альбом из секции успешно удален";
        }

        public string AddAlbums(List<SectionAlbum> sectionAlbums)
        {
            foreach (var sectionAlbum in sectionAlbums)
            {
                AddAlbum(sectionAlbum);
            }
            return "Альбомы успешно добавлены в секцию";
        }

        public string AddAlbum(SectionAlbum sectionAlbum)
        {
            CheckAlbumExists(sectionAlbum.IdAlbum);
            CheckSectionExists(sectionAlbum.IdSection);

            postgres.SectionAlbum.Add(sectionAlbum);
            postgres.SaveChanges();
            return "Альбом успешно добавлен в секцию";
        }
        public string AddPlaylist(SectionPlaylist sectionPlaylist)
        {
            CheckPlaylistExists(sectionPlaylist.IdPlaylist);
            CheckSectionExists(sectionPlaylist.IdSection);

            postgres.SectionPlaylist.Add(sectionPlaylist);
            postgres.SaveChanges();
            return "Плейлист успешно добавлен в секцию";
        }

        public string AddPlaylists(List<SectionPlaylist> sectionPlaylists)
        {
            foreach (var sectionPlaylist in sectionPlaylists)
            {
                AddPlaylist(sectionPlaylist);
            }
            return "Плейлисты успешно добавлены в секцию";
        }

        private void CheckSectionPlaylist(int id)
        {
            if (!postgres.SectionPlaylist.Any(e => e.IdSectionPlaylist == id))
                throw new MyNotFoundException($"Ошибка! В базе данных не найден SectionPlaylist с id {id}!");
        }

        private void CheckSectionAlbum(int id)
        {
            if (!postgres.SectionAlbum.Any(e => e.IdSectionAlbum == id))
                throw new MyNotFoundException($"Ошибка! В базе данных не найден SectionAlbum с id {id}!");
        }

        private void CheckAlbumExists(int id)
        {
            if (!postgres.Album.Any(e => e.IdAlbum == id))
                throw new MyNotFoundException($"Ошибка! В базе данных не найден альбом с id {id}!");
        }

        private void CheckPlaylistExists(long id)
        {
            if (!postgres.Playlist.Any(e => e.IdPlaylist == id))
                throw new MyNotFoundException($"Ошибка! В базе данных не найден плейлист с id {id}!");
        }

        private void CheckSectionExists(int id)
        {
            if (!postgres.Section.Any(e => e.IdSection == id))
                throw new MyNotFoundException($"Ошибка! В базе данных не найдена секция с id {id}!");
        }
    }
}
