using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace AIM2023_CodeGeneration
{
    internal class Program
    {

        /// Goal: Input CSV(s), Output classes with static values
        static void Main(string[] args)
        {
            var folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            GenerateFile(folder, "StateDisabilityWI.csv");
            GenerateFile(folder, "StateDualEnrollmentTypeWI.csv");
            GenerateFile(folder, "StateEducationEnvironmentWI.csv");

            Process.Start(folder);// open folder in windows explorer
        }

        static void GenerateFile(string folder, string fileName)
        {
            var fileNameNoExt = Path.GetFileNameWithoutExtension(fileName);
            var inputFilePath = Path.Combine(folder, fileName);
            var outputFilePath = Path.Combine(folder, fileNameNoExt + ".cs");

            // Note: Our input could be from anywhere, UI, API calls, etc
            using (TextFieldParser parser = new TextFieldParser(inputFilePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.CommentTokens = new[] { "#" };

                // TODO

                var csContents = string.Empty;// TODO
                File.WriteAllText(outputFilePath, csContents);
            }
        }
    }
}
