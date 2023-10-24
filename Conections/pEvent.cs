using System.Data.SQLite;
using System.Data.SqlTypes;
using System.Globalization;
using Calendar.Entities;

namespace Calendar.Conections
{
    internal class pEvent
    {
        public static List<Event> getAll()
        {
            var events = new List<Event>();
            var cmd = new SQLiteCommand("select EventId, Title, Start, End, Place from Events");
            cmd.Connection = Conexion.Connection;
            var reader = cmd.ExecuteReader();
            
            var defaultDate = DateTime.Parse($"01/01/2000 00:00:00");
            while (reader.Read())
            {
                var event_ = new Event();
                event_.Id = reader.GetInt32(0);
                event_.Title = reader.GetString(1);
                event_.Start = DateTime.Parse(reader.GetString(2));
                event_.End = DateTime.Parse(reader.GetString(3));
                event_.Place = reader.GetString(4);
                event_.setDuration();
                events.Add(event_);
            }

            return events;
        }

        public static void Save(Event evnt)
        {
            var cmd = new SQLiteCommand(
                "insert into Events(Title, Start, End, Place) values(@Title, @Start, @End, @Place)");
            cmd.Parameters.Add(new SQLiteParameter("@Title", evnt.Title));
            cmd.Parameters.Add(new SQLiteParameter("@Start", $"{evnt.Start.Day}/{evnt.Start.Month}/{evnt.Start.Year} {evnt.Start.Hour}:{evnt.Start.Minute}:{evnt.Start.Second}"));
            cmd.Parameters.Add(new SQLiteParameter("@End", $"{evnt.End.Day}/{evnt.End.Month}/{evnt.End.Year} {evnt.End.Hour}:{evnt.End.Minute}:{evnt.End.Second}"));
            cmd.Parameters.Add(new SQLiteParameter("@Place", evnt.Place));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }

        public static void Delete(Event event_)
        {
            Console.WriteLine("Se va a eliminar al contacto con id: " + event_.Id);
            Console.ReadKey(true);
            var cmd = new SQLiteCommand("delete from Events where EventId = @Id");
            cmd.Parameters.Add(new SQLiteParameter("@Id", event_.Id));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }

        public static void Update(Event evnt)
        {
            var cmd = new SQLiteCommand(
                "UPDATE Events SET Title = @Title, Start = @Start, End = @End, Place = @Place WHERE EventId = @EventId");
            cmd.Parameters.Add(new SQLiteParameter("@EventId", evnt.Id));
            cmd.Parameters.Add(new SQLiteParameter("@Title", evnt.Title));
            cmd.Parameters.Add(new SQLiteParameter("@Start", $"{evnt.Start.Day}/{evnt.Start.Month}/{evnt.Start.Year} {evnt.Start.Hour}:{evnt.Start.Minute}:{evnt.Start.Second}"));
            cmd.Parameters.Add(new SQLiteParameter("@End", $"{evnt.End.Day}/{evnt.End.Month}/{evnt.End.Year} {evnt.End.Hour}:{evnt.End.Minute}:{evnt.End.Second}"));
            cmd.Parameters.Add(new SQLiteParameter("@Place", evnt.Place));
            cmd.Connection = Conexion.Connection;
            
            cmd.ExecuteNonQuery();
        }
    }
}