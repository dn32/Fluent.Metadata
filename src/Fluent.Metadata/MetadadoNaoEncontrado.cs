using System;

namespace Fluent.Metadata;

public class MetadadoNaoEncontrado : Exception
{
    public MetadadoNaoEncontrado(string chave) : base($"Nenhum metadado encontrado para o objeto selecionado com a chave '{chave}'.")
    {
    }
}