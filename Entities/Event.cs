namespace Calendar.Entities;

public class Event
{
    public string Title;
    public DateTime Start;
    public DateTime End;
    public string Place;

    public Event(string Title, DateTime Start, DateTime End, string Place)
    {
        this.Title = Title;
        this.Start = Start;
        this.End = End;
        this.Place = Place;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}