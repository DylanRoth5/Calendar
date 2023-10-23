using System.Data.SQLite;

namespace Calendar.Conections
{
    internal static class Conexion
    {
        static string connectionString = $"Data Source=Calendar.db";

        public static void OpenConnection()
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
                Connection.Open();
            using var command = new SQLiteCommand("PRAGMA foreign_keys = ON;", Connection);
            command.ExecuteNonQuery();
        }
        public static void CloseConnection()
        {
            Connection.Close();
        }
        public static SQLiteConnection Connection { set; get; } = new SQLiteConnection(connectionString);
    }
}
