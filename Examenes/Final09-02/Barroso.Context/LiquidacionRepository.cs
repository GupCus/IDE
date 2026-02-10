using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Barroso.Clases;
using Microsoft.EntityFrameworkCore;

namespace Barroso.Context
{
    public static class LiquidacionRepository
    {
        public static async Task<Liquidacion> CrearLiquidacionAsync(Liquidacion liquidacion)
        {
            using (LiquidacionesContext bd = new())
            {
                bd.Liquidaciones.Add(liquidacion);
                await bd.SaveChangesAsync();
                return liquidacion;
            }
        }


        public static async Task<List<Liquidacion>> EncontrarLiquidacionesAsync()
        {
            using (LiquidacionesContext bd = new())
            {
                return await bd.Liquidaciones.ToListAsync();
            }
        }

        public static async Task<List<Liquidacion>> EncontrarLiquidacionesPorEstadoAsync(string estado)
        {
            using (LiquidacionesContext bd = new())
            {
                return await bd.Liquidaciones.Where(l => l.Estado == estado).ToListAsync();
            }
        }

        public static async Task<Liquidacion?> EncontrarLiquidacionAsync(int? idL)
        {
            using (LiquidacionesContext bd = new())
            {
                return await bd.Liquidaciones
                    .FirstOrDefaultAsync(l => l.Id == idL);
            }
        }

        public static async Task ActualizarLiquidacionAsync(Liquidacion l)
        {
            using (LiquidacionesContext bd = new())
            {
                Liquidacion? liquidacionact = await bd.Liquidaciones.FindAsync(l.Id);
                if (liquidacionact != null)
                {
                    liquidacionact.Estado = "Liquidada";
                    await bd.SaveChangesAsync();
                }
                else
                {
                    throw new NoEncontradoException("No se encontró la liquidacion");
                }
            }
        }
    }

}
