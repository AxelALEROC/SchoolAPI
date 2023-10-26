using AccesoDatos.Context;
using AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Operacion
{
    public class CalificacionDAO
    {
        ProyectoContext context = new ProyectoContext();
        public List<Calificacion> seleccionar(int id)
        {
            var calificaciones = context.Calificacions.Where(c => c.MatriculaId == id).ToList();
            return calificaciones;
        }
        public bool addCalificacion(Calificacion calif)
        {
            try
            {
                context.Calificacions.Add(calif);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool deleteCalificacion(int id)
        {
            try
            {
                var calificacion = context.Calificacions.Where(c => c.Id == id).FirstOrDefault();
                if (calificacion != null)
                {
                    context.Calificacions.Remove(calificacion);
                    context.SaveChanges();  
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
