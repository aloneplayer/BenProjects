using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//参数对象：把多个参数封装成一个对象
namespace LosTechies.DaysOfRefactoring.SampleCode.ParameterObject.Before
{
    public class Student
    { }

    public class Course
    { }

    public class Registration
    {
        public void Create(decimal amount, Student student, IEnumerable<Course> courses, decimal credits)
        {
            // do work
        }
    }
}

namespace LosTechies.DaysOfRefactoring.SampleCode.ParameterObject.After
{
    public class Student
    { }

    public class Course
    { }

    public class RegistrationContext
    {
        public decimal Amount { get; set; }
        public Student Student { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public decimal Credits { get; set; }
    }

    public class Registration
    {
        public void Create(RegistrationContext registrationContext)
        {
            // do work
        }
    }
}