using APBD_3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_3.Services
{
    public class FileParser
    {
        
        public static List<Student> ParseFileFromCsv(FileInfo file)
        {
            List<Student> students = new List<Student>();

            using (StreamReader stream = new StreamReader(file.OpenRead()))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] st = line.Split(',');


                    Student student = new Student
                    {
                        FirstName = st[0],
                        LastName = st[1],
                        IndexNumber = st[2],
                        BirthDate = st[3],
                        Major = st[4],
                        Mode = st[5],
                        Email = st[6],
                        FathersName = st[8],
                        MothersName = st[7]
                    };
                    students.Add(student);


                }
            }

            return students;
        }

        public static void SaveToFile(Student student)
        {

            string result = Environment.NewLine + 
                    student.FirstName + "," +
                    student.LastName + "," +
                    student.IndexNumber + "," +
                    student.BirthDate + "," +
                    student.Major + "," +
                    student.Mode + "," +
                    student.Email + "," +
                    student.FathersName + "," +
                    student.MothersName;

            File.AppendAllText("Data/studenci.csv", result);

        }
    }
}
