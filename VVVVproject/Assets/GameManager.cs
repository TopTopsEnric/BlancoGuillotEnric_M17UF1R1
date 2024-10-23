using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager; // Instancia est�tica del GameManager
    private Movement playerMovement; // Referencia al script de movimiento del jugador
    private Stack<GameObject> stack;

    private void Awake()
    {
        // Si ya hay una instancia y no es la actual, destruir este objeto
        if (gameManager != null && gameManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gameManager = this; // Establece la instancia del GameManager
            DontDestroyOnLoad(this.gameObject); // No destruir este objeto al cargar una nueva escena
            stack = new Stack<GameObject>(); // Inicializa el stack
        }
    }

    public void SetPlayer(Movement movement)
    {
        playerMovement = movement; // Almacena una referencia al script de movimiento del jugador
    }

    public void RespawnPlayer()
    {
        if (playerMovement != null)
        {
            playerMovement.Respawn(); // Llama al m�todo Respawn del jugador
        }
    }

    public void ChangeScene(int sceneIndex)
    {
       
        SceneManager.LoadScene(sceneIndex);
        RespawnPlayer(); // Respawn al jugador despu�s de cambiar la escena
    }
}