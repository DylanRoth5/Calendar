using Calendar.Conections;
using Calendar.Entities;
using Calendar.Seal;

namespace Calendar.Controllers;

public interface IRelation
{
    
    public static void Create()
    {
        var relation = new Relation();
        relation.EventId = IEvent.Select().Id; //Cambiar a validation
        relation.ContactId = IContact.Select().Id; //Cambiar a validation
        pRelation.Save(relation);
    }

    public static void Delete(Relation relation)
    {
        pRelation.Delete(relation);
    }

    public static void List()
    {
        foreach (var relation in pRelation.getAll()) Tools.SayLine(relation.ToString());
    }

    public static Relation Select()
    {
        List();
        var id = Tools.ReadInt("Enter the Id: ");
        return pRelation.getAll().FirstOrDefault(relation => relation.Id == id) ?? throw new InvalidOperationException();
    }
}