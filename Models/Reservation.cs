using PruebaRiwi.Enums;

namespace PruebaRiwi.Models;

public class Reservation
{
    int Id { get; set; }
    int UserId { get; set; }
    int SportPlaceId { get; set; }
    DateOnly Date { get; set; }
    TimeOnly StartTime { get; set; }
    TimeOnly EndTime { get; set; }
    ReservationStatus Status { get; set; }

    //Navegation
    public  SportPlace? SportPlace { get; set; }
    public User? User { get; set; }

}  