# TP 3 - Agenda
* Se necesita una aplicación para administrar una agenda personal, se deberá poder registrar diferentes eventos y contactos.
* Para los contactos se necesita guardar los datos de nombre, apellido, teléfono y email.
* Cada Evento tendrá, título, fecha y hora, cantidad de horas, contactos que participan del evento (puede no tener datos) y el lugar del evento.
* Permitir la administración de todos los datos, informes de eventos, eventos en que participa algún contacto, etc.
* En la carga de un evento se debe controlar que no se superpongas los eventos.
* Generar una vista por día donde se muestren todas las horas del día y si hay algún evento que este ocupando esa hora.
* Generar una vista por mes que muestra los eventos de cada día, tipo calendario.
---
## A Crear
**ABM eventos y contactos.**
1. **Contactos necesita**:
   * Nombre.
   * Apellido.
   * Teléfono.
   * Email.
2. **Evento necesita**:
   * Titulo.
   * Fecha y hora. 
   * Cantidad de horas.
   * Contactos que participan (pueden no tener datos).
   * Lugar.
   * Controlar superposición de eventos.
3. **Vistas**:
   * Por día, con cada hora y evento que hay (si hay).
   * Por mes, con los eventos del día.
### Relaciones:
   * A un evento puede asistir varias personas, pero una persona puede asistir a un solo evento por día.
   * Un día puede tener varios eventos, pero no pueden ocurrir en el mismo horario. (Posible entidad día?).
### Roles: 
  * **Dylan**: vistas (sugerencia).
  * **Aylem**: por asignar.
  * **Teo**: por asignar.
  * **Enzo**: por asignar.
