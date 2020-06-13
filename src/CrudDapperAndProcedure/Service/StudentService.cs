using CrudDapperAndProcedure.Configuration;
using CrudDapperAndProcedure.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CrudDapperAndProcedure.Service
{
    public class StudentService : IStudentService
    {
        Student _student = new Student();
        List<Student> _students = new List<Student>();


        public string Delete(int studentId)
        {
            string message = string.Empty;

            try
            {
                _student = new Student()
                {
                    StudentId = studentId
                };
                
                using (IDbConnection con = new SqlConnection(Settings.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();

                    var students = con.Query<Student>("SP_Student", this.SetParameters(_student, (int)OperationType.Delete),
                        commandType: CommandType.StoredProcedure);

                    if (students != null && students.Count() > 0)
                    {
                        _student = students.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {

                message = ex.Message;
            }


            return message;
        }

        public Student Get(int studentId)
        {
            _student = new Student();

            using (IDbConnection con = new SqlConnection(Settings.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                var students = con.Query<Student>("SELECT * FROM Student WHERE StudentId = " + studentId).ToList();

                if (students != null && students.Count() > 0)
                {
                    _student = students.SingleOrDefault();
                }
            }

            return _student;
        }

        public List<Student> GetAll()
        {
            _students = new List<Student>();

            using (IDbConnection con = new SqlConnection(Settings.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                var students = con.Query<Student>("SELECT * FROM Student").ToList();

                if (students != null && students.Count() > 0)
                {
                    _students = students;
                }
            }
            return _students;
        }

        public Student Save(Student student)
        {
            _student = new Student();
            try
            {
                int operationType = Convert.ToInt32(student.StudentId == 0 ? OperationType.Insert : OperationType.Update);

                using (IDbConnection con = new SqlConnection(Settings.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();

                    var students = con.Query<Student>("SP_Student", this.SetParameters(student, operationType),
                        commandType: CommandType.StoredProcedure);

                    if (students != null && students.Count() > 0)
                    {
                        _student = students.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {

                _student.Message = ex.Message;
            }

            return _student;
        }

        private DynamicParameters SetParameters(Student student, int operationType)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@StudentId", student.StudentId);
            parameters.Add("@Name", student.Name);
            parameters.Add("@Roll", student.Roll);
            parameters.Add("@OperationType", operationType);

            return parameters;
        }
    }
}
