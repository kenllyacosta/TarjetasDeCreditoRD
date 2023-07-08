using ECRti.Framework;

namespace CarnetConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ipLocal = "192.168.1.100", ipRemota = "192.168.1.200", portNumberLocal = "2018", portNumberRemote = "7060";
            int timeOut = 30790;
            Core core = new();

            Console.WriteLine(core.SetTimeout(timeOut));
            Console.WriteLine(core.SetLocalEndPoint(ipLocal, int.Parse(portNumberLocal)));
            Console.WriteLine(core.SetRemoteEndPoint(ipRemota, int.Parse(portNumberRemote)));

            Console.WriteLine(core.Initialice());

            Console.WriteLine(core.ProcessNormalSale(20000, 0, 0, 100));

            Console.WriteLine("Hello, World!");
            Console.ReadLine();
        }
    }

    public class CardnetResult
    {
        public required string Status { get; set; }
        public required string[] Messages { get; set; }
    }
}