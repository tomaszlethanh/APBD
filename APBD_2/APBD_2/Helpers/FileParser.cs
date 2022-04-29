using APBD_2.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace APBD_2.Helpers
{
    public static class FileParser
    {
        public static HashSet<Student> ParseFileFromCsv(FileInfo file)
        {
            HashSet<Student> students = new HashSet<Student>();
            string logText = "";
            try
            {
                using (StreamReader stream = new StreamReader(file.OpenRead()))
                {
                    string line = null;
                    int lineNum = 1;
                    while ((line = stream.ReadLine()) != null)
                    {
                        string[] st = line.Split(',');
                        int colCount = st.Length;
                        if (colCount == 9)
                        {
                            Boolean flag = false;
                            foreach (string element in st)
                            {
                                if (String.IsNullOrEmpty(element))
                                {
                                    flag = true;
                                }
                            }


                            if (!flag)
                            {
                                Kierunek kierunek = new Kierunek
                                {
                                    Nazwa = st[2],
                                    Tryb = st[3]
                                };

                                Student student = new Student
                                {
                                    FirstName = st[0],
                                    LastName = st[1],
                                    IndexNumber = st[4],
                                    BirthDate = st[5],
                                    Email = st[6],
                                    MothersName = st[7],
                                    FathersName = st[8],
                                    Studies = kierunek
                                };
                                students.Add(student);
                            }
                            else
                            {
                                
                                logText += "Pusta wartość w linii numer: " + lineNum + Environment.NewLine;
                            }


                        }
                        else
                        {
                            logText += "Nieprawidłowa liczba kolumn w linii numer: " + lineNum + Environment.NewLine;
                        }
                        lineNum++;
                    }
                    File.WriteAllText("./Data/log.txt", logText);
                }
                
            }
            catch(FileNotFoundException e)
            {
                File.WriteAllText("./Data/log.txt", "Plik nie istnieje" + Environment.NewLine);
            }
            
            
            return students;
        }
    }
}
