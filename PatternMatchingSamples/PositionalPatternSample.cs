using System;

namespace PatternMatchingSamples
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Teacher HomeRoomTeacher { get; set; }
        public int GradeLevel { get; set; }

        public Student(string firstName, string lastName, Teacher homeRoomTeacher, int gradeLevel)
        {
            FirstName = firstName;
            LastName = lastName;
            HomeRoomTeacher = homeRoomTeacher;
            GradeLevel = gradeLevel;
        }

        public void Deconstruct(out string firstName, out string lastName, out Teacher homeRoomTeacher, out int gradeLevel)
        {
            firstName = FirstName;
            lastName = LastName;
            homeRoomTeacher = HomeRoomTeacher;
            gradeLevel = GradeLevel;
        }
    
    }

    public class Teacher
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }

        public Teacher(string firstName, string lastName, string subject)
        {
            FirstName = firstName;
            LastName = lastName;
            Subject = subject;
        }

        public void Deconstruct(out string firstName, out string lastName, out string subject)
        {
            firstName = FirstName;
            lastName = LastName;
            subject = Subject;
        }
    }

    public static class PositionalPatternSample
    {
        public static bool IsInSeventhGradeMath(Student s)
        {
            return s is (_, _, (_, _, "Math"), 7);
        }
    }

    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string Region { get; set; }
        public Employee ReportsTo { get; set; }
    }

    public static class PropertyPatternSample
    {
        public static bool IsIsBasedWithUkManager(object o)
        {
            return o is Employee e && e is { Region: "US", ReportsTo: { Region: "UK"} };
        }
    }

    public class Rectangle
    {
        public int Length { get; set; }
        public int Width { get; set; }

        public Rectangle(int length, int width) => (Length, Width) = (length, width);
    }

    public class Triangle
    {
        public int Side1 { get; set; }
        public int Side2 { get; set; }
        public int Side3 { get; set; }

        public Triangle(int side1, int side2, int side3) =>
            (Side1, Side2, Side3) = (side, side2, side3);
    }

    public class Circle
    {
        public int Radius { get; set; }
        public Circle(int radius) => Radius = radius;
    }


    public static class SwitchExpressionSample
    {
        public static string DisplayShapeInfo(object shape) => 
            shape switch
            {
                Rectangle r => r switch
                {
                    _ when r.Length == r.Width => "Square!",
                    _ => "",
                },
                Rectangle r => $"Rectangle (l={r.Length} w={r.Width})",
                Circle {Radius: 1 } c => "Small Circle",
                Circle c => $"Circle (r={c.Radius})",
                Triangle t => $"Triangle ({t.Side1}, {t.Side2}, {t.Side3})",
                _ => "Unknown Shape"
            };
    }

    public enum Color
    {
        Unknown,
        Red,
        Blue,
        Green,
        Purple,
        Orange,
        Brown,
        Yellow
    }

    public static class TuplePatternSample
    {
        public static Color GetColor(Color c1, Color c2)
        {
            return (c1, c2) switch
            {
                (Color.Red, Color.Blue) => Color.Purple,
                (Color.Blue, Color.Red) => Color.Purple,
                (_,_) when c1 == c2 => c1,
                _ => Color.Unknown
            };
        }
    }
}



