using System;

namespace API.Models;

public class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }

    public Person(string name, string email, string address, string phone)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Address = address;
        Phone = phone;
    }

    public void Update(string name, string email, string address, string phone)
    {
        Name = name;
        Email = email;
        Address = address;
        Phone = phone;
    }
}