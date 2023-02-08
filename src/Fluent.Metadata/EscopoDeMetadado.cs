using System;

namespace Fluent.Metadata;

public class EscopoDeMetadado : IDisposable
{
    public object ObjetoAlvo { get; set; }
    public string Chave { get;   set; }

    public void Dispose()
    {
        ObjetoAlvo.EliminarMetadado(Chave);
    }
}
