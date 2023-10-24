using System.Data.SQLite;
using Calendar.Entities;

namespace Calendar.Conections;

public class pRelation
{
    public static List<Relation> getAll()
    {
        var relations = new List<Relation>();
        var cmd = new SQLiteCommand("select RegisId, EventId, ContactId from Registered");
        cmd.Connection = Conexion.Connection;
        var obdr = cmd.ExecuteReader();

        while (obdr.Read())
        {
            var relation = new Relation();
            relation.Id = obdr.GetInt32(0);
            relation.EventId = obdr.GetInt32(1);
            relation.ContactId = obdr.GetInt32(2);
            relations.Add(relation);
        }

        return relations;
    }

    public static void Save(Relation relation)
    {
        var cmd = new SQLiteCommand("insert into Registered(EventId, ContactId) values(@EventId, @ContactId)");
        cmd.Parameters.Add(new SQLiteParameter("@EventId", relation.EventId));
        cmd.Parameters.Add(new SQLiteParameter("@ContactId", relation.ContactId));
        cmd.Connection = Conexion.Connection;
        cmd.ExecuteNonQuery();
    }

    public static void Delete(Relation relation)
    {
        Console.WriteLine("Se va a eliminar la relacion con id: " + relation.Id);
        Console.ReadKey(true);
        var cmd = new SQLiteCommand("delete from Registered where RegisId = @Id");
        cmd.Parameters.Add(new SQLiteParameter("@Id", relation.Id));
        cmd.Connection = Conexion.Connection;
        cmd.ExecuteNonQuery();
    }
}