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
                for (int i = 0; i <= 3025; i++)
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
                            Console.Write("   " + DGN + "   " + PGN);
                            broadcastSource(DGN, hexString2);
                        } else {Console.WriteLine("Invalid number"); }
                    }
                }
                Console.SetWindowPosition(0, 1);
            }


            Console.ReadKey(true);

        }

        static void broadcastSource(string input, string hexString)
        {
            if (input == "1FED9")
            {
                Console.Write("       Generic Indicator Command PGN");
                /*if (hexString != null)
                {*/
                var biniry = Convert.ToString(Convert.ToInt64(hexString, 16), 2).PadLeft(4, '0');
                /*Console.WriteLine(biniry);*/
                
            } else if (input == "1FEDB")
            {
                Console.Write("       DC Dimmer Command 3 PGN");
                if (hexString != null)
                {
                    var binary = Convert.ToString(Convert.ToInt64(hexString, 16), 2).PadLeft(4, '0');
                    var num = binary.Substring(9, 7);
                    /* Console.WriteLine(binary);*/
                    if (binary.Substring(0, 8) == "11111111"){

                    } 
                    if (binary.Substring(8, 1) == "0")
                    {
                        var numb = "1";
                        if(num == "1111110") { numb = "1"; } else if (num == "1111101") { numb = "2"; } else if (num == "1111011") { numb = "3"; }
                        if (num != "11111111")
                        {
                            Console.Write("     Master Group " + numb);
                        } else { Console.Write("  Non-group related"); }
                    } else if (binary.Substring(8, 1) == "0")
                    {
                        var numeral = Convert.ToInt32(num, 2);
                        Console.Write("     Node Group " + numeral);
                    } 
                    int bitThree = Convert.ToInt32(binary.Substring(16,8), 2);
                    if (bitThree <= 249) {Console.Write("  Brightness level: " + bitThree/2 + "%");} else if (bitThree == 250) { Console.Write("  Dimmed memory value"); } else if (bitThree == 251) { Console.Write("  Master memory value"); }
                    int bitFour = Convert.ToInt32(binary.Substring(24,8), 2);

                    int bitFive = Convert.ToInt32(binary.Substring(32,8), 2);
                    if (bitFive <= 240) { Console.Write("  " + bitFive + " Seconds of delay"); } else if (bitFive >= 241 && bitFive <= 249) { Console.Write("  " + (bitFive - 240 + 5) + " minutes of delay"); } else if (bitFive == 250) { Console.Write("  30 minutes of delay"); } else { Console.Write("  350 ms timeout"); } 
                    if (binary.Substring(40, 2).Contains("1")) { Console.Write("  await returned interlock bitset"); }
                    if (binary.Substring(47, 1) == "1") { Console.Write(" will be locked"); } else { Console.Write(" will be unlocked"); }
                }
            } else if (input == "1FFBD")
            {

            }
        }
    }
}
