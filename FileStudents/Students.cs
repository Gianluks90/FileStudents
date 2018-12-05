using System;
using System.Collections.Generic;
using System.Text;

namespace FileStudents
{
    public class Student
    {
        public int ID;
        public string name;
        public string surname;
        public int age;
        public string gender;
        public int mark;

        public Student(string s0, string s1, string s2, string s3, string s4, string s5)
        {
            ID = int.Parse(s0);
            name = s1;
            surname = s2;
            age = int.Parse(s3);
            gender = s4;
            mark = int.Parse(s5);
        }
    }
}
