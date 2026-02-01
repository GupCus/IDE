using Clases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Context
{
    public static class LenguajeRepository
    {
        public static async Task CrearLenguajeAsync(Lenguaje lenguaje)
        {
            using (SnippetsCentreContext bd = new())
            {
                bd.Lenguajes.Add(lenguaje);
                await bd.SaveChangesAsync();
            }
        }

        public static async Task EliminarLenguajeAsync(int? id)
        {
            using (SnippetsCentreContext bd = new())
            {
                Lenguaje? lenguajeelim = await bd.Lenguajes.FindAsync(id);
                if (lenguajeelim != null)
                {
                    bd.Lenguajes.Remove(lenguajeelim);
                    await bd.SaveChangesAsync();
                }
            }
        }

        public static async Task<List<Lenguaje>> EncontrarLenguajesAsync()
        {
            using (SnippetsCentreContext bd = new())
            {
                return await bd.Lenguajes.Include(l => l.Snippets).ToListAsync();
            }
        }

        public static async Task<Lenguaje?> EncontrarLenguajeAsync(int? idL)
        {
            using (SnippetsCentreContext bd = new())
            {
                return await bd.Lenguajes
                    .Include(l => l.Snippets)
                    .FirstOrDefaultAsync(l => l.Id == idL);
            }
        }

        public static async Task ActualizarLenguajeAsync(Lenguaje l)
        {
            using (SnippetsCentreContext bd = new())
            {
                Lenguaje? lenguajeact = await bd.Lenguajes.FindAsync(l.Id);
                if (lenguajeact != null)
                {
                    lenguajeact.Nombre = l.Nombre;
                    await bd.SaveChangesAsync();
                }
            }
        }

        public static async Task CrearSnippetAsync(int? idLenguaje, Snippet snippet)
        {
            using (SnippetsCentreContext bd = new())
            {
                Lenguaje? lenguajeact = await bd.Lenguajes
                    .Include(len => len.Snippets)
                    .FirstOrDefaultAsync(len => len.Id == idLenguaje);
                
                if (lenguajeact != null)
                {
                    lenguajeact.Snippets.Add(snippet);
                    await bd.SaveChangesAsync();
                }
            }
        }

        public static async Task ActualizarSnippetAsync(Snippet snippetActualizado)
        {
            using (SnippetsCentreContext bd = new())
            {
                Snippet? snippet = await bd.Set<Snippet>().FindAsync(snippetActualizado.Id);
                
                if (snippet != null)
                {
                    snippet.Titulo = snippetActualizado.Titulo;
                    snippet.Codigo = snippetActualizado.Codigo;
                    await bd.SaveChangesAsync();
                }
            }
        }

        public static async Task EliminarSnippetAsync(int? idLenguaje, int? idSnippet)
        {
            using (SnippetsCentreContext bd = new())
            {
                Lenguaje? lenguajeact = await bd.Lenguajes
                    .Include(len => len.Snippets)
                    .FirstOrDefaultAsync(len => len.Id == idLenguaje);

                if (lenguajeact != null)
                {
                    Snippet? snippetElim = lenguajeact.Snippets
                        .FirstOrDefault(s => s.Id == idSnippet);
                    
                    if (snippetElim != null)
                    {
                        lenguajeact.Snippets.Remove(snippetElim);
                        await bd.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
