using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallDeath : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el GameObject que colisiona tenga la etiqueta "Player"
        {
            other.SendMessage("Die");
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
