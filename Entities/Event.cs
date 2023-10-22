namespace Calendar.Entities;

public class Event
{
    public int Id;
    public string Title;
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
    }

    public override string ToString()
    {
        return base.ToString();
    }
}