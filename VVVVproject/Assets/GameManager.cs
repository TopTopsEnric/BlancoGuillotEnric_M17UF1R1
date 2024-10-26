using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager; // Instancia estática del GameManager
    private Movement playerMovement;// Referencia al script de movimiento del jugador
    private Vector3 lastposition;
    private Vector3 postionTrue;
    public GameObject projectilePrefab; // Prefab del proyectil que deseas usar
    public int initialPoolSize = 5;
    public Stack<GameObject> stack;
    private bool salida;
   

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
            DontDestroyOnLoad(this.gameObject);
            // No destruir este objeto al cargar una nueva escena
             // Inicializa el stack
        }
    }


    private void InicializarPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject go = Instantiate(projectilePrefab);
            if (go != null)
            {
                go.SetActive(false);
                stack.Push(go);
                Debug.Log("va de puta madre el prefab");
            }
            else
            {
                Debug.Log("algo falla con el prefab");
            }
        }
    }

    public void SetPlayer(Movement movement)
    {
        playerMovement = movement; // Almacena una referencia al script de movimiento del jugador
    }

    public void setPosition(Vector3 position) 
    {
        postionTrue = lastposition;
        lastposition = position;
    }
    public void SetSalida(bool sortida)
    {
        salida= sortida;
    }

    public Vector3 getPosition()
    {
        return lastposition;
    }
    public Vector3 getTruePosition()
    {
        if (salida == true)
        {
            Debug.Log("era una salida");
            postionTrue.x += 1;
        }
        else
        {
            Debug.Log("No era una salida");
            postionTrue.x -= 1;
        }
        Debug.Log("Se ha enviado la posicion"+postionTrue);
        return postionTrue;
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

        
            stack = new Stack<GameObject>();
            InicializarPool(); // Mueve la creación de los proyectiles aquí
        
    }

    public void ResetFirstTimeFlag(string sceneName)
    {
        PlayerPrefs.DeleteKey(sceneName + "_firstTime");
        PlayerPrefs.Save();
    }
    public void Push(GameObject go)
    {
        stack.Push(go);
    }

    public GameObject Pop()
    {
        if (stack.Count == 0)
        {
            Debug.LogWarning("La pila está vacía, no se puede hacer Pop.");
            return null;
        }

        GameObject go = stack.Pop();
        if (go == null)
        {
            Debug.LogError("El objeto fue destruido antes de ser activado.");
            return null;
        }

        Debug.Log("no es nulo es:" + go);
        go.SetActive(true); // Activa el objeto
        return go;

    }
    public void Peek()
    {
        Debug.Log(stack.Peek());
    }

    

   

   
}