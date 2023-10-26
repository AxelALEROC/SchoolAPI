using AccesoDatos.Models;
using AccesoDatos.Operacion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private AlumnoDAO alumnoDAO = new AlumnoDAO();
        [HttpGet("alumnosprofesor")]
        public List<AlumnoProfesor> GetAlumnosProfesor(string usuario)
        {
            return alumnoDAO.seleccionarAlumnosProfesor(usuario);
        }
        [HttpGet("alumno")]
        public Alumno getAlumno(int id)
        {
            return alumnoDAO.seleccionar(id);
        }
        [HttpPut("alumno")]
        public bool updateAlumno([FromBody]Alumno alumno)
        {
            return alumnoDAO.Update(alumno.Id, alumno.Dni, alumno.Nombre, alumno.Direccion, alumno.Edad, alumno.Email);
        }
        [HttpPost("alumno")]
        public bool insertarMatricula([FromBody]Alumno alumno, int id_asig)
        {
            return alumnoDAO.agregarYMatricular(alumno.Dni, alumno.Nombre, alumno.Direccion, alumno.Edad, alumno.Email, id_asig);
        }
        [HttpDelete("alumno")]
        public bool deleteAlumno(int id)
        {
            return alumnoDAO.eliminarAlumno(id);
        }

    }
}
