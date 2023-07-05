using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.VisualBasic.FileIO;

namespace AIM2023_CodeGeneration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            GenerateFile(folder, "StateDisabilityWI.csv");
            GenerateFile(folder, "StateDualEnrollmentTypeWI.csv");
            GenerateFile(folder, "StateEducationEnvironmentWI.csv");

            Process.Start(folder);// open folder in windows explorer

            foreach (var value in StateDisabilityWI.AllValues)
            {
                Console.WriteLine($"{value.Code} - {value.Description}");
            }
            Console.ReadKey();
        }

        static void GenerateFile(string folder, string fileName)
        {
            // This method is what we'd write within an extension or other tool
            // Note: Our input could be from anywhere, UI, API calls, etc

            var fileNameNoExt = Path.GetFileNameWithoutExtension(fileName);
            var inputFilePath = Path.Combine(folder, fileName);
            var outputFilePath = Path.Combine(folder, fileNameNoExt + ".cs");

            using (TextFieldParser parser = new TextFieldParser(inputFilePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.CommentTokens = new[] { "#" };

                var tt = new CreateRuntime()
                {
                    FileName = fileNameNoExt,
                    Headers = parser.ReadFields(),
                    Values = new List<string[]>(),
                };

                while (!parser.EndOfData)
                {
                    tt.Values.Add(parser.ReadFields());
                }

                var csContents = tt.TransformText();
                File.WriteAllText(outputFilePath, csContents);
            }
        }
    }
}
