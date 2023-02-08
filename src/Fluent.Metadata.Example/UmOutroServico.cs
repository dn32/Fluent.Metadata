namespace Fluent.Metadata.Example;

public class UmOutroServico
{
    public void UmOutroMetodoQualquerAsync(PessoaDeExemplo p1)
    {
        var tempo = p1.ObterMetadado<Tempo>("tempo");
        var nome = p1.ObterMetadado<string>("nome");
        tempo.UltimaOcorrencia = DateTime.Now;
        var decorrido = ((int)tempo.UltimaOcorrencia.Subtract(tempo.MomentoDeInicioDoMetodo).TotalSeconds);
        Console.WriteLine($"{p1.Nome}-{nome}: {decorrido * tempo.Fator}");
    }
}