﻿using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Fluent.Metadata;

internal class MetadadosDeUmObjeto
{
    private ConcurrentDictionary<string, object> DicionarioDeMetadadosDeUmObjeto { get; set; }

    public MetadadosDeUmObjeto(string chave, object valor)
    {
        DicionarioDeMetadadosDeUmObjeto = new();
        DicionarioDeMetadadosDeUmObjeto.TryAdd(chave, valor);
    }

    public object ObterMetadado(string chave, bool gerarExcecaoCasoNaoExista = false)
    {
        if (DicionarioDeMetadadosDeUmObjeto.TryGetValue(chave, out var metadados))
        {
            return metadados;
        }
        else if (gerarExcecaoCasoNaoExista)
        {
            throw new MetadadoNaoEncontrado(chave);
        }

        return null;
    }

    public MetadadosDeUmObjeto DefinirMetadado(string chave, object valor, bool sobrescrever = true)
    {
        DicionarioDeMetadadosDeUmObjeto.AddOrUpdate(chave, valor, (key, valorSalvo) =>
        {
            return sobrescrever ? valor : valorSalvo;
        });

        return this;
    }

    public bool Eliminar(string chave)
    {
        return DicionarioDeMetadadosDeUmObjeto.TryRemove(chave, out _);
    }

    public int QuantidadeRestante => DicionarioDeMetadadosDeUmObjeto.Count;
}
