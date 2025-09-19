using System;

namespace gr3act7
{
    public class Student
    {
    public int ID { get; set; }
#nullable disable
    public string Name { get; set; }
#nullable restore
    public int Age { get; set; }
    public DateTime Birthday { get; set; }
    }
}