using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace SRP
{
    public class Journal
    {
        private readonly List<string> entries = new List<string>();

        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($" {++count}: {text}");
            return count;
        }
        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }
        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
       
    }
    class Persistence
    {
        public void Save(Journal j, string filename, bool overwritten = false)
        {
            if (overwritten || !File.Exists(filename))
            {
                File.WriteAllText(filename, j.ToString());
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I cried today");
            j.AddEntry("I eat a bug");
            WriteLine(j.ToString());
            var p = new Persistence();
            var filename = @"C:\Users\IGOR\source\repos\S.O.L.I.D\SRP\journal.txt";
            p.Save(j, filename, true);
            Process.Start(filename);
            Console.ReadLine();
        }
    }
}
