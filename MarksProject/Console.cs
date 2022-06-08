using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Figgle;
using System.IO;

namespace MarksProject
{
    internal class Console
    {
        string filePath = @"C:\Users\ethana\Desktop\stuff2.txt";

        string [] lines = File.ReadAllLines(filePath);

        List<string> lines = new List<string>();
        lines = File.ReadAllLines(filePath).ToList();

        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }

        Console.ReadKey(true);

    }
}
