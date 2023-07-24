using ECRti.Framework;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace CarnetConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ipLocal = "192.168.8.10", portNumberLocal = "2018", ipRemota = "192.168.8.150", portNumberRemote = "7060";                   
            int timeOut = 180000;
            Core core = new();

            Console.WriteLine(core.SetLocalEndPoint(ipLocal, int.Parse(portNumberLocal)));
            Console.WriteLine(core.SetRemoteEndPoint(ipRemota, int.Parse(portNumberRemote)));
            Console.WriteLine(core.SetTimeout(timeOut));

            Console.WriteLine(core.Initialice());

            Console.WriteLine(core.ProcessNormalSale(20000, 0, 0, 100));

            Console.WriteLine("Hello, World!");
            Console.ReadLine();
        }

        private static List<string> GetLocalIPAdressV4()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            var ipAddressV4 = new List<string>();

            foreach (var ip in ipHostInfo.AddressList)
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    ipAddressV4.Add(ip.ToString());

            return ipAddressV4;
        }
    }

    public class CardnetResult
    {
        public required string Status { get; set; }
        public required string[] Messages { get; set; }
    }
}