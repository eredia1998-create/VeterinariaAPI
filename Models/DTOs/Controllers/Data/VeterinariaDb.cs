using VeterinariaAPI.Models;

namespace VeterinariaAPI.Data;

public static class VeterinariaDb
{
    public static List<Mascota> Mascotas { get; set; } =
    [
        new Mascota
        {
            Id = 1,
            Nombre = "Max",
            Especie = "Perro",
            Raza = "Labrador",
            Edad = 3,
            Sexo = "Macho",
            Propietario = "Juan Pérez",
            TelefonoPropietario = "809-555-1234"
        },
        new Mascota
        {
            Id = 2,
            Nombre = "Luna",
            Especie = "Gato",
            Raza = "Siamés",
            Edad = 2,
            Sexo = "Hembra",
            Propietario = "María Rodríguez",
            TelefonoPropietario = "809-555-5678"
        }
    ];
}