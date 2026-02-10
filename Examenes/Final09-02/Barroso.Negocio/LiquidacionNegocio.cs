using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Barroso.DTOs;

namespace Barroso.Negocio
{
    public static class LiquidacionDTONegocio
    {
        public async static Task<IEnumerable<LiquidacionDTO>> GetAll()
        {
            var response = await Conexion.Cliente.GetAsync("Liquidacion");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<LiquidacionDTO>>();
            }

            return [];
        }

        public async static Task<IEnumerable<LiquidacionDTO>> GetPorEstado(string estado)
        {
            var response = await Conexion.Cliente.GetAsync($"../estado/{estado}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<LiquidacionDTO>>();
            }

            return [];
        }

        public async static Task<Boolean> Post(LiquidacionCreateDTO l)
        {
            var response = await Conexion.Cliente.PostAsJsonAsync("Liquidacion",l);
            return response.IsSuccessStatusCode;
        }

        public async static Task<Boolean> Put(LiquidacionDTO l)
        {
            var response = await Conexion.Cliente.PutAsJsonAsync($"Liquidacion/{l.Id}", l);
            return response.IsSuccessStatusCode;
        }

    }

}
