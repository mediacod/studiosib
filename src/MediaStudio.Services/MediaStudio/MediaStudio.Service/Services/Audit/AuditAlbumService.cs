using DBContext.Connect;
using DBContext.Models;
using MediaStudioService.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Services.audit
{
    public class AuditAlbumService
    {
        private readonly MediaStudioContext postgres;
        private readonly AuditAlbum auditAlbum;

        public AuditAlbumService(MediaStudioContext context)
        {
            postgres = context;
            auditAlbum = new AuditAlbum();
        }

        public void Add(LogOperaion action, string nameAlbum, string executorLogin, string oldValue = null, int? IdAlbum = null)
        {
            auditAlbum.Action = action.ToString();
            auditAlbum.NameAlbum = nameAlbum;
            auditAlbum.ExecutorLogin = executorLogin;
            auditAlbum.OldValue = oldValue;
            auditAlbum.IdAlbum = IdAlbum;
            auditAlbum.IsSuccessful = false;

            postgres.AuditAlbum.Add(auditAlbum);
            postgres.SaveChanges();
        }

        public void MarkSucces(int? IdAlbum = null)
        {
            if (IdAlbum != null)
            {
                auditAlbum.IdAlbum = IdAlbum;
            }
            auditAlbum.IsSuccessful = true;
            postgres.Update(auditAlbum);
            postgres.SaveChanges();
        }

        public async Task<List<AuditAlbum>> GetListAsync()
        {
            return await postgres.AuditAlbum
                .OrderByDescending(a => a.TimeOperation)
                                .Take(100)
                                .ToListAsync();
        }
    }
}
