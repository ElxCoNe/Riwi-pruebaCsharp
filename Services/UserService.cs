using PruebaRiwi.Data;
using PruebaRiwi.Models;
using PruebaRiwi.Response;

namespace PruebaRiwi.Services
{
    public class UserService
    {
        private readonly MysqlDbContext _context;

        public UserService(MysqlDbContext context)
        {
            _context = context;
        }

        //Listar todos los usuarios
        public ServiceResponse<IEnumerable<User>> GetAll()
        {
            try
            {
                var users = _context.Users.ToList();
                return new ServiceResponse<IEnumerable<User>>
                {
                    Success = true,
                    Data = users
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<User>>
                {
                    Success = false,
                    Message = "Error al obtener los usuarios: " + ex.Message,
                    Data = null
                };
            }
        }

        //Obtener usuario por ID
        public ServiceResponse<User> GetById(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user != null)
                {
                    return new ServiceResponse<User>
                    {
                        Success = true,
                        Data = user
                    };
                }
                return new ServiceResponse<User>
                {
                    Success = false,
                    Message = "Usuario no encontrado",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    Message = "Error al buscar el usuario: " + ex.Message,
                    Data = null
                };
            }
        }

        //Registrar usuario
        public ServiceResponse<User> Register(User user)
        {
            try
            {
                if (_context.Users.Any(u => u.Document == user.Document))
                {
                    return new ServiceResponse<User>
                    {
                        Success = false,
                        Message = "Ya existe un usuario con ese documento",
                        Data = null
                    };
                }

                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    return new ServiceResponse<User>
                    {
                        Success = false,
                        Message = "Ya existe un usuario con ese correo electrónico",
                        Data = null
                    };
                }

                _context.Users.Add(user);
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return new ServiceResponse<User>
                    {
                        Success = true,
                        Message = "Usuario registrado exitosamente",
                        Data = user
                    };
                }
                return new ServiceResponse<User>
                {
                    Success = false,
                    Message = "No se pudo registrar el usuario",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    Message = "Error al registrar el usuario: " + ex.Message,
                    Data = null
                };
            }
        }

        //Editar campos de usuario
        public ServiceResponse<User> Edit(User user)
        {
            try
            {
                var existingUser = _context.Users.Find(user.Id);
                if (existingUser == null)
                {
                    return new ServiceResponse<User>
                    {
                        Success = false,
                        Message = "Usuario no encontrado",
                        Data = null
                    };
                }

                if (_context.Users.Any(u => u.Document == user.Document && u.Id != user.Id))
                {
                    return new ServiceResponse<User>
                    {
                        Success = false,
                        Message = "Ya existe otro usuario con ese documento",
                        Data = null
                    };
                }

                if (_context.Users.Any(u => u.Email == user.Email && u.Id != user.Id))
                {
                    return new ServiceResponse<User>
                    {
                        Success = false,
                        Message = "Ya existe otro usuario con ese correo electrónico",
                        Data = null
                    };
                }

                existingUser.Name = user.Name;
                existingUser.Document = user.Document;
                existingUser.Phone = user.Phone;
                existingUser.Email = user.Email;

                _context.SaveChanges();

                return new ServiceResponse<User>
                {
                    Success = true,
                    Message = "Usuario actualizado exitosamente",
                    Data = existingUser
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    Message = "Error al actualizar el usuario: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}