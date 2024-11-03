using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionesBotones : MonoBehaviour
{
    

    // Función para cargar la escena 0
    public void GoToSceneZero()
    {
        Debug.Log("esta clicando el boton");
        GameManager.gameManager.ChangeScene(1);
        
            // Llama a la función Die del script del Player
            

            // Salir automáticamente de la pausa
            // Se asume que isPaused y Menu_Pausa son accesibles de alguna manera aquí.
            PauseManager pauseManager = GetComponentInParent<PauseManager>();
            if (pauseManager != null)
            {
                pauseManager.isPaused = false; // Cambiar el estado de pausa
                pauseManager.menuPausa.SetActive(false); // Desactivar el menú de pausa
                Time.timeScale = 1; // Restablecer el tiempo

                // Asegurarse de que el cursor se desbloquee
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        
                
          
        
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
