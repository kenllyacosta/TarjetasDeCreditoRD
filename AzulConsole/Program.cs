using AZUL.Ingenico4;
using AZUL.Ingenico4.Requests;
using AZUL.Ingenico4.Responses;

namespace AzulConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
			try
			{
                Client pos = new("COM4", "C:\\Temp\\Azul");
                SaleRequest q = new(1.00);
                BaseResponse r = pos.Send(q);
                Console.WriteLine($"AID            = {r.AID}");
                Console.WriteLine($"BatchNumber    = {r.BatchNumber}");
                Console.WriteLine($"BIN            = {r.BIN}");
                Console.WriteLine($"CardHolderName = {r.CardHolderName}");
                Console.WriteLine($"CVM            = {r.CVM}");
                Console.WriteLine($"Date           = {r.Date}");
                Console.WriteLine($"InvoiceNumber  = {r.InvoiceNumber}");
                Console.WriteLine($"TSI            = {r.TSI}");
                Console.WriteLine($"TVR            = {r.TVR}");
            }
			catch (Exception ex)
			{
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Hello, World!");
            Console.ReadLine();
        }
    }
}