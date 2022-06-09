using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Figgle;

namespace TraceParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(FiggleFonts.Train.Render(" Information types"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. General info\n2. hexadecimal\n3. Device ID (binary)");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            string filePath = @"C:\Users\ethana\stuff.txt";
            // Read and display lines from the file until the end of
            // the file is reached.
            using (StreamReader sr = new StreamReader(filePath))
            {
                if (choice == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(FiggleFonts.Banner.Render("Hexadecimal"));
                    Console.ForegroundColor = ConsoleColor.White;
                } else if (choice == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(FiggleFonts.Slant.Render("Device IDs"));
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("   ID (Binary)   | DGN | PGN");
                }
                for (int i = 0; i <= 30; i++)
                {
                    if (i <= 20)
                    {
                        //Do nothing
                        sr.ReadLine();
                    }
                    else
                    {
                        var everything = sr.ReadLine();
                        var id = everything.Substring(31, 8);
                        var DGN = id.Substring(2, 4);
                        var PGN = id.Substring(6);
                        var half = everything.Substring(48, 8);
                        byte[] banana = Encoding.Default.GetBytes(everything);
                        byte[] banana2 = Encoding.Default.GetBytes(half);
                        everything = everything.Substring(0, 8) + everything.Substring(10, 12) + everything.Substring(27, 12) + everything.Substring(44);
                        var hexString = BitConverter.ToString(banana);
                        var hexString2 = BitConverter.ToString(banana2);
                        hexString = hexString.Replace("-", "");
                        hexString2 = hexString2.Replace("-", "");
                        /*id = id.Substring(0, 8);*/
                        if (choice == 1)
                        {
                            Console.WriteLine("Message#|Tos (ms)    |  ID |     Data Bytes");
                            Console.Write("\n" + everything);
                        } else if (choice == 2)
                        {
                            Console.WriteLine(everything.Substring(0, 8) + "   " + hexString);
                        } else if (choice == 3)
                        {
                            Console.Write("\n" + everything.Substring(0, 8));
                            Console.Write(id);
                            if (id.Contains("9"))
                            {
                                DGN = "1" + DGN;
                            } else
                            {
                                DGN = " " + DGN;
                            }
                            Console.WriteLine("   " + DGN + "   " + PGN);
                            broadcastSource(DGN, hexString2);
                        } else {Console.WriteLine("Invalid number"); }
                        Console.SetWindowPosition(0, 1);
                    }
                }
            }


            Console.ReadKey(true);

        }

        static void broadcastSource(string input, string hexString)
        {
            if (input == "1FED9")
            {
                Console.Write("       Spyder RV-C / Switch Application Layer Architecture");
                if (hexString != null)
                {
                    var biniry = Convert.ToString(Convert.ToInt64(hexString, 16), 2).PadLeft(4, '0');
                    Console.WriteLine(biniry);
                }
            }
        }
    }
}
