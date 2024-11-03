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
}
