namespace Fluent.Metadata.Example;

internal class Program
{
    static void Main(string[] args)
    {
        var p1 = new PessoaDeExemplo { Nome = "Um nome" };

        Iniciar(p1);

        Console.ReadKey();
    }

    private static async void Iniciar(PessoaDeExemplo p1)
    {
        using var metaTempo = p1.DefinirMetadado("tempo", new Tempo { MomentoDeInicioDoMetodo = DateTime.Now, Fator = 1 });
        using var metaNome = p1.DefinirMetadado("nome", nameof(UmServicoQualquer));

        var servico = new UmServicoQualquer(p1);
        servico.UmMetodoQualquer();

        var outroServico = new UmOutroServico();
        outroServico.UmOutroMetodoQualquerAsync(p1);

        await Task.Delay(TimeSpan.FromSeconds(10));
    }
}
