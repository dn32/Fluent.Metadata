using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Fluent.Metadata;

public static class FluentMetadados
{
    private static ConcurrentDictionary<object, MetadadosDeUmObjeto> Metadados { get; set; } = new();

    public static T ObterMetadado<T>(this object objetoAlvo, string chave, bool gerarExcecaoCasoNaoExista = false) where T : class
    {
        if (objetoAlvo is null or ValueType) throw new InvalidOperationException("Somente objetos não nulos e do tipo referência podem ter metadados");

        if (Metadados.TryGetValue(objetoAlvo, out var meta))
        {
            return meta.ObterMetadado(chave, gerarExcecaoCasoNaoExista) as T;
        }
        else if (gerarExcecaoCasoNaoExista)
        {
            throw new MetadadoNaoEncontrado(chave);
        }

        return null;
    }

    public static EscopoDeMetadado DefinirMetadado<T>(this T objetoAlvo, string chave, object valor, bool sobrescrever = true)
    {
        if (objetoAlvo is null or ValueType) throw new InvalidOperationException("Somente objetos não nulos e do tipo referência podem ter metadados");
        Metadados.AddOrUpdate(objetoAlvo, (_) => new MetadadosDeUmObjeto(chave, valor), (_, valorSalvo) => valorSalvo.DefinirMetadado(chave, valor, sobrescrever));
        return new EscopoDeMetadado
        {
            ObjetoAlvo = objetoAlvo,
            Chave = chave
        };
    }

    public static bool EliminarMetadado<T>(this T objetoAlvo, string chave)
    {
        if (objetoAlvo is null or ValueType) return false;

        if (Metadados.TryGetValue(objetoAlvo, out var meta))
        {
            var sucesso = meta.Eliminar(chave);
            if(meta.QuantidadeRestante == 0)
            {
               return Metadados.TryRemove(objetoAlvo, out _);
            }

            return sucesso;
        }

        return false;
    }

    static FluentMetadados()
    {
        _ = MonitorarGC();
    }

    internal static async Task MonitorarGC()
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            var objetos = Metadados.Values.ToList();
            if (objetos.Any())
            {
                foreach (var obj in objetos)
                {
                    GCHandle handle = GCHandle.Alloc(obj, GCHandleType.Normal);
                    var a = handle.IsAllocated;
                    Console.WriteLine(a);
                }
            }
            else
            {
                Console.WriteLine("Dic vazio");
            }
        }
    }
}
