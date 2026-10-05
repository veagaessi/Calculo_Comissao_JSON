using System.Text.Json;
public class Venda
{
    public required string Vendedor { get; set; }
    public decimal ValorVenda { get; set; }
    public decimal Comissao { get; set; }
    public decimal ValorComissao { get; set; }
}

public class VendaJson
{
    public required string Vendedor { get; set; }
    public decimal ValorVenda { get; set; }
}

public class CalculoComissao // irá calcular o valor da comissão com base no valor da venda
{
    public static decimal CalcularComissao(decimal ValorVenda) // verifica valor da venda e aplica comissão se for o caso
    {
        if (ValorVenda <= 100)
        {
            return 0m; // sem comissão
        }
        else if (ValorVenda <= 500)
        {
            return 1m; // 1% de comissão
        }
        else
        {
            return 5m; // 5% de comissão
        }
    }
    public static decimal CalcularValorComissao(decimal ValorVenda) // calcula o valor da comissão
    {
        if (ValorVenda <= 100)
        {
            return ValorVenda * 0m; // sem comissão
        }
        else if (ValorVenda <= 500)
        {
            return ValorVenda * 0.01m; // 1% de comissão
        }
        else
        {
            return ValorVenda * 0.05m; // 5% de comissão
        }
    }
}

class Programa
{
    static void Main()
    {
        try
        {
            string caminho = "C:\\Users\\hugov\\Desktop\\coisas\\Teste\\ConsoleApp1\\vendas.json";
            
            if (!File.Exists(caminho))
            {
                Console.WriteLine($"Arquivo {caminho} não encontrado!");
                return;
            }

            string jsonString = File.ReadAllText(caminho);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var vendasDoJson = JsonSerializer.Deserialize<List<VendaJson>>(jsonString, options);

            var vendasCalculadas = vendasDoJson.Select(item => new Venda
            {
                Vendedor = item.Vendedor,
                ValorVenda = item.ValorVenda,
                Comissao = CalculoComissao.CalcularComissao(item.ValorVenda),
                ValorComissao = CalculoComissao.CalcularValorComissao(item.ValorVenda)
            }).ToList();

            Console.WriteLine("--- VENDAS INDIVIDUAIS ---");
            foreach (var v in vendasCalculadas)
            {
                Console.WriteLine($"Vendedor: {v.Vendedor}, Valor: R$ {v.ValorVenda:F2}, Comissão: {v.Comissao}%, Valor Comissão: R$ {v.ValorComissao:F2}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}
