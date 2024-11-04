using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_principal : MonoBehaviour
{
    // Función para cargar la escena 0
    public void Inicir_Juego()
    {
        Debug.Log("esta clicando el boton");
        ActivarDontDestroyOnLoad();
        SceneManager.LoadScene(1);
    }

    // Función para llamar a la función Die() del Player
    public void KillPlayer()
    {
        // Mensaje de depuración en la consola
        Debug.Log("Cerrando el juego...");

        // Si está en el Editor, se detiene la ejecución
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // Cerrar la aplicación
#endif
    }

    private void ActivarDontDestroyOnLoad()
    {
        Debug.Log("Activando GameObjects de DontDestroyOnLoad");
        // Obtener todos los objetos en DontDestroyOnLoad
        GameObject[] objetosDontDestroy = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objetosDontDestroy)
        {
            // Verificar si el objeto está en la carpeta DontDestroyOnLoad
            if (obj.scene.name == "DontDestroyOnLoad")
            {
                obj.SetActive(true); // Activar el GameObject
            }
        }
    }
}