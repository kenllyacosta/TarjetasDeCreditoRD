using ECRti;
using ECRti.Dtos;
using ECRti.Framework;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using static ECRti.Dtos.TransactionDto;
using static ECRti.Settings;

namespace CarnetConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var ips = GetLocalIPAdressV4Object();

                foreach (var item in ips)
                    Console.WriteLine(item.ToString());

                Console.ReadLine();

                Ethernet ethernet = new(new IPEndPoint(ips[6], int.Parse(portNumberLocal)), new IPEndPoint(long.Parse(ipRemota.Replace(".", "")), int.Parse(portNumberRemote)), timeOut);
                Console.WriteLine("Ethernet:");
                
                Console.ReadLine();

                EthernetCommunication ethernetCommunication = new(ethernet);
                Console.WriteLine("EthernetCommunication");
                
                Console.ReadLine();

                var transaction = new TransactionDto
                {
                    TransactionType = TransactionTypes.NormalSale,
                    MultiMerchantID = 0,
                    MultiAcquirerID = 0,
                    TransactionAmount = 100,
                    TransactionITBIS = 0,
                    TransactionOtherTaxes = 0,
                    AuthorizationNumber = "",
                    Host = CardDto.Hosts.Credit,
                    ReferenceNumber = 0
                };
                Console.WriteLine("TransactionDto");
                
                Console.ReadLine();

                var tr = ethernetCommunication.AuthorizeTransaction(ECRti.Dtos.TransactionDto.TransactionTypes.NormalSale, transaction);
                Console.WriteLine("Tr");
                
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Hello, World!");
            Console.ReadLine();
        }

        static string ipLocal = "192.168.8.10", portNumberLocal = "2018", ipRemota = "192.168.8.150", portNumberRemote = "7060";
        static int timeOut = 180000;
        private static void ECRtiFramwork()
        {
            Core core = new();

            Console.WriteLine(core.SetLocalEndPoint(ipLocal, int.Parse(portNumberLocal)));
            Console.WriteLine(core.SetRemoteEndPoint(ipRemota, int.Parse(portNumberRemote)));
            Console.WriteLine(core.SetTimeout(timeOut));

            Console.WriteLine(core.Initialice());

            Console.WriteLine(core.ProcessNormalSale(20000, 0, 0, 100));
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

        private static IPAddress[] GetLocalIPAdressV4Object()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            var ipAddressV4 = new List<string>();

            return ipHostInfo.AddressList;
        }
    }

    public class CardnetResult
    {
        public required string Status { get; set; }
        public required string[] Messages { get; set; }
    }
}