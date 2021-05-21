using DBContext.Connect;
using DBContext.Models;
using MediaStudioService.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Services.audit
{
    public class AuditAccountService
    {
        private readonly MediaStudioContext postgres;
        private readonly AuditAccount auditAccount;

        public AuditAccountService(MediaStudioContext context)
        {
            postgres = context;
            auditAccount = new AuditAccount();
        }
        public void Add(LogOperaion action, string accountToEdit, string executorLogin, string oldValue = null, int? IdAccount = null)
        {
            auditAccount.Action = action.ToString();
            auditAccount.AccountToEdit = accountToEdit;
            auditAccount.ExecutorLogin = executorLogin;
            auditAccount.OldValue = oldValue;
            auditAccount.IdAccount = IdAccount;
            auditAccount.IsSuccessful = false;

            postgres.AuditAccount.Add(auditAccount);
            postgres.SaveChanges();
        }

        public void MarkSucces(int? IdAccount = null)
        {
            if (IdAccount != null)
            {
                auditAccount.IdAccount = IdAccount;
            }
            auditAccount.IsSuccessful = true;
            postgres.Update(auditAccount);
            postgres.SaveChanges();
        }
        public async Task<List<AuditAccount>> GetListAsync()
        {
            return await postgres
                .AuditAccount.OrderByDescending(a => a.TimeOperation)
                                .Take(100)
                                .ToListAsync(); ;
        }
    }
}
