using System;

namespace CreatePdf
{
    class Program
    {
        static void Main(string[] args)
        {
            new InvoiceToPdf()
                .FillPdf(@$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\StackOverflow Invoice Test.pdf");

            Console.WriteLine("The test is finished");
            Console.ReadKey();
        }
    }
}
