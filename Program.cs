using Calendar.Controllers;
using Calendar.Entities;

namespace Calendar;

internal static class Program
{
    // TP 3 - Agenda
    // Se necesita una aplicación para administrar una agenda personal, se deberá poder registrar diferentes eventos y contactos.
    //     Para los contactos se necesita guardar los datos de nombre, apellido, teléfono y email.
    //     Cada Evento tendrá, título, fecha y hora, cantidad de horas, contactos que participan del evento (puede no tener datos) y el lugar del evento.
    //     Permitir la administración de todos los datos, informes de eventos, eventos en que participa algún contacto, etc.
    //     En la carga de un evento se debe controlar que no se superpongas los eventos.
    //     Generar una vista por día donde se muestren todas las horas del día y si hay algún evento que este ocupando esa hora.
    //     Generar una vista por mes que muestra los eventos de cada día, tipo calendario.
    //
    // •	ABM eventos y contactos.
    // •	Contactos necesita:
    // o	Nombre.
    //     o	Apellido.
    //     o	Teléfono.
    //     o	Email.
    // •	Evento necesita:
    // o	Titulo.
    //     o	Fecha y hora. 
    //     o	Cantidad de horas.
    //     o	Contactos que participan (pueden no tener datos).
    // o	Lugar.
    // •	Controlar superposición de eventos.
    // •	Vistas:
    // o	Por día, con cada hora y evento que hay (si hay).
    // o	Por mes, con los eventos del día.
    // •	Relaciones:
    // o	A un evento puede asistir varias personas, pero una persona puede asistir a un solo evento por día.
    //     o	Un día puede tener varios eventos, pero no pueden ocurrir en el mismo horario. (Posible entidad día?).
    // •	Roles: 
    // o	Ya que le sale bien el menú gamer, Dylan puede hacer las vistas (sugerencia).
    // o	Aylem, Teo, Enzo: por asignar.

    public static List<Thing>? Things;
    private static void Main()
    {
        Things = new List<Thing>();
        Load();
        Menu();
        Save();
    }
    private static void Menu()
    {
        while (true)
        {
            string[] options = { "Thing" };
            var result = Seal.Menu("Calendar Menu", options);
            switch (result)
            {
                case 1:
                    NThing.Menu();
                    continue;
                case 0:
                    break;
            }
            break;
        }
    }
    private static void Save()
    {
        File.WriteAllText(@"Data\\Thing.txt", "");
        if (Things != null)
            foreach (var item in Things)
                Seal.FileWrite(item.ToString(), @"Data\\Thing.txt");
    }
    private static void Load()
    {
        var ThingData = File.ReadAllLines(@"Data\\Thing.txt");
        foreach (var info in ThingData)
        {
            string?[] data = info.Split('|');
            NThing.Add(data[1]);
        }
    }
}