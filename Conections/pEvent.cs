using System.Data.SQLite;
using Calendar.Entities;

namespace Calendar.Conections
{
    internal class pEvent
    {
        public static List<Event> getAll()
        {
            List<Event> events = new List<Event>();
            SQLiteCommand cmd = new SQLiteCommand("select EventId, Title, Start, End, Place from Events");
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();

            while (obdr.Read())
            {
                Event evt = new Event();
                evt.Id = obdr.GetInt32(0);
                evt.Title = obdr.GetString(1);
                evt.Start = DateTime.Parse(obdr.GetString(2));
                evt.End = DateTime.Parse(obdr.GetString(3));
                evt.Place = obdr.GetString(4);
                evt.setDuration();
                events.Add(evt);
            }
            return events;
        }
        public static void Save(Event evt)
        {
            SQLiteCommand cmd = new SQLiteCommand("insert into Events( Title, Start, End, Place ) values( @Title, @Start, @End, @Place )");
            cmd.Parameters.Add(new SQLiteParameter("@Title", evt.Title));
            cmd.Parameters.Add(new SQLiteParameter("@Start", evt.Start));
            cmd.Parameters.Add(new SQLiteParameter("@End", evt.End));
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
            SQLiteCommand cmd = new SQLiteCommand("UPDATE Events SET Title = @Title, Start = @Start, End = @End, Place = @Place  WHERE EventId = @EventID");
            cmd.Parameters.Add(new SQLiteParameter("@EventId", evt.Id));
            cmd.Parameters.Add(new SQLiteParameter("@Title", evt.Title));
            cmd.Parameters.Add(new SQLiteParameter("@Start", evt.Start));
            cmd.Parameters.Add(new SQLiteParameter("@End", evt.End));
            cmd.Parameters.Add(new SQLiteParameter("@Place", evt.Place));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }
    }
}
        // public static Event getById(int id)
        // {
        //     Event evt = new Event();
        //     SQLiteCommand cmd = new SQLiteCommand("select EventId, Title, StartDate, Place, Start, End, from Events where EventId = @EventId");
        //     cmd.Parameters.Add(new SQLiteParameter("@EventId", evt.Id));
        //     cmd.Connection = Conexion.Connection;
        //     SQLiteDataReader obdr = cmd.ExecuteReader();
        //     while (obdr.Read())
        //     {
        //         evt.Id = obdr.GetInt32(0);
        //         evt.Title = obdr.GetString(1);
        //         evt.FechaHora = DateTime.Parse(obdr.GetString(2));
        //         evt.Place = obdr.GetString(3);
        //         string startTime = obdr.GetString(4);
        //         string endTime = obdr.GetString(5);
        //         evt.Start = startTime;
        //         evt.End = endTime;
        //         TimeSpan timeDifference = DateTime.Parse(endTime) - DateTime.Parse(startTime);
        //         evt.cantidadHoras = (int)timeDifference.TotalHours;
        //         
        //     }
        //     return evt;
        // }
