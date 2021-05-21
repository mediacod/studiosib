using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UAParser;

namespace MediaStudio.Core
{
    public class ClientInformation
    {
        public string IPv4 { get; }
        public string UserAgent { get; }
        public string NameDevice { get; private set; } = null;
        public string Login { get; set; }

        private readonly IPAddress IPAddress;
        private readonly string defaultValue = "Не обнаружено";
        private ClientInfo cientInfo;

        public ClientInformation(HttpContext httpContext)
        {
            IPAddress = httpContext.Connection.RemoteIpAddress;

            IPv4 = IPAddress.MapToIPv4() == null
                ? defaultValue
                : IPAddress.MapToIPv4().ToString();

            UserAgent = httpContext.Request.Headers.ContainsKey("User-Agent")
                 ? ParseDeviceInfo(httpContext.Request)
                 : defaultValue;

            BildNameDevice();
        }

        private string ParseDeviceInfo(HttpRequest request)
        {
            var userAgent = request.Headers["User-Agent"];
            string uaString = Convert.ToString(userAgent[0]);
            var uaParser = Parser.GetDefault();
            cientInfo = uaParser.Parse(uaString);
            return $"{cientInfo.UserAgent.Family} - {cientInfo.OS.Family} {cientInfo.OS.Major} {cientInfo.OS.Minor}";
        }

        private void BildNameDevice()
        {
            try
            {
                IPHostEntry GetIPHost = Dns.GetHostEntry(IPAddress);
                List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
                NameDevice = compName.First();
            }
            catch
            {
                NameDevice = $"{cientInfo.Device.Family} {cientInfo.Device.Brand}";
            }
        }
    }
}
