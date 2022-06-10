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
            int choice2 = 0;
            Console.Clear();
            string filePath = @"C:\Users\ethana\stuff.txt";
            // Read and display lines from the file until the end of
            // the file is reached.
            using (StreamReader sr = new StreamReader(filePath))
            {
                if (choice == 1)
                {
                    Console.WriteLine("Message#|Tos (ms)    |  ID |     Data Bytes");
                } else if (choice == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(FiggleFonts.Banner.Render("Hexadecimal"));
                    Console.ForegroundColor = ConsoleColor.White;
                } else if (choice == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(FiggleFonts.Stacey.Render("Filtered Devices"));
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("1. Used\n2. Unused");
                    choice2 = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(FiggleFonts.Slant.Render("Device IDs"));
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("   ID (Binary)   | DGN | PGN |  Binary");
                } 
                if (choice2 == 1)
                {
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
                            if (choice == 1)
                            {
                                Console.Write("\n" + everything);
                            }
                            else if (choice == 2)
                            {
                                Console.WriteLine(everything.Substring(0, 8) + "   " + hexString);
                            }
                            if (DGN == "FEDB" || DGN == "FFBD" || DGN == "1FEDD")
                            {
                                if (choice == 3)
                                {
                                    Console.Write("\n" + everything.Substring(0, 8));
                                    Console.Write(id);
                                    if (id.Contains("9"))
                                    {
                                        DGN = "1" + DGN;
                                    }
                                    else
                                    {
                                        DGN = " " + DGN;
                                    }
                                    var binary = Convert.ToString(Convert.ToInt32(PGN, 16), 2);
                                    Console.Write("   " + DGN + "   " + PGN + "   " + binary);
                                    broadcastSource(DGN, hexString2);
                                }
                                else { Console.WriteLine("Invalid number"); }
                            }
                        }
                    }
                } else
                {
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
                            if (choice == 1)
                            {
                                Console.Write("\n" + everything);
                            }
                            else if (choice == 2)
                            {
                                Console.WriteLine(everything.Substring(0, 8) + "   " + hexString);
                            }
                            if (DGN == "FEDB" || DGN == "FFBD" || DGN == "1FEDD")
                            {
                                // Do nothing
                            } else
                            {
                                if (choice == 3)
                                {
                                    Console.Write("\n" + everything.Substring(0, 8));
                                    Console.Write(id);
                                    if (id.Contains("9"))
                                    {
                                        DGN = "1" + DGN;
                                    }
                                    else
                                    {
                                        DGN = " " + DGN;
                                    }
                                    Console.Write("   " + DGN + "   " + PGN);
                                    broadcastSource(DGN, hexString2);
                                }
                                else { Console.WriteLine("Invalid number"); }
                            }
                        }
                    }
                }
                
                Console.SetWindowPosition(0, 1);
            }


            Console.ReadKey(true);

        }

        static void broadcastSource(string input, string hexString)
        {
            var binary = Convert.ToString(Convert.ToInt64(hexString, 16), 2).PadLeft(4, '0');
            if (input == "1FED9")
            {
                Console.Write("       Generic Indicator Command PGN");
                /*if (hexString != null)
                {*/

                /*Console.WriteLine(biniry);*/

            }
            else if (input == "1FEDB")
            {
                Console.Write("       DC Dimmer Command 3 PGN");
            if (hexString != null)
                {
                    var num = binary.Substring(9, 7);
                    /* Console.WriteLine(binary);*/
                    if (binary.Substring(0, 8) == "11111111")
                    {

                    }
                    if (binary.Substring(8, 1) == "0")
                    {
                        var numb = "1";
                        if (num == "1111110") { numb = "1"; } else if (num == "1111101") { numb = "2"; } else if (num == "1111011") { numb = "3"; }
                        if (num != "11111111")
                        {
                            Console.Write("     Master Group " + numb);
                        }
                        else { Console.Write("  Non-group related"); }
                    }
                    else if (binary.Substring(8, 1) == "0")
                    {
                        var numeral = Convert.ToInt32(num, 2);
                        Console.Write("     Node Group " + numeral);
                    }
                    int bitThree = Convert.ToInt32(binary.Substring(16, 8), 2);
                    if (bitThree <= 249) { Console.Write("  Brightness level: " + bitThree / 2 + "%"); } else if (bitThree == 250) { Console.Write("  Dimmed memory value"); } else if (bitThree == 251) { Console.Write("  Master memory value"); }
                    int bitFour = Convert.ToInt32(binary.Substring(24, 8), 2);

                    int bitFive = Convert.ToInt32(binary.Substring(32, 8), 2);
                    if (bitFive <= 240) { Console.Write(" " + bitFive + " Seconds of delay"); } else if (bitFive >= 241 && bitFive <= 249) { Console.Write("  " + (bitFive - 240 + 5) + " minutes of delay"); } else if (bitFive == 250) { Console.Write("  30 minutes of delay"); } else { Console.Write("  350 ms timeout"); }
                    if (binary.Substring(40, 2).Contains("1")) { Console.Write("  await returned interlock bitset"); }
                    if (binary.Substring(47, 1) == "1") { Console.Write(" will be locked"); } else { Console.Write(" will be unlocked"); }

                }
            }
            else if (input == "1FFBD")
            {
                Console.Write("       DC Load Status 1 PGN");
                int bitOne = Convert.ToInt32(binary.Substring(0, 8), 2);
                if (bitOne <= 248) { Console.Write(" Valid Command"); } else { Console.Write(" INVALID COMMAND"); }
                if (binary.Substring(15, 1) == "0") { Console.Write(" Group 1"); } else if (binary.Substring(14, 1) == "0") { Console.Write(" Group 2"); } else { Console.Write(" No Data"); }
                int bitThree = Convert.ToInt32(binary.Substring(16, 8), 2);
                if (bitThree <= 249) { Console.Write("  Brightness level: " + bitThree / 2 + "%"); } else if (bitThree == 252) { Console.Write("  Flashing"); }
                int bitFive = Convert.ToInt32(binary.Substring(32, 8), 2);
                if (bitFive <= 200) { Console.Write(" " + bitFive + " seconds of delay"); } else { Console.Write(" no delay/duration active"); }
            }
            else if (input == "1FEDC")
            {
                Console.Write("       DC Load Status 2 PGN");
                int bitOne = Convert.ToInt32(binary.Substring(0, 8), 2);
                if (bitOne <= 248) { Console.Write(" Valid Command"); } else { Console.Write(" INVALID COMMAND"); }
                if (binary.Substring(9, 1) == "1") { Console.Write(" Locked"); } else { Console.Write(" Unlocked"); }
                /*if (binary.Substring())*/
                if (binary.Substring(25, 1) == "1") { Console.Write(" Interlock Command Active"); } else { Console.Write(" Interlock Command Inactive"); }
            }
            else if (input == "1FFBC")
            {
                Console.Write("       DC Load Command PGN");
                int bitOne = Convert.ToInt32(binary.Substring(0, 8), 2);
                if (bitOne <= 248) { Console.Write(" Instance Number " + bitOne); } else { Console.Write(" INVALID Instance Number"); }
                var num = binary.Substring(9, 7);
                if (binary.Substring(8, 1) == "0")
                {
                    var numb = "1";
                    if (num == "1111110") { numb = "1"; } else if (num == "1111101") { numb = "2"; } else if (num == "1111011") { numb = "3"; }
                    if (num != "11111111")
                    {
                        Console.Write("     Master Group " + numb);
                    }
                    else { Console.Write("  Non-group related"); }
                }
                else if (binary.Substring(8, 1) == "0")
                {
                    var numeral = Convert.ToInt32(num, 2);
                    Console.Write("     Node Group " + numeral);
                }
                else if (binary.Substring(8, 8) == "11111111") { Console.Write(" Non Group Command"); }
                int bitThree = Convert.ToInt32(binary.Substring(16, 8), 2);
                if (bitThree <= 249) { Console.Write("  Brightness level: " + bitThree / 2 + "%"); } else if (bitThree == 251) { Console.Write("  Master memory value"); }
                if (binary.Substring(28, 1) == "1") { Console.Write(" Interlock A"); } else { Console.Write(" Interlock B"); }
                // More commands
                int bitSix = Convert.ToInt32(binary.Substring(40, 8), 2);
                if (bitSix <= 240) { Console.Write("  " + bitSix + " Seconds of delay"); } else if (bitSix >= 241 && bitSix <= 249) { Console.Write("  " + (bitSix - 240 + 5) + " minutes of delay"); } else if (bitSix == 250) { Console.Write("  30 minutes of delay"); }
            }
            else if (input == "1FF8F")
            {
                Console.Write("       AC Load Status 1 PGN");
                int bitOne = Convert.ToInt32(binary.Substring(0, 8), 2);
                if (bitOne <= 248) { Console.Write(" Instance Number " + bitOne); } else { Console.Write(" INVALID Instance Number"); }
                if (binary.Substring(15, 1) == "0") { Console.Write(" Group 1"); } else if (binary.Substring(14, 1) == "0") { Console.Write(" Group 2"); } else { Console.Write(" No Data"); }
                int bitThree = Convert.ToInt32(binary.Substring(16, 8), 2);
                if (bitThree <= 249) { Console.Write("  Output level: " + bitThree / 2 + "%"); } else if (bitThree == 252) { Console.Write("  Flashing"); }
                int bitFive = Convert.ToInt32(binary.Substring(40, 8), 2);
                if (bitFive <= 240) { Console.Write("  " + bitFive + " Seconds of delay"); } else if (bitFive >= 241 && bitFive <= 249) { Console.Write("  " + (bitFive - 240 + 5) + " minutes of delay"); } else if (bitFive == 250) { Console.Write("  30 minutes of delay"); }
            }
            else if (input == "1FEBD")
            {
                Console.Write("       AC Load Status 2 PGN");
                int bitOne = Convert.ToInt32(binary.Substring(0, 8), 2);
                if (bitOne <= 248) { Console.Write(" Instance Number " + bitOne); } else { Console.Write(" INVALID Instance Number"); }
                if (binary.Substring(9, 1) == "1") { Console.Write(" will be locked"); } else { Console.Write(" will be unlocked"); }
                if (binary.Substring(11, 1) == "1") { Console.Write(" Overcurrent condition active"); } else { Console.Write(" Overcurrent condition inactive"); }
                if (binary.Substring(13, 1) == "1") { Console.Write(" Override Active"); } else { Console.Write(" Override Inactive"); }
                //More commands
            } else if (input == "1FEBD")
            {
            Console.Write("       AC Load Status 2 PGN");
            int bitOne = Convert.ToInt32(binary.Substring(0, 8), 2);
            if (bitOne <= 248) { Console.Write(" Instance Number " + bitOne); } else { Console.Write(" INVALID Instance Number"); }
            if (binary.Substring(9, 1) == "1") { Console.Write(" will be locked"); } else { Console.Write(" will be unlocked"); }
            if (binary.Substring(11, 1) == "1") { Console.Write(" Overcurrent condition active"); } else { Console.Write(" Overcurrent condition inactive"); }
            if (binary.Substring(13, 1) == "1") { Console.Write(" Override Active"); } else { Console.Write(" Override Inactive"); }
            }
            else if (input == "1FEE8" || input == "1FFFD" || input == "1EAFF" || input == "1EA8F" || input == "1EA8E" || input == " ECFF" || input == "1FFD7" || input == " EBFF" || input == "1FFD5" || input == "1FFD6" || input == "1FECA" || input == "1FFD4" || input == "1FFD8")
            {
                Console.Write("       DC Load Command PGN");
                int bitOne = Convert.ToInt32(binary.Substring(0, 8), 2);
                if (bitOne <= 248)
                {
                    Console.Write(" Instance Number " + bitOne);
                }
                else
                {
                    Console.Write(" INVALID Instance Number");
                }
                var num = binary.Substring(9, 7);
                if (binary.Substring(8, 1) == "0")
                {
                    var numb = "1";
                    if (num == "1111110")
                    {
                        numb = "1";
                    }
                    else if (num == "1111101")
                    {
                        numb = "2";
                    }
                    else if (num == "1111011")
                    {
                        numb = "3";
                    }
                    if (num != "11111111")
                    {
                        Console.Write("     Master Group " + numb);
                    }
                    else
                    {
                        Console.Write("  Non-group related");
                    }
                }
                else if (binary.Substring(8, 1) == "0")
                {
                    var numeral = Convert.ToInt32(num, 2);
                    Console.Write("     Node Group " + numeral);
                }
                else if (binary.Substring(8, 8) == "11111111")
                {
                    Console.Write(" Non Group Command");
                }
                int bitThree = Convert.ToInt32(binary.Substring(16, 8), 2);
                if (bitThree <= 249)
                {
                    Console.Write("  Brightness level: " + bitThree / 2 + "%");
                }
                else if (bitThree == 251)
                {
                    Console.Write("  Master memory value");
                }
                if (binary.Substring(28, 1) == "1")
                {
                    Console.Write(" Interlock A");
                }
                else
                {
                    Console.Write(" Interlock B");
                }
                int bitSix = Convert.ToInt32(binary.Substring(40, 8), 2);
                if (bitSix <= 240)
                {
                    Console.Write("  " + bitSix + " Seconds of delay");
                }
                else if (bitSix >= 241 && bitSix <= 249)
                {
                    Console.Write("  " + (bitSix - 240 + 5) + " minutes of delay");
                }
                else if (bitSix == 250)
                {
                    Console.Write("  30 minutes of delay");
                }
            }
        }
    }
}