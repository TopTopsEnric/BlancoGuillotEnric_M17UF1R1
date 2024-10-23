using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambiadorScene : MonoBehaviour
{


    private Vector3 posicion;
    public int Scenetoload;
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El personaje cambiando escena.");
            posicion = other.transform.position;

            // Cambiar de escena de forma as�ncrona
            StartCoroutine(ChangeSceneAndSetPosition(posicion, sceneToLoad));
        }
    }

    // Corutina que cambia de escena y aplica la posici�n cuando la nueva escena est� cargada
    private IEnumerator ChangeSceneAndSetPosition(Vector3 position, string sceneName)
    {
        // Cambiar la escena as�ncronamente
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Esperar a que la escena se cargue completamente
        while (!asyncLoad.isDone)
        {
            yield return null; // Espera un frame
        }

        // Una vez que la escena ha cargado completamente, aplicar la posici�n
        GameManager.gameManager.setPosition(position);
        Debug.Log("Posici�n aplicada despu�s de cambiar de escena.");
    }
}
