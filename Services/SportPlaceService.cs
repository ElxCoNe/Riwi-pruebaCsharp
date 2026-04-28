using PruebaRiwi.Data;
using PruebaRiwi.Enums;
using PruebaRiwi.Models;
using PruebaRiwi.Response;

namespace PruebaRiwi.Services;

public class SportPlaceService
{
    private readonly MysqlDbContext _context;
    
    public SportPlaceService(MysqlDbContext context)
    {
        _context = context;
    }

    //Obtener todos

    public ServiceResponse<IEnumerable<SportPlace>> GetAll()
    {
        try
        {
            var sportPlaces = _context.SportPlaces.ToList();
            return new ServiceResponse<IEnumerable<SportPlace>>
            {
                Data = sportPlaces,
                Success = true
            };

        }
        catch (Exception ex)
        {
            return new ServiceResponse<IEnumerable<SportPlace>>
            {
                Success = false,
                Message = "Error al obetner lugares de deporte" + ex.Message,
                Data = null
            };

        }
        
    }

    //Obtener por ID
    public ServiceResponse<SportPlace> GetById(int id)
    {
        try
        {
            var sportPlace = _context.SportPlaces.Find(id);
            if (sportPlace == null)
            {
                return new ServiceResponse<SportPlace>
                {
                    Success = false,
                    Data = null,
                    Message = "No se encontro el lugar"
                };
            }

            return new ServiceResponse<SportPlace>
            {
                Success = true,
                Data = sportPlace
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<SportPlace>
            {
                Message = "Error al obtener el lugar" + ex.Message,
                Success = false,
                Data = null
            };
        }
        
    }

    //Crear lugar de deporte
    public ServiceResponse<SportPlace> Register(SportPlace sportPlace)
    {
        try
        {
            if (_context.SportPlaces.Any(s => s.Name == sportPlace.Name))
            {
                return new ServiceResponse<SportPlace>
                {
                    Success = false,
                    Message = "Lugar ya se encuentra registradp",
                    Data = null
                };
            }
            _context.SportPlaces.Add(sportPlace);
            var result = _context.SaveChanges();

            if (result > 0)
            {
                return new ServiceResponse<SportPlace>
                {
                    Success = true,
                    Message = "Lugar registrado",
                    Data = sportPlace
                };
            }

            return new ServiceResponse<SportPlace>
            {
                Success = false,
                Message = "No se pudo registrar el lugar",
                Data = null,
            };


        }
        catch (Exception ex)
        {
            return new ServiceResponse<SportPlace>
            {
                Success = false,
                Message = "Error al registrar el lugar" + ex.Message,
                Data = null
            };
        }
        
    }



    //Edicion de lugar de deporte
    public ServiceResponse<SportPlace> Edit(SportPlace sportPlace)
    {
        try
        {
            var existingPlace = _context.SportPlaces.Find(sportPlace.Id);
            if (existingPlace == null)
            {
                return new ServiceResponse<SportPlace>
                {
                    Success = false,
                    Message = "Lugar no encontrado",
                    Data = null
                };
            }

            if (_context.SportPlaces.Any(s => s.Name == sportPlace.Name && s.Id != sportPlace.Id))
            {
                return new ServiceResponse<SportPlace>
                {
                    Success = false,
                    Message = "Lugar ya se encuentra registrado",
                    Data = null
                };
            }
            
            existingPlace.Name = sportPlace.Name;
            existingPlace.Type = sportPlace.Type;
            existingPlace.Capacity = sportPlace.Capacity;
            
            _context.SaveChanges();
            return new ServiceResponse<SportPlace>
            {
                Success = true,
                Message = "Lugar editador con exito",
                Data = existingPlace
            };


        }
        catch (Exception ex)
        {
            return new ServiceResponse<SportPlace>
            {
                Success = false,
                Message = "Error al editar el lugar" + ex.Message,
                Data = null
            };
        }
    }
    
    //Para filtrar por tipo de lugar de deporte
    public ServiceResponse<IEnumerable<SportPlace>> FilterByType(PlaceType type)
    {
        try
        {
            var sportPlaces = _context.SportPlaces
                .Where(s => s.Type == type)
                .ToList();

            return new ServiceResponse<IEnumerable<SportPlace>>
            {
                Success = true,
                Data = sportPlaces
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<IEnumerable<SportPlace>>
            {
                Success = false,
                Message = "Error al filtrar los espacios: " + ex.Message,
                Data = null
            };
        }
    }
    
    
}