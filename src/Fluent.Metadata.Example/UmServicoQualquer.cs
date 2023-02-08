namespace Fluent.Metadata.Example;

public class UmServicoQualquer
{
    public PessoaDeExemplo P1 { get; }

    public UmServicoQualquer(PessoaDeExemplo p1)
    {
        P1 = p1;
    }

    public void UmMetodoQualquer()
    {
        P1.DefinirMetadado("tempo", new Tempo { MomentoDeInicioDoMetodo = DateTime.Now, Fator = 1 });
        P1.DefinirMetadado("nome", nameof(UmServicoQualquer));
    }
}
