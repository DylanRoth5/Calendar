namespace Calendar.Entities;

public class Event
{
    public int Id;
    public string Title;
<<<<<<< HEAD
    public DateTime FechaHora;
    public string EndTime;
    public string StartTime;

    public int cantidadHoras;
    public string Place;

    public Event(){

    }
    public Event(string titulo, DateTime fechaHora, string EndTime, string StartTime, string lugar)
    {
        Title = titulo;
        FechaHora = fechaHora;
        this.EndTime = EndTime;
        this.StartTime = StartTime;
        cantidadHoras = int.Parse(EndTime) - int.Parse(StartTime);
        Place = lugar;
=======
    public DateTime Start;
    public DateTime End;
    public string Place;

    public Event(string Title, DateTime Start, DateTime End, string Place)
    {
        this.Title = Title;
        this.Start = Start;
        this.End = End;
        this.Place = Place;
>>>>>>> 9b2cd98f79c00e97513e736b60bc14a13fe5900e
    }

    public override string ToString()
    {
        return base.ToString();
    }
}