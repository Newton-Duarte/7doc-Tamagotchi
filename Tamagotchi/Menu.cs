using System;
using System.Collections.Generic;
using System.Text.Json;
using Tamagotchi.Model;
using Tamagotchi.Services;
using static Program;
using static Tamagotchi.Services.PokemonService;

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
                    PokemonService.GetPokemons();
                    Divider();
                    string answer = Console.ReadLine();
                    var response = PokemonService.GetPokemon(answer);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var pokemonRecord = JsonSerializer.Deserialize<PokemonRecord>(response.Content);
                        var abilities = new List<string>();
                        foreach ( var ability in pokemonRecord.abilities)
                        {
                            abilities.Add(ability.ability.name);
                        }
                        pokemon = new Pokemon(pokemonRecord.name, pokemonRecord.height, pokemonRecord.weight, abilities);
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
                    Console.WriteLine(pokemon.ToString());
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
            Console.WriteLine($"1 - Saber mais sobre o {Capitalize(pokemon.Name)}");
            Console.WriteLine($"2 - Adotar {Capitalize(pokemon.Name)}");
            Console.WriteLine("3 - Voltar");

            int pokemonMenuOption = int.Parse(Console.ReadLine());
            HandlePokemonMenuOption(pokemonMenuOption);
        }
    }
}
