namespace Calendar.Entities;

public class Event
{
    public string Title;
    public DateTime FechaHora;
    public int cantidadHoras;
    public string Place;

    public Event(string titulo, DateTime fechaHora, int cantidadHoras, string lugar)
    {
        Title = titulo;
        FechaHora = fechaHora;
        this.cantidadHoras = cantidadHoras;
        Place = lugar;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}