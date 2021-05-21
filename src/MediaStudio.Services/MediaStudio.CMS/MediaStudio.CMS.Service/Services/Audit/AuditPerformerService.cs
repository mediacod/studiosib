using DBContext.Connect;
using DBContext.Models;
using MediaStudioService.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Services.audit
{
    public class AuditPerformerService
    {
        private readonly MediaStudioContext postgres;
        private readonly AuditPerformer auditAccount;

        public AuditPerformerService(MediaStudioContext context)
        {
            postgres = context;
            auditAccount = new AuditPerformer();
        }
        public void Add(LogOperaion action, string namePerformer, string executorLogin, string oldValue = null, int? IdPerformer = null)
        {
            auditAccount.Action = action.ToString();
            auditAccount.NamePerformer = namePerformer;
            auditAccount.ExecutorLogin = executorLogin;
            auditAccount.OldValue = oldValue;
            auditAccount.IdPerformer = IdPerformer;
            auditAccount.IsSuccessful = false;

            postgres.AuditPerformer.Add(auditAccount);
            postgres.SaveChanges();
        }

        public void MarkSucces(int? IdPerformer = null)
        {
            if (IdPerformer != null)
            {
                auditAccount.IdPerformer = IdPerformer;
            }
            auditAccount.IsSuccessful = true;
            postgres.Update(auditAccount);
            postgres.SaveChanges();
        }
        public async Task<List<AuditPerformer>> GetListAsync()
        {
            return await postgres.AuditPerformer
                .OrderByDescending(a => a.TimeOperation)
                .Take(100)
                .ToListAsync();
        }
    }
}
