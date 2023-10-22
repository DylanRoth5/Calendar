using System.Data;
using System.Data.SQLite;
using Calendar.Entities;

namespace Calendar.Controllers
{
    internal class pContact
    {
        public static List<Contact> getAll()
        {
            List<Contact> contacts = new List<Contact>();
            SQLiteCommand cmd = new SQLiteCommand("select ContactId, Name, LastName, Phone, Email from Contacts");
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();

            while (obdr.Read())
            {
                Contact contact = new Contact();
                contact.Id = obdr.GetInt32(0);
                contact.Nombre = obdr.GetString(1);
                contact.Apellido = obdr.GetString(2);
                contact.Telefono = obdr.GetInt32(3);
                contact.Email = obdr.GetString(4);
                contacts.Add(contact);
            }
            return contacts;
        }
        public static Contact getById(int id)
        {
            Contact contact = new Contact();
            SQLiteCommand cmd = new SQLiteCommand("select Contactd, Name, LastName, Phone, Email from Contacts where Contactd = @Contactd");
            cmd.Parameters.Add(new SQLiteParameter("@Contactd", contact.Id));
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                contact.Id = obdr.GetInt32(0);
                contact.Nombre = obdr.GetString(1);
                contact.Apellido = obdr.GetString(2);
                contact.Telefono = obdr.GetInt32(3);
                contact.Email = obdr.GetString(4);
                
            }
            return contact;
        }
        public static void Save(Contact contact)
        {
            SQLiteCommand cmd = new SQLiteCommand("insert into Contacts(Name, LastName, Phone, Email) values(@Name, @LastName, @Phone, @Email)");
            cmd.Parameters.Add(new SQLiteParameter("@Name", contact.Nombre));
            cmd.Parameters.Add(new SQLiteParameter("@LastName", contact.Apellido));
            cmd.Parameters.Add(new SQLiteParameter("@Phone", contact.Telefono));
            cmd.Parameters.Add(new SQLiteParameter("@Email", contact.Email));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }

        public static void Delete(Contact contact)
        {
            Console.WriteLine("Se va a eliminar al contacto con id: " + contact.Id);
            Console.ReadKey(true);
            SQLiteCommand cmd = new SQLiteCommand("delete from Contacts where Contactd = @Contactd");
            cmd.Parameters.Add(new SQLiteParameter("@Contactd", contact.Id));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }
        public static void Update(Contact contact)
        {
            SQLiteCommand cmd = new SQLiteCommand("UPDATE Contacts SET Name = @Name, LastName = @LastName, Phone = @Phone, Email = @Email WHERE Contactd = @Contactd");
            cmd.Parameters.Add(new SQLiteParameter("@Contactd", contact.Id));
            cmd.Parameters.Add(new SQLiteParameter("@Name", contact.Nombre));
            cmd.Parameters.Add(new SQLiteParameter("@LastName", contact.Apellido));
            cmd.Parameters.Add(new SQLiteParameter("@Phone", contact.Telefono));
            cmd.Parameters.Add(new SQLiteParameter("@Email", contact.Email));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }
    }
}
