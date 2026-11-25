namespace API.DTOs;

public class PersonDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public PersonDto(string name, string email, string address, string phone)
    {
        Name = name;
        Email = email;
        Address = address;
        Phone = phone;
    }
}