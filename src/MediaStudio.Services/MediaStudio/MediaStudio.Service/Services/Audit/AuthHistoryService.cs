using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Core;
using MediaStudioService.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Services.audit
{
    public class AuthHistoryService
    {
        private readonly MediaStudioContext postgres;
        private readonly AuthHistory authHistory;

        public AuthHistoryService(MediaStudioContext context)
        {
            authHistory = new AuthHistory();
            postgres = context;
        }

        public void Add(LogOperaion action, ClientInformation clientInfo)
        {
            authHistory.Action = action.ToString();
            authHistory.ExecutorLogin = clientInfo.Login;
            authHistory.Ipv4 = clientInfo.IPv4;
            authHistory.NameDevice = clientInfo.NameDevice;
            authHistory.UserAgent = clientInfo.UserAgent;

            authHistory.IsSuccessful = false;
            postgres.AuthHistory.Add(authHistory);
            postgres.SaveChanges();
        }

        public void MarkSucces()
        {
            authHistory.IsSuccessful = true;
            postgres.Update(authHistory);
            postgres.SaveChanges();
        }

        public void MarkForseDeleteJWT(LogOperaion action)
        {
            authHistory.Action = action.ToString();
            postgres.Update(authHistory);
            postgres.SaveChanges();
        }

        public void AddLogin(string login)
        {
            authHistory.ExecutorLogin = login;
            postgres.Update(authHistory);
            postgres.SaveChanges();
        }

        public async Task<List<AuthHistory>> GetListAsync()
        {
            return await postgres.AuthHistory
                .OrderByDescending(a => a.TimeAction)
                                .Take(100)
                                .ToListAsync();
        }
    }
}
