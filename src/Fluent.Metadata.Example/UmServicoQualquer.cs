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
        var nome = P1.ObterMetadado<string>("nome");
        Console.WriteLine(nome);
    }
}
