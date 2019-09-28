using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NeedAidLogging
{
    public static class CommonWebUtility
    {
        public static string GetRequestIp(HttpRequest request = null)
        {
            //if request is null, get current request
            if (request == null)
                request = HttpContext.Current?.Request;
            //if request is still null then unable to get request from current context
            if (request == null)
                return string.Empty;

            var ip = string.Empty;

            try
            {
                //find by HTTP_CLIENT_IP
                var httpClientIp = request.ServerVariables["HTTP_CLIENT_IP"];
                if (!string.IsNullOrEmpty(httpClientIp))
                {
                    ip = httpClientIp;
                }

                //FIND BY HTTP_X_FORWARD if PROXY IS INVOLCED
                if (string.IsNullOrEmpty(ip))
                {

                    var httpXForward = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    ip = !string.IsNullOrEmpty(httpXForward) && httpXForward?.ToLower() != "unknown"
                        ? httpXForward
                        : request.ServerVariables["REMOTE_ADDR"];

                    if (!string.IsNullOrEmpty(ip) && ip.Contains(","))
                        ip = ip.Split(',').First();
                }

                //FIND BY UserHostAddress
                if (string.IsNullOrEmpty(ip))
                    ip = request?.UserHostAddress ?? "unidentified ip";
            }
            catch (Exception ex)
            {

                //ignore exception
                //TODO: handle gracefully
                ip = "unknown ip";
            }

            return ip?.Trim();
        }

        public static string GetHostname(string ipAddress = "", HttpRequest request = null)
        {
            var hostName = string.Empty;
            ipAddress = string.IsNullOrEmpty(ipAddress) ? GetRequestIp(request) : ipAddress;

            //if request is still null then unable to get request from current context
            if (string.IsNullOrEmpty(ipAddress))
                return string.Empty;

            if (!string.IsNullOrEmpty(ipAddress))
            {
                try
                {
                    var hostEntry = Dns.GetHostEntry(ipAddress);
                    hostName = hostEntry?.HostName ?? string.Empty;
                }
                catch (Exception ex)
                {
                    //ignore exception
                    //TODO: handle gracefully
                    hostName = "unknown host";
                }
            }

            return hostName?.Trim();
        }

        public static string GetClientIpAddress(HttpRequestMessage request)
        {
            string HttpContext = "MS_HttpContext";
            const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            return null;
        }
    }
}
