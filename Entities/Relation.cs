using Calendar.Controllers;

namespace Calendar.Entities;

public class Relation
{
    public int Id;
    public int EventId;
    public int ContactId;

    public Relation()
    {
    }

    public override string ToString()
    {
        return $"[{Id}] \n  Event:[{IEvent.Select(EventId)}] \n   Contact:[{IContact.Select(ContactId)}]";
    }
}