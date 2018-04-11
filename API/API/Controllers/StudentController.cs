using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Student")]
    public class StudentController : Controller
    {

        private readonly BasicSchoolContext _context;
        public StudentController(BasicSchoolContext context)
        {
            _context = context;
        }
        // GET: api/Student
        [HttpGet]
        public IEnumerable<Student> GetAll()
        {
            return _context.Student.ToArray();
        }

        // GET: api/Student/5
        [HttpGet("{id}", Name = "Get")]
        public string GetById(int id)
        {
            var student = _context.Student.FirstOrDefault(s => s.StudentId == id);
            if (student != null) return student.Name;
            return "value";
        }
        
        // Create
        // POST: api/Student
        [HttpPost]
        public IActionResult Post([FromBody]Student value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            _context.Student.Add(value);
            _context.SaveChanges();

            return CreatedAtRoute("Get", new {id = value.StudentId}, value);
        }
        
        // Update
        // PUT: api/Student/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Student value)
        {
            if (value == null || value.StudentId != id)
            {
                return BadRequest();
            }

            var student = _context.Student.FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            student.Name = value.Name;
            student.Age = value.Age;
            student.ClassRoomId = value.ClassRoomId;
            student.Gender = value.Gender;
            student.Grade = value.Grade;

            _context.Student.Update(student);
            _context.SaveChanges();

            return new NoContentResult();

        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = _context.Student.FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return BadRequest();
            }

            _context.Student.Remove(student);
            _context.SaveChanges();

            return new NoContentResult();

        }
    }
}
