using System;
using System.IO;
using System.Globalization;
using ExerciseFilaAndStream.Entities;

namespace CourseFileAndStream
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter file full path: ");
            string sourceFilePath = Console.ReadLine();

            try
            {
                string[] lines = File.ReadAllLines(sourceFilePath);

                string sourceFolderPath = Path.GetDirectoryName(sourceFilePath);
                string targetFolderPath = $@"{sourceFolderPath}\out";
                string targetFilePath = $@"{targetFolderPath}\summary.csv";

                Directory.CreateDirectory(targetFolderPath);

                using (StreamWriter sw = File.AppendText(targetFilePath))
                {
                    foreach (string line in lines)
                    {
                        string[] field = line.Split(',');

                        string name = field[0];
                        double value = double.Parse(field[1], CultureInfo.InvariantCulture);
                        int quantity = int.Parse(field[2]);
                        Products prod = new Products(name, value, quantity);

                        sw.WriteLine($@"{prod.Name}, {prod.Total().ToString("F2", CultureInfo.InvariantCulture)}");
                    }
                }

            }
            catch(IOException e)
            {
                Console.WriteLine("An error ocurred.");
                Console.WriteLine(e.Message);
            }

        }
    }
}