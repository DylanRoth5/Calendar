namespace Calendar.Entities;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public int Phone { get; set; }
    public string Email { get; set; }

    public Contact(){

    }
    public Contact(string name, string lastName, int phone, string email)
    {
        Name = name;
        LastName = lastName;
        Phone = phone;
        Email = email;
    }

    public override string ToString()
    {
        return $"[{Id}] [{Name}] [{LastName}] [{Phone}] [{Email}]";
    }
}