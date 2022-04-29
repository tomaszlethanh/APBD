using APBD_3.Models;
using APBD_3.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace APBD_3.Controllers
{
    
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = new List<Student>();

        

        [HttpGet]
        public IActionResult GetStudents()
        {
            string path = "Data/studenci.csv";
            FileInfo fi = new FileInfo(path);
            students = FileParser.ParseFileFromCsv(fi);
            return Ok(students);
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber)
        {
            //string path = "Data/studenci.csv";
            //FileInfo fi = new FileInfo(path);
            //students = FileParser.ParseFileFromCsv(fi);
            //students = students.Where(x => x.IndexNumber.Equals(indexNumber)).ToList();
            return Ok(students.ElementAt(0));
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            students.Add(student);
            FileParser.SaveToFile(student);
            return Ok();
        }
    }
}
