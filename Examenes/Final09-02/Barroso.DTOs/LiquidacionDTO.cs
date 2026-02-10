namespace Barroso.DTOs
{
    public class LiquidacionDTO
    {
        public int Id { get; set; }
        public DateOnly Fecha { get; set; }
        public string Empleado { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }

    }
    public class LiquidacionCreateDTO
    {
        public DateOnly Fecha { get; set; }
        public string Empleado { get; set; }
        public decimal Monto { get; set; }

    }
}
