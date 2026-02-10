namespace Barroso.Clases
{
    public class Liquidacion
    {
        public int? Id { get; set; }
        public DateOnly Fecha { get; set; }
        public string Empleado { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }

    }
}
