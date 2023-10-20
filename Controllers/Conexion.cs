using System.Data.SQLite;

namespace Calendar.Controllers
{
    internal class Conexion
    {
        static string connectionString = $"Data Source=Calendar.db";
        static SQLiteConnection conexion = new SQLiteConnection(connectionString);
        public static void OpenConnection()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
                conexion.Open();
                using (SQLiteCommand command = new SQLiteCommand("PRAGMA foreign_keys = ON;", conexion))
                {
                    command.ExecuteNonQuery();
                }
        }
        public static void CloseConnection()
        {
            conexion.Close();
        }
        public static SQLiteConnection Connection
        {
            set { conexion = value; }
            get { return conexion; }
        }
    }
}
