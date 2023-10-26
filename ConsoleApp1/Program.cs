// See https://aka.ms/new-console-template for more information
using AccesoDatos.Models;
using AccesoDatos.Operacion;
using System.Xml;

AlumnoDAO opAlumno = new AlumnoDAO();//C R U D

//opAlumno.insertar("45464T", "Jose Antonio", "C/Argetina", 22, "jarruis@gmail.com"); INSETA

Console.WriteLine("-------------------------------------------------------------");

//opAlumno.Update(15, "45464T", "Jose Antonio Aduriz", "C/Argetina", 22, "jarruis@gmail.com"); ACTUALIZA
//opAlumno.Delete(15); BORRA

var alumnos = opAlumno.seleccionarTodos();

foreach (var alumno in alumnos)
{
    Console.WriteLine(alumno.Nombre);
}

Console.WriteLine("-----------------");
var alumno1 = opAlumno.seleccionar(1);
if (alumno1 != null)
{
    Console.WriteLine($"El alumno con id = 1 es: {alumno1.Nombre}");
}
else
{ Console.WriteLine("No existe"); }


Console.WriteLine("----------------------");


var alumasig = opAlumno.selecAlumnosAsignaturas();
foreach(AlumnoAsignatura alasig in alumasig)
{ Console.WriteLine($"{alasig.NombreAlumno}, {alasig.NombreAsignatura}"); }

Console.ReadKey();