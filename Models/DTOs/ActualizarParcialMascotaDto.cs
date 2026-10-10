namespace VeterinariaAPI.DTOs;

public class ActualizarParcialMascotaDto
{
    public string? Nombre { get; set; }

    public string? Especie { get; set; }

    public string? Raza { get; set; }

    public int? Edad { get; set; }

    public string? Sexo { get; set; }

    public string? Propietario { get; set; }

    public string? TelefonoPropietario { get; set; }
}