using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        var client = new RestClient($"https://pokeapi.co/api/v2/pokemon");
        var request = new RestRequest("", Method.Get);
        var response = client.Execute(request);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var json = JsonSerializer.Deserialize<PokeAPIResponse>(response.Content);
            foreach (var pokemon in json.results)
            {
                System.Console.WriteLine(pokemon.name);
            }
        }
        else
        {
            System.Console.WriteLine(response.ErrorMessage);
        }
    }

    public record struct PokeAPIResponse(
        int count,
        Pokemon[] results
    );

    public record struct Pokemon (
        string name
    );
}