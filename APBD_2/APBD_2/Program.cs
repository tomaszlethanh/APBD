using APBD_2.Helpers;
using APBD_2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace APBD_2
{
    class Program
    {
        
        public static void Main(string[] args)
        {
            string path = args[0];
            string dataFormat = args[1];
            string destinationPath = $"{args[2]}.{dataFormat}";

            FileInfo fi = new FileInfo(path);

            HashSet<Student> students = FileParser.ParseFileFromCsv(fi);
            Uczelnia uni = new Uczelnia(students);
            Serializable ser = new Serializable(uni);
            string jsonString = Serializer.SerializeToJson(ser);
            try
            {
                File.WriteAllText(destinationPath, jsonString);
            }
            catch
            {
                File.WriteAllText("./Data/log.txt", "Podana ścieżka jest niepoprawna" + Environment.NewLine);
            }
            
        }
    }
}
