namespace Calendar.Entities;

public class Contact
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public int Telefono { get; set; }
    public string Email { get; set; }

    public Contact(){

    }
    public Contact(string nombre, string apellido, int telefono, string email)
    {
        Nombre = nombre;
        Apellido = apellido;
        Telefono = telefono;
        Email = email;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}