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
                ECRtiFramwork();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Hello, World!");
            Console.ReadLine();
        }

        static string ipLocal = "10.0.0.19", portNumberLocal = "2018", ipRemota = "10.0.0.122", portNumberRemote = "7060";
        static int timeOut = 180000;
        private static void ECRtiFramwork()
        {
            Core core = new();

            Console.WriteLine(JsonConvert.DeserializeObject<CardnetResult>(core.SetLocalEndPoint(ipLocal, int.Parse(portNumberLocal))));

            Console.WriteLine(JsonConvert.DeserializeObject<CardnetResult>(core.SetRemoteEndPoint(ipRemota, int.Parse(portNumberRemote))));
            Console.WriteLine(JsonConvert.DeserializeObject<CardnetResult>(core.SetTimeout(timeOut)));

            Console.WriteLine(JsonConvert.DeserializeObject<CardnetResult>(core.Initialice()));

            Console.WriteLine(JsonConvert.DeserializeObject<CardnetResult>(core.ProcessNormalSale(20000, 0, 0, 100)));
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
        public string Status { get; set; }
        public required string[] Messages { get; set; }
        public Host Host { get; set; }
        public Mode Mode { get; set; }
        public Card Card { get; set; }
        public Transaction Transaction { get; set; }
        public Dynamiccurrencyconversion DynamicCurrencyConversion { get; set; }
        public int Batch { get; set; }
        public string TerminalID { get; set; }
        public string MerchantID { get; set; }
        public string Acquired { get; set; }
        public object Reserved { get; set; }
        public object Signature { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class Host
    {
        public int Value { get; set; }
        public string Description { get; set; }
    }

    public class Mode
    {
        public string Value { get; set; }
        public string Description { get; set; }
    }

    public class Card
    {
        public string Product { get; set; }
        public string CardNumber { get; set; }
        public string HolderName { get; set; }
    }

    public class Transaction
    {
        public string AuthorizationNumber { get; set; }
        public int Reference { get; set; }
        public long RetrievalReference { get; set; }
        public string DataTime { get; set; }
        public string ApplicationIdentifier { get; set; }
        public object LoyaltyDeferredNumber { get; set; }
        public object TID { get; set; }
    }

    public class Dynamiccurrencyconversion
    {
        public object SalesIndicator { get; set; }
        public object CalculationAccepted { get; set; }
        public int MarginRate { get; set; }
        public int Amount { get; set; }
        public int DisplayRate { get; set; }
        public object TransactionCurrency { get; set; }
    }

}