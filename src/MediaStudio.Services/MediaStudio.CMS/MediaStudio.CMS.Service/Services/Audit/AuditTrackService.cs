using DBContext.Connect;
using DBContext.Models;
using MediaStudioService.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Services.audit
{
    public class AuditTrackService
    {
        private readonly MediaStudioContext postgres;
        private AuditTrack auditTrack;

        public AuditTrackService(MediaStudioContext context)
        {
            postgres = context;
        }

        public void Add(LogOperaion action, string nameTrack, string executorLogin, string oldValue = null, long? IdTrack = null)
        {
            auditTrack = new AuditTrack
            {
                Action = action.ToString(),
                NameTrack = nameTrack,
                ExecutorLogin = executorLogin,
                OldValue = oldValue,
                IdTrack = IdTrack,
                IsSuccessful = false
            };

            postgres.AuditTrack.Add(auditTrack);
            postgres.SaveChanges();
        }

        public void MarkSucces(long? IdTrack = null)
        {
            if (IdTrack != null)
            {
                auditTrack.IdTrack = IdTrack;
            }
            auditTrack.IsSuccessful = true;
            postgres.Update(auditTrack);
            postgres.SaveChanges();
        }

        public async Task<List<AuditTrack>> GetListAsync()
        {
            return await postgres.AuditTrack
                .OrderByDescending(a => a.TimeOperation)
                                .Take(100)
                                .ToListAsync();
        }
    }
}
