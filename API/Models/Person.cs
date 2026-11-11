using System;

namespace SUM.Model;

public class Person
{
    private Guid Id { get; set; }
    private string Name { get; set; }
    private string Email { get; set; }
    private string Address { get; set; }
    private string Phone { get; set; }

    public Person(string name, string email, string address, string phone)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Address = address;
        Phone = phone;
    }
}