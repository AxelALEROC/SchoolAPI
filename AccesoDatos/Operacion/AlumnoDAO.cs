using AccesoDatos.Context;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Operacion
{
    public class AlumnoDAO  
    {
        public ProyectoContext context = new ProyectoContext();//PARA AGREGAR EL CONTEXTO ES NECESARIO HACERLO EN LA CLASE PARA HACER USO DE SUS PROPIEDADES

        public List<Alumno> seleccionarTodos()
        {
            var alumnos = context.Alumnos.ToList<Alumno>();
            return alumnos;
        }

        public Alumno seleccionar(int id)
        {
            var alumno = context.Alumnos.Where(a => a.Id==id).FirstOrDefault();
            return alumno;
        }
        public Alumno seleccionarDni(string dni)
        {
            var alumno = context.Alumnos.Where(a => a.Dni.Equals(dni)).FirstOrDefault();
            return alumno;
        }

        public bool insertar(string dni, string nombre, string direccion, int edad, string email)
        {
            try
            {
                Alumno alumno = new Alumno();
                alumno.Dni = dni;
                alumno.Nombre = nombre;
                alumno.Direccion = direccion;
                alumno.Edad = edad;
                alumno.Email = email;

                context.Alumnos.Add(alumno);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Update(int id, string dni, string nombre, string direccion, int edad, string email)
        {
            try
            {
                var Alumno = seleccionar(id);
                if (Alumno == null)
                {
                    return false;
                }
                else
                {
                    Alumno.Dni = dni; 
                    Alumno.Nombre = nombre;
                    Alumno.Direccion = direccion;
                    Alumno.Edad = edad;
                    Alumno.Email = email;

                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var alumno = seleccionar(id);
                if (alumno == null)
                {
                    return false;
                }
                else
                {
                    context.Alumnos.Remove(alumno);
                    context.SaveChanges();
                    return true;
                }
               
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<AlumnoAsignatura> selecAlumnosAsignaturas()
        {
            var query = from a in context.Alumnos
                        join m in context.Matriculas on a.Id equals m.AlumnoId
                        join asig in context.Asignaturas on m.AsignaturaId equals asig.Id
                        select new AlumnoAsignatura
                        {
                            NombreAlumno = a.Nombre,
                            NombreAsignatura = asig.Nombre
                        };
            return query.ToList();
        }
        public List<AlumnoProfesor> seleccionarAlumnosProfesor(string usuario)
        {
            var query = from a in context.Alumnos
                        join m in context.Matriculas on a.Id equals m.AlumnoId
                        join asig in context.Asignaturas on m.AsignaturaId equals asig.Id
                        where asig.Profesor == usuario
                        select new AlumnoProfesor
                        {
                            Id = a.Id,
                            Dni = a.Dni,
                            Nombre = a.Nombre,
                            Direccion = a.Direccion,
                            Edad = a.Edad,
                            Email = a.Email,
                            Asignatura = asig.Nombre
                        };
                        return query.ToList();
        }
        public bool agregarYMatricular(string dni, string nombre, string direccion, int edad, string email, int id_asig)
        {
            try
            {
                var exist = seleccionarDni(dni);
                if (exist == null)
                {
                    insertar(dni, nombre, direccion, edad, email);
                    var insertado = seleccionarDni(dni);
                    Matricula mat = new Matricula();
                    mat.AlumnoId = insertado.Id;
                    mat.AsignaturaId = id_asig;
                    context.Matriculas.Add(mat);
                    context.SaveChanges();
                }
                else
                {
                    Matricula matricula = new Matricula();
                    matricula.AlumnoId = exist.Id;
                    matricula.AsignaturaId = id_asig;
                    context.Matriculas.Add(matricula);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool eliminarAlumno(int id)
        {
            try
            {
                var alumno = context.Alumnos.Where(a => a.Id == id).FirstOrDefault();

                if (alumno != null)
                {
                    var matriculas = context.Matriculas.Where(m => m.AlumnoId == id);
                    foreach (Matricula m in matriculas)
                    {
                        var calificaciones = context.Calificacions.Where(c => c.MatriculaId == m.Id);
                        context.Calificacions.RemoveRange(calificaciones);
                    }
                    context.Matriculas.RemoveRange(matriculas);
                    context.Alumnos.Remove(alumno);
                    context.SaveChanges();
                    return true;
                }
                else { return false;}
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
