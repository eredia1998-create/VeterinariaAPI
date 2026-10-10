using System.ComponentModel.DataAnnotations;

namespace VeterinariaAPI.DTOs;

public class ActualizarMascotaDto
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public string Especie { get; set; } = string.Empty;

    [Required]
    public string Raza { get; set; } = string.Empty;

    [Range(0, 100)]
    public int Edad { get; set; }

    [Required]
    public string Sexo { get; set; } = string.Empty;

    [Required]
    public string Propietario { get; set; } = string.Empty;

    [Required]
    public string TelefonoPropietario { get; set; } = string.Empty;
}