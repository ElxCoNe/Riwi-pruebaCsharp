using Microsoft.EntityFrameworkCore;
using PruebaRiwi.Data;
using PruebaRiwi.Enums;
using PruebaRiwi.Models;
using PruebaRiwi.Response;

namespace PruebaRiwi.Services;

public class ReservationService
{
    private readonly MysqlDbContext _context;

    public ReservationService(MysqlDbContext context)
    {
        _context = context;
    }

    // Obtener todas las reservas
    public ServiceResponse<IEnumerable<Reservation>> GetAll()
    {
        try
        {
            var reservations = _context.Reservations
                .Include(r => r.User)
                .Include(r => r.SportPlace)
                .ToList();

            return new ServiceResponse<IEnumerable<Reservation>>
            {
                Success = true,
                Data = reservations
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<IEnumerable<Reservation>>
            {
                Success = false,
                Message = "Error al obtener las reservas: " + ex.Message,
                Data = null
            };
        }
    }

    // Obtener por ID
    public ServiceResponse<Reservation> GetById(int id)
    {
        try
        {
            var reservation = _context.Reservations
                .Include(r => r.User)
                .Include(r => r.SportPlace)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return new ServiceResponse<Reservation>
                {
                    Success = false,
                    Message = "Reserva no encontrada",
                    Data = null
                };
            }

            return new ServiceResponse<Reservation>
            {
                Success = true,
                Data = reservation
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Reservation>
            {
                Success = false,
                Message = "Error al obtener la reserva: " + ex.Message,
                Data = null
            };
        }
    }

    // Crear reserva
    public ServiceResponse<Reservation> Create(Reservation reservation)
    {
        try
        {
            // Validar que la hora de fin sea mayor a la hora de inicio
            if (reservation.EndTime <= reservation.StartTime)
            {
                return new ServiceResponse<Reservation>
                {
                    Success = false,
                    Message = "La hora de fin debe ser mayor a la hora de inicio",
                    Data = null
                };
            }

            // Validar que no se creen reservas en fechas pasadas
            if (reservation.Date < DateOnly.FromDateTime(DateTime.Now))
            {
                return new ServiceResponse<Reservation>
                {
                    Success = false,
                    Message = "No se pueden crear reservas en fechas pasadas",
                    Data = null
                };
            }

            // Validar que no se creen reservas en horas pasadas (si es hoy)
            if (reservation.Date == DateOnly.FromDateTime(DateTime.Now) 
                && reservation.StartTime < TimeOnly.FromDateTime(DateTime.Now))
            {
                return new ServiceResponse<Reservation>
                {
                    Success = false,
                    Message = "No se pueden crear reservas en horas pasadas",
                    Data = null
                };
            }

            // Validar que el espacio no tenga reservas en rangos de tiempo solapados
            var placeSolap = _context.Reservations.Any(r =>
                r.SportPlaceId == reservation.SportPlaceId
                && r.Date == reservation.Date
                && r.Status == ReservationStatus.Active
                && r.StartTime < reservation.EndTime
                && r.EndTime > reservation.StartTime);

            if (placeSolap)
            {
                return new ServiceResponse<Reservation>
                {
                    Success = false,
                    Message = "El espacio deportivo ya tiene una reserva en ese horario",
                    Data = null
                };
            }

            // Validar que el usuario no tenga reservas en el mismo rango de horario
            var userSolap = _context.Reservations.Any(r =>
                r.UserId == reservation.UserId
                && r.Date == reservation.Date
                && r.Status == ReservationStatus.Active
                && r.StartTime < reservation.EndTime
                && r.EndTime > reservation.StartTime);

            if (userSolap)
            {
                return new ServiceResponse<Reservation>
                {
                    Success = false,
                    Message = "El usuario ya tiene una reserva en ese horario",
                    Data = null
                };
            }

            reservation.Status = ReservationStatus.Active;
            _context.Reservations.Add(reservation);
            var result = _context.SaveChanges();

            if (result > 0)
            {
                return new ServiceResponse<Reservation>
                {
                    Success = true,
                    Message = "Reserva creada exitosamente",
                    Data = reservation
                };
            }

            return new ServiceResponse<Reservation>
            {
                Success = false,
                Message = "No se pudo crear la reserva",
                Data = null
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Reservation>
            {
                Success = false,
                Message = "Error al crear la reserva: " + ex.Message,
                Data = null
            };
        }
    }

    // Cancelar reserva
    public ServiceResponse<Reservation> Cancel(int id)
    {
        try
        {
            var reservation = _context.Reservations.Find(id);

            if (reservation == null)
            {
                return new ServiceResponse<Reservation>
                {
                    Success = false,
                    Message = "Reserva no encontrada",
                    Data = null
                };
            }

            reservation.Status = ReservationStatus.Cancelled;
            _context.SaveChanges();

            return new ServiceResponse<Reservation>
            {
                Success = true,
                Message = "Reserva cancelada exitosamente",
                Data = reservation
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Reservation>
            {
                Success = false,
                Message = "Error al cancelar la reserva: " + ex.Message,
                Data = null
            };
        }
    }

    // Finalizar reserva
    public ServiceResponse<Reservation> Finish(int id)
    {
        try
        {
            var reservation = _context.Reservations.Find(id);

            if (reservation == null)
            {
                return new ServiceResponse<Reservation>
                {
                    Success = false,
                    Message = "Reserva no encontrada",
                    Data = null
                };
            }

            reservation.Status = ReservationStatus.Finished;
            _context.SaveChanges();

            return new ServiceResponse<Reservation>
            {
                Success = true,
                Message = "Reserva finalizada exitosamente",
                Data = reservation
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Reservation>
            {
                Success = false,
                Message = "Error al finalizar la reserva: " + ex.Message,
                Data = null
            };
        }
    }

    // Listar reservas por usuario
    public ServiceResponse<IEnumerable<Reservation>> GetByUser(int userId)
    {
        try
        {
            var reservations = _context.Reservations
                .Include(r => r.User)
                .Include(r => r.SportPlace)
                .Where(r => r.UserId == userId)
                .ToList();

            return new ServiceResponse<IEnumerable<Reservation>>
            {
                Success = true,
                Data = reservations
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<IEnumerable<Reservation>>
            {
                Success = false,
                Message = "Error al obtener las reservas del usuario: " + ex.Message,
                Data = null
            };
        }
    }

    // Listar reservas por espacio deportivo
    public ServiceResponse<IEnumerable<Reservation>> GetByPlace(int placeId)
    {
        try
        {
            var reservations = _context.Reservations
                .Include(r => r.User)
                .Include(r => r.SportPlace)
                .Where(r => r.SportPlaceId == placeId)
                .ToList();

            return new ServiceResponse<IEnumerable<Reservation>>
            {
                Success = true,
                Data = reservations
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<IEnumerable<Reservation>>
            {
                Success = false,
                Message = "Error al obtener las reservas del espacio: " + ex.Message,
                Data = null
            };
        }
    }
    
    
}