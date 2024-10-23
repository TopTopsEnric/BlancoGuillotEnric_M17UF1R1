using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambiadorScene : MonoBehaviour
{


    public int sceneToLoad; // Número de la escena a cargar

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto con el que colisionamos tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("El personaje cambiando escena.");
            GameManager.gameManager.ChangeScene(sceneToLoad);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
