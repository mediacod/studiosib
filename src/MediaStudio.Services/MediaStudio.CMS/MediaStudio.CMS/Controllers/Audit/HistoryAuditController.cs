namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DBContext.Models;
    using MediaStudioService.Core;
    using MediaStudioService.Services.audit;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("admin/[controller]")]
    [ApiController]
    public class HistoryAuditController : ControllerBase
    {
        private readonly AuditAccountService auditAccount;
        private readonly AuditAlbumService auditAlbum;
        private readonly AuditTrackService auditTrack;
        private readonly AuthHistoryService authHistory;
        private readonly AuditPropertiesService auditProp;
        private readonly AuditPerformerService auditPerform;


        public HistoryAuditController(AuditAccountService account, AuditAlbumService album,
            AuditTrackService track, AuthHistoryService auth, AuditPropertiesService prop,
            AuditPerformerService perform)
        {
            authHistory = auth;
            auditTrack = track;
            auditAccount = account;
            auditAlbum = album;
            auditProp = prop;
            auditPerform = perform;
        }

        [Authorize(Policy = Policy.AuditViewing)]
        [HttpGet("Performer")]
        public async Task<List<AuditPerformer>> GetAuditPerform()
        {
            return await auditPerform.GetListAsync();
        }

        [Authorize(Policy = Policy.AuditViewing)]
        [HttpGet("Property")]
        public async Task<List<AuditProperties>> GetAuditProp()
        {
            return await auditProp.GetListAsync();
        }

        [Authorize(Policy = Policy.AuditViewing)]
        [HttpGet("Album")]
        public async Task<List<AuditAlbum>> GetAuditAlbum()
        {
            return await auditAlbum.GetListAsync();
        }

        [Authorize(Policy = Policy.AuditViewing)]
        [HttpGet("Track")]
        public async Task<List<AuditTrack>> GetAuditTrack()
        {
            return await auditTrack.GetListAsync();
        }

        [Authorize(Policy = Policy.AuditViewing)]
        [HttpGet("Account")]
        public async Task<List<AuditAccount>> GetAuditAccount()
        {
            return await auditAccount.GetListAsync();
        }

        [Authorize(Policy = Policy.AuditViewing)]
        [HttpGet("Auth")]
        public async Task<List<AuthHistory>> GetAuthHistory()
        {
            return await authHistory.GetListAsync();
        }
    }
}