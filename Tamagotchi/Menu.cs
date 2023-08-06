using System;
using System.Text.Json;
using static Program;

namespace Tamagotchi
{
    internal class Menu
    {
        public static Pokemon pokemon { get; set; }

        public static void Open()
        {
            Divider();
            Console.WriteLine("Bem-vindo(a) ao -->TAMAGOTCHI<--");
            Divider();

            Console.WriteLine($"{name}, você deseja:");
            Console.WriteLine("----------------- MENU -----------------");
            Console.WriteLine("1 - Adotar um mascote virtual");
            Console.WriteLine("2 - Ver seus mascotes");
            Console.WriteLine("3 - Sair");

            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine("Escolha um pokemon:");
                    Divider();
                    GetPokemons();
                    Divider();
                    string answer = Console.ReadLine();
                    var response = GetPokemon(answer);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        pokemon = JsonSerializer.Deserialize<Pokemon>(response.Content);
                        PokemonMenu();

                    }
                    else
                    {
                        var errorMessage = response.ErrorMessage;
                        Console.WriteLine(errorMessage != null ? errorMessage : "Pokemon não encontrrado");
                        Open();
                    }
                    break;
                case 2:
                    Divider();
                    ShowAdoptedPokemons();
                    Divider();
                    Open();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    Open();
                    break;
            }
        }

        private static void HandlePokemonMenuOption(int option)
        {
            switch (option)
            {
                case 1:
                    Divider();
                    PokemonInfo(pokemon);
                    PokemonMenu();
                    break;
                case 2:
                    Divider();
                    adoptedPokemons.Add(pokemon);
                    Console.WriteLine("Mascote adotado com sucesso!");
                    Open();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    Open();
                    break;
            }
        }

        static void Divider()
        {
            Console.WriteLine("----------------------------------------");
        }

        static void PokemonMenu()
        {
            Divider();
            Console.WriteLine($"{name}, você deseja:");
            Console.WriteLine($"1 - Saber mais sobre o {pokemon.name}");
            Console.WriteLine($"2 - Adotar {pokemon.name}");
            Console.WriteLine("3 - Voltar");

            int pokemonMenuOption = int.Parse(Console.ReadLine());
            HandlePokemonMenuOption(pokemonMenuOption);
        }
    }
}
