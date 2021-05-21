using DBContext.Connect;
using DBContext.Models;
using MediaStudioService.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Services.audit
{
    public class AuditPropertiesService
    {
        private readonly MediaStudioContext postgres;
        private readonly AuditProperties auditProp;

        public AuditPropertiesService(MediaStudioContext context)
        {
            postgres = context;
            auditProp = new AuditProperties();
        }

        public void Add(LogOperaion action, string nameProp, string executorLogin, string oldValue = null, int? idProp = null)
        {
            auditProp.Action = action.ToString();
            auditProp.NameProp = nameProp;
            auditProp.ExecutorLogin = executorLogin;
            auditProp.OldValue = oldValue;
            auditProp.IdProp = idProp;
            auditProp.IsSuccessful = false;

            postgres.AuditProperties.Add(auditProp);
            postgres.SaveChanges();
        }

        public void MarkSucces(int? idProp = null)
        {
            if (idProp != null)
            {
                auditProp.IdProp = idProp;
            }
            auditProp.IsSuccessful = true;
            postgres.Update(auditProp);
            postgres.SaveChanges();
        }
        public async Task<List<AuditProperties>> GetListAsync()
        {
            return await postgres.AuditProperties.
                OrderByDescending(a => a.TimeOperation)
                                .Take(100)
                                .ToListAsync();
        }
    }
}
