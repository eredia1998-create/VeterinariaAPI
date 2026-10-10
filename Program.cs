using VeterinariaAPI.Models;
using VeterinariaAPI.DTOs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// Lista de mascotas en memoria.
var mascotas = new List<Mascota>();
var siguienteId = 1;

// POST: Crear una mascota.
app.MapPost("/mascotas", (CrearMascotaDto dto) =>
{
if (string.IsNullOrWhiteSpace(dto.Nombre) ||
string.IsNullOrWhiteSpace(dto.Especie) ||
string.IsNullOrWhiteSpace(dto.Raza) ||
string.IsNullOrWhiteSpace(dto.Sexo) ||
string.IsNullOrWhiteSpace(dto.Propietario) ||
dto.Edad < 0 || dto.Edad > 100)
{
return Results.BadRequest(new
{
mensaje = "Los datos de la mascota son inválidos."
});
}

var mascota = new Mascota
{
    Id = siguienteId++,
    Nombre = dto.Nombre.Trim(),
    Especie = dto.Especie.Trim(),
    Raza = dto.Raza.Trim(),
    Edad = dto.Edad,
    Sexo = dto.Sexo.Trim(),
    Propietario = dto.Propietario.Trim()
};

mascotas.Add(mascota);

return Results.Created($"/mascotas/{mascota.Id}", mascota);
});

// GET: Consultar todas las mascotas o buscar por nombre.
app.MapGet("/mascotas", (string? nombre) =>
{
if (string.IsNullOrWhiteSpace(nombre))
{
return Results.Ok(mascotas);
}

var resultado = mascotas
    .Where(m => m.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
    .ToList();

if (resultado.Count == 0)
{
    return Results.NotFound(new
    {
        mensaje = "No se encontraron mascotas con ese nombre."
    });
}

return Results.Ok(resultado);

});

// GET: Consultar una mascota por ID.
app.MapGet("/mascotas/{id:int}", (int id) =>
{
var mascota = mascotas.FirstOrDefault(m => m.Id == id);

if (mascota is null)
{
    return Results.NotFound(new
    {
        mensaje = "Mascota no encontrada."
    });
}

return Results.Ok(mascota);
});

// PUT: Actualizar todos los datos.
app.MapPut("/mascotas/{id:int}", (int id, ActualizarMascotaDto dto) =>
{
var mascota = mascotas.FirstOrDefault(m => m.Id == id);

if (mascota is null)
{
    return Results.NotFound(new
    {
        mensaje = "Mascota no encontrada."
    });
}

if (string.IsNullOrWhiteSpace(dto.Nombre) ||
    string.IsNullOrWhiteSpace(dto.Especie) ||
    string.IsNullOrWhiteSpace(dto.Raza) ||
    string.IsNullOrWhiteSpace(dto.Sexo) ||
    string.IsNullOrWhiteSpace(dto.Propietario) ||
    dto.Edad < 0 || dto.Edad > 100)
{
    return Results.BadRequest(new
    {
        mensaje = "Los datos de actualización son inválidos."
    });
}

mascota.Nombre = dto.Nombre.Trim();
mascota.Especie = dto.Especie.Trim();
mascota.Raza = dto.Raza.Trim();
mascota.Edad = dto.Edad;
mascota.Sexo = dto.Sexo.Trim();
mascota.Propietario = dto.Propietario.Trim();

return Results.Ok(mascota);

});

// PATCH: Actualizar solo los campos enviados.
app.MapPatch("/mascotas/{id:int}", (int id, ActualizarParcialMascotaDto dto) =>
{
var mascota = mascotas.FirstOrDefault(m => m.Id == id);

if (mascota is null)
{
    return Results.NotFound(new
    {
        mensaje = "Mascota no encontrada."
    });
}

if ((dto.Nombre is not null && string.IsNullOrWhiteSpace(dto.Nombre)) ||
    (dto.Especie is not null && string.IsNullOrWhiteSpace(dto.Especie)) ||
    (dto.Raza is not null && string.IsNullOrWhiteSpace(dto.Raza)) ||
    (dto.Sexo is not null && string.IsNullOrWhiteSpace(dto.Sexo)) ||
    (dto.Propietario is not null && string.IsNullOrWhiteSpace(dto.Propietario)) ||
    (dto.Edad.HasValue && (dto.Edad.Value < 0 || dto.Edad.Value > 100)))
{
    return Results.BadRequest(new
    {
        mensaje = "Los campos enviados contienen datos inválidos."
    });
}

if (dto.Nombre is not null) mascota.Nombre = dto.Nombre.Trim();
if (dto.Especie is not null) mascota.Especie = dto.Especie.Trim();
if (dto.Raza is not null) mascota.Raza = dto.Raza.Trim();
if (dto.Edad.HasValue) mascota.Edad = dto.Edad.Value;
if (dto.Sexo is not null) mascota.Sexo = dto.Sexo.Trim();
if (dto.Propietario is not null) mascota.Propietario = dto.Propietario.Trim();

return Results.Ok(mascota);

});

// DELETE: Eliminar una mascota.
app.MapDelete("/mascotas/{id:int}", (int id) =>
{
    var mascota = mascotas.FirstOrDefault(m => m.Id == id);

    if (mascota is null)
    {
        return Results.NotFound(new
        {
            mensaje = "Mascota no encontrada."
        });
    }

    mascotas.Remove(mascota);

    return Results.Ok(new
    {
        mensaje = "Mascota eliminada correctamente."
    });
});


app.Run();
