using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionesBotones : MonoBehaviour
{
    

    // Funci�n para cargar la escena 0
    public void GoToSceneZero()
    {
        Debug.Log("esta clicando el boton");
        GameManager.gameManager.ChangeScene(1);
        
            // Llama a la funci�n Die del script del Player
            

            // Salir autom�ticamente de la pausa
            // Se asume que isPaused y Menu_Pausa son accesibles de alguna manera aqu�.
            PauseManager pauseManager = GetComponentInParent<PauseManager>();
            if (pauseManager != null)
            {
                pauseManager.isPaused = false; // Cambiar el estado de pausa
                pauseManager.menuPausa.SetActive(false); // Desactivar el men� de pausa
                Time.timeScale = 1; // Restablecer el tiempo

                // Asegurarse de que el cursor se desbloquee
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        
                
          
        
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
