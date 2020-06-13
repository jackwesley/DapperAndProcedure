using CrudDapperAndProcedure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudDapperAndProcedure.Service
{
    public interface IStudentService
    {
        Student Save(Student student);
        List<Student> GetAll();

        Student Get(int studentId);
        string Delete(int studentId);

    }
}
