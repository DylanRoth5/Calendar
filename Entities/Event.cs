namespace Calendar.Entities;

public class Event
{
    public int Id;
    public string Title;
    public DateTime End;
    public DateTime Start;
    public TimeSpan Duration;
    public string Place;

    public Event(){

    }
    public Event(string title, DateTime start, DateTime end, string place)
    {
        Title = title;
        Start = start;
        End = end;
        Duration = End - Start;
        Place = place;
    }

    public override string ToString()
    {
        return $"[{Id}] [{Title}] [{Start}] [{End}] [{Duration}] [{Place}]";
    }

    public void setDuration()
    {
        Duration = End - Start;
    }
}