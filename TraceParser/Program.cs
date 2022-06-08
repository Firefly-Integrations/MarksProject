using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TraceParser
{
    class Program
    {
        static void Main(string[] args)
        {

            string filePath = @"C:\Users\ethana\stuff.txt";
            // Read and display lines from the file until the end of
            // the file is reached.
            Console.WriteLine("Message#  |Tos (ms)| Bus|Type |    ID |  Data Len  |   Data Bytes");

            /*            using (StreamReader sr = new StreamReader(filePath))
                        {
                            for (int i = 0; i>=1; i++)
                            {
                                Console.WriteLine(sr.ReadLine());
                            }
                        }*/

            /*foreach (string line in lines)
            {*/
            using (StreamReader sr = new StreamReader(filePath))
            {
                for (int i = 0; i <= 30; i++)
                {
                    if (i <= 20)
                    {
                        //Do nothing
                        sr.ReadLine();
                    }
                    else
                    {
                        var grape = sr.ReadLine();
                        byte[] banana = Encoding.Default.GetBytes(grape);
                        Console.Write(grape);
                        var hexString = BitConverter.ToString(banana);
                        /*hexString = hexString.Replace("-", "");*/
                        Console.WriteLine("\n\n" + hexString + "\n");
                    }
                }
            }


            Console.ReadKey(true);

        }
    }
}
