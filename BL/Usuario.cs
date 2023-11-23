using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaControlFarmaciaContext context = new DL.FlunaControlFarmaciaContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}' , '{usuario.Email}' , '{usuario.Password}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto el registro";

                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaControlFarmaciaContext context = new DL.FlunaControlFarmaciaContext())
                {
                    var query = context.Usuarios.FromSqlRaw("UsuarioGetAll").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Usuario usuario = new ML.Usuario()
                            {
                               IdUsuario = obj.IdUsuario,
                               Nombre = obj.Nombre,
                               Email = obj.Email,
                               Password = obj.Password
                            };
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaControlFarmaciaContext context = new DL.FlunaControlFarmaciaContext())
                {
                    var usuario = context.Usuarios.FirstOrDefault(e => e.IdUsuario == IdUsuario);

                    if (usuario != null)
                    {
                        context.Usuarios.Remove(usuario);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró el empleado para eliminar.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaControlFarmaciaContext context = new DL.FlunaControlFarmaciaContext())
                {
                    var objquery = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();
                    if (objquery != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objquery.IdUsuario;
                        usuario.Nombre = objquery.Nombre;
                        usuario.Email = objquery.Email;
                        usuario.Password = objquery.Password;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo completar los registros de la tabla";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaControlFarmaciaContext context = new DL.FlunaControlFarmaciaContext())
                {
                    var existingUser = context.Usuarios.Find(usuario.IdUsuario);

                    if (existingUser != null)
                    {
                        existingUser.Nombre = usuario.Nombre;
                        existingUser.Email = usuario.Email;
                        existingUser.Password = usuario.Password;

                        context.SaveChanges();

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró el usuario para actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result ValidarUsuario(string email, string password)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaControlFarmaciaContext context = new DL.FlunaControlFarmaciaContext())
                {
                    // Consulta para verificar las credenciales del usuario
                    var usuario = context.Usuarios
                        .FirstOrDefault(u => u.Email == email && u.Password == password);

                    if (usuario != null)
                    {
                        result.Correct = true;
                        // Puedes incluir más información del usuario en el resultado si es necesario
                        result.Object = usuario;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Credenciales incorrectas";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
