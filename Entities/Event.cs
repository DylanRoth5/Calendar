namespace Calendar.Entities;

public class Event
{
    public int Id;
    public string Title;
    public DateTime EndDate;
    public DateTime StartDate;
    public TimeSpan Duration;
    public string Place;

    public Event(){

    }
    public Event(string title, DateTime StartDate, DateTime EndDate, string place)
    {
        Title = title;
        StartDate = StartDate;
        EndDate = EndDate;
        Duration = EndDate - StartDate;
        Place = place;
    }

    public override string ToString()
    {
        return $"[{Id}] [{Title}] [{StartDate}] [{EndDate}] [{Duration}] [{Place}]";
    }

    public void setDuration()
    {
        Duration = EndDate - StartDate;
    }
}