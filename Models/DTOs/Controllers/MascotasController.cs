using Microsoft.AspNetCore.Mvc;
using VeterinariaAPI.Data;
using VeterinariaAPI.DTOs;
using VeterinariaAPI.Models;

namespace VeterinariaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MascotasController : ControllerBase
{
    // GET: api/mascotas
    [HttpGet]
    public ActionResult<IEnumerable<Mascota>> ObtenerMascotas()
    {
        return Ok(VeterinariaDb.Mascotas);
    }

    // GET: api/mascotas/1
    [HttpGet("{id}")]
    public ActionResult<Mascota> ObtenerMascota(int id)
    {
        var mascota = VeterinariaDb.Mascotas
            .FirstOrDefault(m => m.Id == id);

        if (mascota == null)
        {
            return NotFound(new
            {
                mensaje = "Mascota no encontrada."
            });
        }

        return Ok(mascota);
    }

    // POST: api/mascotas
    [HttpPost]
    public ActionResult<Mascota> CrearMascota(
        CrearMascotaDto datos)
    {
        var nuevoId = VeterinariaDb.Mascotas.Count == 0
            ? 1
            : VeterinariaDb.Mascotas.Max(m => m.Id) + 1;

        var mascota = new Mascota
        {
            Id = nuevoId,
            Nombre = datos.Nombre,
            Especie = datos.Especie,
            Raza = datos.Raza,
            Edad = datos.Edad,
            Sexo = datos.Sexo,
            Propietario = datos.Propietario,
            TelefonoPropietario = datos.TelefonoPropietario
        };

        VeterinariaDb.Mascotas.Add(mascota);

        return CreatedAtAction(
            nameof(ObtenerMascota),
            new { id = mascota.Id },
            mascota);
    }

    // PUT: api/mascotas/1
    [HttpPut("{id}")]
    public ActionResult<Mascota> ActualizarMascota(
        int id,
        ActualizarMascotaDto datos)
    {
        var mascota = VeterinariaDb.Mascotas
            .FirstOrDefault(m => m.Id == id);

        if (mascota == null)
        {
            return NotFound(new
            {
                mensaje = "Mascota no encontrada."
            });
        }

        mascota.Nombre = datos.Nombre;
        mascota.Especie = datos.Especie;
        mascota.Raza = datos.Raza;
        mascota.Edad = datos.Edad;
        mascota.Sexo = datos.Sexo;
        mascota.Propietario = datos.Propietario;
        mascota.TelefonoPropietario = datos.TelefonoPropietario;

        return Ok(mascota);
    }

    // PATCH: api/mascotas/1
    [HttpPatch("{id}")]
    public ActionResult<Mascota> ActualizarParcialmente(
        int id,
        ActualizarParcialMascotaDto datos)
    {
        var mascota = VeterinariaDb.Mascotas
            .FirstOrDefault(m => m.Id == id);

        if (mascota == null)
        {
            return NotFound(new
            {
                mensaje = "Mascota no encontrada."
            });
        }

        if (datos.Nombre != null)
            mascota.Nombre = datos.Nombre;

        if (datos.Especie != null)
            mascota.Especie = datos.Especie;

        if (datos.Raza != null)
            mascota.Raza = datos.Raza;

        if (datos.Edad.HasValue)
            mascota.Edad = datos.Edad.Value;

        if (datos.Sexo != null)
            mascota.Sexo = datos.Sexo;

        if (datos.Propietario != null)
            mascota.Propietario = datos.Propietario;

        if (datos.TelefonoPropietario != null)
            mascota.TelefonoPropietario =
                datos.TelefonoPropietario;

        return Ok(mascota);
    }

    // DELETE: api/mascotas/1
    [HttpDelete("{id}")]
    public IActionResult EliminarMascota(int id)
    {
        var mascota = VeterinariaDb.Mascotas
            .FirstOrDefault(m => m.Id == id);

        if (mascota == null)
        {
            return NotFound(new
            {
                mensaje = "Mascota no encontrada."
            });
        }

        VeterinariaDb.Mascotas.Remove(mascota);

        return NoContent();
    }
}