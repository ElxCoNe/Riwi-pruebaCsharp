using PruebaRiwi.Enums;

namespace PruebaRiwi.Models;

public class SportPlace
{
    int Id { get; set; }
    int Name { get; set; }
    PlaceType Type { get; set; }
    int Capacity { get; set; }
    
    //Navigation
    public ICollection<Reservation> Reservations { get; set; }
    
}