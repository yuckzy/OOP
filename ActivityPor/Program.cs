using System;

namespace ActivityPor
{
    abstract class Student
    {
        protected string name;
        protected double grade;
        protected int age;
        public void SetData(string name, double grade)
        {
            this.name = name;
            this.grade = grade;
        }
        public abstract void DisplayInfo();
        public abstract string CheckStatus();
    }

    class CollegeStudent : Student
    {
        private double passingGrade = 3.0; // 1.0 is highest, 3.0 is passing
        public override string CheckStatus()
        {
            return grade <= passingGrade ? "Passed" : "Failed";
        }
        public override void DisplayInfo()
        {
            Console.WriteLine($"College Student Name: {name}, Grade: {grade:F2}");
            Console.WriteLine($"Status: {CheckStatus()}");
        }
    }

    class SeniorStudent : Student
    {
        private double passingGrade = 75.0; // 75 is passing for senior high
        public override string CheckStatus()
        {
            return grade >= passingGrade ? "Passed" : "Failed";
        }
        public override void DisplayInfo()
        {
            Console.WriteLine($"Senior Student Name: {name}, Grade: {grade:F2}");
            Console.WriteLine($"Status: {CheckStatus()}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Student collegeStudent1 = new CollegeStudent();
            collegeStudent1.SetData("Dwyane", 1.25);
            collegeStudent1.DisplayInfo();

            Student collegeStudent2 = new CollegeStudent();
            collegeStudent2.SetData("Karl", 2.75);
            collegeStudent2.DisplayInfo();

            Student seniorStudent1 = new SeniorStudent();
            seniorStudent1.SetData("Luffy", 99.99);
            seniorStudent1.DisplayInfo();

            Student seniorStudent2 = new SeniorStudent();
            seniorStudent2.SetData("Zoro", 74.45);
            seniorStudent2.DisplayInfo();
        }
    }
}
