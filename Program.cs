﻿using System;
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

                // Note: Our output could also be to anywhere like creating
                // files via VS extension APIs
                File.WriteAllText(outputFilePath, csContents);
            }
        }
    }
}
