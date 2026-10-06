namespace ProjectSaborLatino.Models;

// Nombres de los roles como constantes, para no escribirlos a mano en cada [Authorize]
public static class Roles
{
    public const string Superadministrador = "Superadministrador";
    public const string Administrador = "Administrador";
    public const string Trabajador = "Trabajador";
    public const string Cliente = "Cliente";

    public static readonly string[] Todos =
        [Superadministrador, Administrador, Trabajador, Cliente];
}