using System;
using System.IO;
using System.Text;

namespace FileCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("C# console application that will compare two large CSV files line by line");
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectDir = Directory.GetParent(baseDir).Parent.Parent.FullName;

            //sample data source http://eforexcel.com/wp/downloads-18-sample-csv-files-data-sets-for-testing-sales/#more-13035
            string[] OneCSV_Lines = File.ReadAllLines(projectDir + "\\one.csv");
            string[] TwoCSV_Lines = File.ReadAllLines(projectDir + "\\two.csv");

            Console.WriteLine("Number of lines in one.csv : {0}",OneCSV_Lines.Length);
            Console.WriteLine("Number of lines in two.csv : {0}", TwoCSV_Lines.Length);

            var dffCSV = new StringBuilder();
            var sameCSV = new StringBuilder();
            var d = 0;
            var s = 0;

            for (int li=0; li<OneCSV_Lines.Length; li++)
            {
                if (li < TwoCSV_Lines.Length)
                {
                    if (OneCSV_Lines[li].Equals(TwoCSV_Lines[li]))
                    {
                        sameCSV.AppendLine(OneCSV_Lines[li]);
                        s++;
                        //Console.WriteLine("Same line in two.csv : {0}", OneCSV_Lines[li]);
                    }
                    else
                    {
                        //Console.WriteLine("Different line in two.csv : {0}", OneCSV_Lines[li]);
                        dffCSV.AppendLine(OneCSV_Lines[li]);
                        d++;
                    }
                }
                else
                {
                    //Console.WriteLine("line NOT in two.csv : {0}", OneCSV_Lines[li]);
                }
            }

            Console.WriteLine("Number of diff lines : {0}", d);
            Console.WriteLine("Number of same lines : {0}", s);

            File.WriteAllText(projectDir + "\\diff.csv", dffCSV.ToString());
            File.WriteAllText(projectDir + "\\same.csv", sameCSV.ToString());

            Console.WriteLine("diff file path {0}\\diff.csv", projectDir);

            Console.ReadLine();
        } 
    }
}
