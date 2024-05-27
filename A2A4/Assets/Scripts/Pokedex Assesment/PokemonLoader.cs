using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonLoader : MonoBehaviour
{
    [SerializeField] TMP_InputField pokeInput;
    [SerializeField] TextMeshProUGUI pokeName;
    [SerializeField] TextMeshProUGUI pokeID;
    [SerializeField] TextMeshProUGUI pokeHeight;
    [SerializeField] TextMeshProUGUI pokeWeight;
    [SerializeField] TextMeshProUGUI pokeTypes;
    [SerializeField] Image pokeImage;

    private RequestHandler requestHandler;

    void Start()
    {
        requestHandler = gameObject.AddComponent<RequestHandler>();
    }

    public void RequestPokemon()
    {
        LoadPokemonData(pokeInput.text);
    }

    private async void LoadPokemonData(string pokemonName)
    {
        Pokemon pokemon = await requestHandler.GetPokemonData(pokemonName);

        if (pokemon != null)
        {
            Debug.Log($"Pokemon ID: {pokemon.id}");
            Debug.Log($"Pokemon Name: {pokemon.name}");
            Debug.Log($"Pokemon Base Experience: {pokemon.base_experience}");
            Debug.Log($"Pokemon Height: {pokemon.height}");
            Debug.Log($"Pokemon Weight: {pokemon.weight}");

            pokeName.text = pokemon.name;
            pokeID.text = $"ID {pokemon.id}";
            pokeHeight.text = $"{(pokemon.height / 10.0f).ToString("F1")} m"; // Converted to meters with one decimal place
            pokeWeight.text = $"{(pokemon.weight / 10.0f).ToString("F1")} kg"; // Converted to kilograms with one decimal place

            string pokeTypesString = "";
            foreach (var type in pokemon.types)
            {
                Debug.Log($"Type: {type.type.name}");
                pokeTypesString += type.type.name + " ";
            }
            pokeTypes.text = pokeTypesString.Trim();

            // Fetch and set the sprite
            Sprite pokeSprite = await requestHandler.GetPokemonSprite(pokemon.sprites.front_default);
            if (pokeSprite != null)
            {
                pokeImage.sprite = pokeSprite;
            }
        }
        else
        {
            Debug.LogError("Failed to load Pokemon data.");
        }
    }
}
