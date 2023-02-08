using System.Collections.Concurrent;

namespace Fluent.Metadata;

public static class FluentMetadados
{
    private static ConcurrentDictionary<object, MetadadosDeUmObjeto> Metadados { get; set; } = new();

    public static T ObterMetadado<T>(this object objetoAlvo, string chave, bool gerarExcecaoCasoNaoExista = false) where T : class
    {
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

    public static T DefinirMetadado<T>(this T objetoAlvo, string chave, object valor, bool sobrescrever = true)
    {
        Metadados.AddOrUpdate(objetoAlvo, (_) => new MetadadosDeUmObjeto(chave, valor), (_, valorSalvo) => valorSalvo.DefinirMetadado(chave, valor, sobrescrever));
        return objetoAlvo;
    }
}
