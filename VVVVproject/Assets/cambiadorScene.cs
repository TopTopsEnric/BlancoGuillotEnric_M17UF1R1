using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambiadorScene : MonoBehaviour
{


    private Vector3 posicion;
    public int Scenetoload;
    private string sceneToLoad;
    private bool exit=false;
    private Dictionary<int, string> sceneDictionary = new Dictionary<int, string>
    {
        {0, "nivel1"},
        {1, "nivel2"},
        {2, "nivel3"},
        {3, "nivel4"},
        // Agrega aqu� m�s escenas seg�n sea necesario
    };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(CompareTag("salida"))
            {
                exit = true;
            }  
            else if(CompareTag("entrada"))
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
                // Cambiar de escena de forma as�ncrona
                StartCoroutine(ChangeSceneAndSetPosition( sceneToLoad));
            }
            else
            {
                Debug.LogError("C�digo de escena no encontrado en el diccionario.");
            }
        }
    }

    // Corutina que cambia de escena y aplica la posici�n cuando la nueva escena est� cargada
    private IEnumerator ChangeSceneAndSetPosition( string sceneName)
    {
        // Cambiar la escena as�ncronamente
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Esperar a que la escena se cargue completamente
        while (!asyncLoad.isDone)
        {
            Debug.Log("Progreso de carga: " + asyncLoad.progress);
            yield return null; // Espera un frame
        }

       
    }
}
