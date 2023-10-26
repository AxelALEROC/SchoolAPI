using AccesoDatos.Models;
using AccesoDatos.Operacion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class CalificacionController : ControllerBase
    {
        private CalificacionDAO calificacionDAO = new CalificacionDAO();
        [HttpGet("calificaciones")]
        public List<Calificacion>GetCalificacion(int idMatricula)
        {
            return calificacionDAO.seleccionar(idMatricula);
        }
        [HttpPost("calificacion")]
        public bool addc([FromBody] Calificacion calif)
        {
            return calificacionDAO.addCalificacion(calif);
        }
        [HttpDelete("calificacion")]
        public bool eliminarC(int id)
        {
            return calificacionDAO.deleteCalificacion(id);
        }

    }
}
