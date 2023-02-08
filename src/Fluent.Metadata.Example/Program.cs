namespace Fluent.Metadata.Example;

internal class Program
{
    static void Main(string[] args)
    {
        var p1 = new PessoaDeExemplo { Nome = "Um nome" };

        var servico = new UmServicoQualquer(p1);
        servico.UmMetodoQualquer();

        var outroServico = new UmOutroServico();
        _ = outroServico.UmOutroMetodoQualquerAsync(p1);

        Console.ReadKey();
    }
}
