using System.ComponentModel.DataAnnotations;

namespace Student_Microservice.Models;

public class Student
{
    public int id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}