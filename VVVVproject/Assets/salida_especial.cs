using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class salida_especial : MonoBehaviour
{
    private Vector3 posicion;
    public int Scenetoload;
    private string sceneToLoad;
    private bool exit = false;
    private AudioSource audioSource; // Componente de AudioSource
    private Dictionary<int, string> sceneDictionary = new Dictionary<int, string>
    {
        {0, "nivel1"},
        {1, "nivel2"},
        {2, "nivel3"},
        {3, "nivel4"},
        {4, "nivel5"},
        // Agrega aquí más escenas según sea necesario
    };

    private void Start()
    {
        // Obtener el componente AudioSource en el mismo objeto
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No se encontró un componente AudioSource en el objeto.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CompareTag("salida"))
            {
                exit = true;
            }
            else if (CompareTag("entrada"))
            {
                exit = false;
            }

            Debug.Log("El personaje cambiando escena.");
            posicion = other.transform.position;
            Debug.Log("se envia posicion:" + posicion);
            GameManager.gameManager.setPosition(posicion);
            Debug.Log("se envia salida:" + exit);
            GameManager.gameManager.SetSalida(exit);

            if (sceneDictionary.TryGetValue(Scenetoload, out sceneToLoad))
            {
                // Destruye el jugador y cambia la escena
               // Destruye el jugador
                PlaySoundAndChangeScene(sceneToLoad); // Cambia la escena
            }
            else
            {
                Debug.LogError("Código de escena no encontrado en el diccionario.");
            }
        }
    }

    private void PlaySoundAndChangeScene(string sceneName)
    {
        if (audioSource != null)
        {
            // Reproduce el sonido una vez
            audioSource.PlayOneShot(audioSource.clip);

            // Comienza la corutina para cambiar de escena después de un corto retraso
            StartCoroutine(ChangeSceneAfterAudio(sceneName, audioSource.clip.length));
        }
        else
        {
            // Si no hay sonido, cambia la escena de inmediato
            StartCoroutine(ChangeSceneAndSetPosition(sceneName));
        }
    }

    private IEnumerator ChangeSceneAfterAudio(string sceneName, float delay)
    {
        // Espera la duración del sonido antes de cambiar de escena
        yield return new WaitForSeconds(delay);

        // Cambia la escena asíncronamente
        yield return ChangeSceneAndSetPosition(sceneName);
    }

    private IEnumerator ChangeSceneAndSetPosition(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Espera a que la escena se cargue completamente
        while (!asyncLoad.isDone)
        {
            Debug.Log("Progreso de carga: " + asyncLoad.progress);
            yield return null;
        }
    }
}
