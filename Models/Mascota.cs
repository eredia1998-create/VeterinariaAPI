namespace VeterinariaAPI.Models;

public class Mascota
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Especie { get; set; } = string.Empty;

    public string Raza { get; set; } = string.Empty;

    public int Edad { get; set; }

    public string Sexo { get; set; } = string.Empty;

    public string Propietario { get; set; } = string.Empty;

    public string TelefonoPropietario { get; set; } = string.Empty;
}