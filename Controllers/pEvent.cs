using System.Data;
using System.Data.SQLite;
using Calendar.Entities;

namespace Calendar.Controllers
{
    internal class pEvent
    {
        public static List<Event> getAll()
        {
            List<Event> events = new List<Event>();
            SQLiteCommand cmd = new SQLiteCommand("select EventId, Title, StartDate, Place, StartTime, EndTime, from Events");
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();

            while (obdr.Read())
            {
                Event evt = new Event();
                evt.Id = obdr.GetInt32(0);
                evt.Title = obdr.GetString(1);
                evt.FechaHora = DateTime.Parse(obdr.GetString(2));
                evt.Place = obdr.GetString(3);
                string startTime = obdr.GetString(4);
                string endTime = obdr.GetString(5);
                evt.StartTime = startTime;
                evt.EndTime = endTime;
                TimeSpan timeDifference = DateTime.Parse(endTime) - DateTime.Parse(startTime);
                evt.cantidadHoras = (int)timeDifference.TotalHours;
                events.Add(evt);
            }
            return events;
        }
        public static Event getById(int id)
        {
            Event evt = new Event();
            SQLiteCommand cmd = new SQLiteCommand("select EventId, Title, StartDate, Place, StartTime, EndTime, from Events where EventId = @EventId");
            cmd.Parameters.Add(new SQLiteParameter("@id", evt.Id));
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                evt.Id = obdr.GetInt32(0);
                evt.Title = obdr.GetString(1);
                evt.FechaHora = DateTime.Parse(obdr.GetString(2));
                evt.Place = obdr.GetString(3);
                string startTime = obdr.GetString(4);
                string endTime = obdr.GetString(5);
                evt.StartTime = startTime;
                evt.EndTime = endTime;
                TimeSpan timeDifference = DateTime.Parse(endTime) - DateTime.Parse(startTime);
                evt.cantidadHoras = (int)timeDifference.TotalHours;
                
            }
            return evt;
        }
        public static void Save(Event evt)
        {
            SQLiteCommand cmd = new SQLiteCommand("insert into Events(Title, StartDate, Place, StartTime, EndTime) values(@Title, @StartDate, @Place, @StartTime, @EndTime)");
            cmd.Parameters.Add(new SQLiteParameter("@Title", evt.Title));
            cmd.Parameters.Add(new SQLiteParameter("@StartDate", evt.FechaHora));
            cmd.Parameters.Add(new SQLiteParameter("@Place", evt.Place));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }

        public static void Delete(Event evt)
        {
            Console.WriteLine("Se va a eliminar al Evento con id: " + evt.Id);
            Console.ReadKey(true);
            SQLiteCommand cmd = new SQLiteCommand("delete from Events where EventId = @EventId");
            cmd.Parameters.Add(new SQLiteParameter("@EventId", evt.Id));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }
        public static void Update(Event evt)
        {
            SQLiteCommand cmd = new SQLiteCommand("UPDATE Events SET Name = @Name, LastName = @LastName, Phone = @Phone, Email = @Email WHERE EventId = @EventID");
            cmd.Parameters.Add(new SQLiteParameter("@EventId", evt.Id));
            // cmd.Parameters.Add(new SQLiteParameter("@Name", contact.Nombre));
            // cmd.Parameters.Add(new SQLiteParameter("@LastName", contact.Apellido));
            // cmd.Parameters.Add(new SQLiteParameter("@Phone", contact.Telefono));
            // cmd.Parameters.Add(new SQLiteParameter("@Email", contact.Email));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }
    }
}
