using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_principal : MonoBehaviour
{
    // Funci�n para cargar la escena 0
    public void Inicir_Juego()
    {
        Debug.Log("esta clicando el boton");
        ActivarDontDestroyOnLoad();
        SceneManager.LoadScene(1);
    }

    // Funci�n para llamar a la funci�n Die() del Player
    public void KillPlayer()
    {
        // Mensaje de depuraci�n en la consola
        Debug.Log("Cerrando el juego...");

        // Si est� en el Editor, se detiene la ejecuci�n
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // Cerrar la aplicaci�n
#endif
    }

    private void ActivarDontDestroyOnLoad()
    {
        Debug.Log("Activando GameObjects de DontDestroyOnLoad");
        // Obtener todos los objetos en DontDestroyOnLoad
        GameObject[] objetosDontDestroy = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objetosDontDestroy)
        {
            // Verificar si el objeto est� en la carpeta DontDestroyOnLoad
            if (obj.scene.name == "DontDestroyOnLoad")
            {
                obj.SetActive(true); // Activar el GameObject
            }
        }
    }
}