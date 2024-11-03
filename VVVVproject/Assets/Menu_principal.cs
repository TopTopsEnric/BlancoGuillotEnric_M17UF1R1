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
}
