using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Pomelo
{
    public static class IPUtility
    {
        #region Private Members
        /// <summary>
        /// Class A: 10.0.0.0-10.255.255.255
        /// </summary>
        static private long ipABegin, ipAEnd;
        /// <summary>
        /// Class B: 172.16.0.0-172.31.255.255   
        /// </summary>
        static private long ipBBegin, ipBEnd;
        /// <summary>
        /// Class C: 192.168.0.0-192.168.255.255
        /// </summary>
        static private long ipCBegin, ipCEnd;
        #endregion

        #region Constructors
        /// <summary>
        /// static new
        /// </summary>
        static IPUtility()
        {
            ipABegin = ConvertToNumber("10.0.0.0");
            ipAEnd = ConvertToNumber("10.255.255.255");

            ipBBegin = ConvertToNumber("172.16.0.0");
            ipBEnd = ConvertToNumber("172.31.255.255");

            ipCBegin = ConvertToNumber("192.168.0.0");
            ipCEnd = ConvertToNumber("192.168.255.255");
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Ip address convert to long
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        static public long ConvertToNumber(string ipAddress)
        {
            return ConvertToNumber(IPAddress.Parse(ipAddress));
        }
        /// <summary>
        /// Ip address convert to long
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        static public long ConvertToNumber(IPAddress ipAddress)
        {
            var bytes = ipAddress.GetAddressBytes();
            return bytes[0] * 256 * 256 * 256 + bytes[1] * 256 * 256 + bytes[2] * 256 + bytes[3];
        }
        /// <summary>
        /// Returns true if the IP is an intranet IP
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        static public bool IsIntranet(string ipAddress)
        {
            return IsIntranet(ConvertToNumber(ipAddress));
        }
        /// <summary>
        /// Returns true if the IP is an intranet IP
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        static public bool IsIntranet(IPAddress ipAddress)
        {
            return IsIntranet(ConvertToNumber(ipAddress));
        }
        /// <summary>
        /// Returns true if the IP is an intranet IP
        /// </summary>
        /// <param name="longIp"></param>
        /// <returns></returns>
        static public bool IsIntranet(long longIp)
        {
            return ((longIp >= ipABegin) && (longIp <= ipAEnd) ||
                    (longIp >= ipBBegin) && (longIp <= ipBEnd) ||
                    (longIp >= ipCBegin) && (longIp <= ipCEnd));
        }
        /// <summary>
        /// Get the local intranet IP
        /// </summary>
        /// <returns></returns>
        static public IPAddress? GetLocalIntranetIP()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
             .Select(p => p.GetIPProperties())
             .SelectMany(p => p.UnicastAddresses)
             .FirstOrDefault(p =>
                 p.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                 && !IPAddress.IsLoopback(p.Address)
                 && IsIntranet(p.Address)
             )?.Address;
        }
        /// <summary>
        /// Get the list of local intranet IPs
        /// </summary>
        /// <returns></returns>
        static public List<IPAddress> GetLocalIntranetIpList()
        {
            var infList = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
            .Select(p => p.GetIPProperties())
            .SelectMany(p => p.UnicastAddresses)
            .Where(p =>
                p.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                && !IPAddress.IsLoopback(p.Address)
                && IsIntranet(p.Address)
            );

            var result = new List<IPAddress>();
            foreach (var child in infList)
            {
                result.Add(child.Address);
            }

            return result;
        }
        #endregion
    }
}
