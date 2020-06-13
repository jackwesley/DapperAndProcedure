using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudDapperAndProcedure.Models;
using CrudDapperAndProcedure.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CrudDapperAndProcedure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/Students
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _studentService.GetAll();
        }

        // GET api/Students/5
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            return _studentService.Get(id);
        }

        // POST api/Students
        [HttpPost]
        public Student Post([FromBody] Student student)
        {
            return _studentService.Save(student);
        }

        // PUT api/Students/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Student student)
        {
            return "Use method post";
        }

        // DELETE api/Students/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return _studentService.Delete(id);
        }
    }
}
