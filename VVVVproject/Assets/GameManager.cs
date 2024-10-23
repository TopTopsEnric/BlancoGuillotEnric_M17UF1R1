using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager; // Instancia estática del GameManager
    private Movement playerMovement;// Referencia al script de movimiento del jugador
    private Vector3 lastposition;
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

    public void setPosition(Vector3 position) 
    {
        lastposition = position;
    }

    public Vector3 getPosition()
    {
        return lastposition;
    }

    public void ChangeScene(int sceneIndex)
    {
       
        SceneManager.LoadScene(sceneIndex);
        
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Desuscribirse del evento cuando este objeto sea destruido o desactivado
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    // Esta función se llama cuando la escena ha terminado de cargarse
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        // Llamar a la función Firsttime()
        playerMovement.Firsttime();

        // Establecer la posición de respawn al inicio de la escena
        playerMovement.SetInitialSpawnPosition();
    }

    void ResetFirstTimeFlag(string sceneName)
    {
        PlayerPrefs.DeleteKey(sceneName + "_firstTime");
        PlayerPrefs.Save();
    }
}