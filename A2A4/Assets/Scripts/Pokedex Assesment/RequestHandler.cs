using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class RequestHandler : MonoBehaviour
{
    private const string PokeApiUrl = "https://pokeapi.co/api/v2/pokemon/";

    public async Task<Pokemon> GetPokemonData(string pokemonName)
    {
        string url = PokeApiUrl + pokemonName.ToLower();

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            var operation = webRequest.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {webRequest.error}");
                return null;
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                Pokemon pokemon = JsonUtility.FromJson<Pokemon>(jsonResponse);
                return pokemon;
            }
        }
    }

    public async Task<Sprite> GetPokemonSprite(string spriteUrl)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(spriteUrl))
        {
            var operation = webRequest.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {webRequest.error}");
                return null;
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }
    }
}
