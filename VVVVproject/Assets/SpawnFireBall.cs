using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFireBall : MonoBehaviour
{
    
    public float speed = 30f; // Velocidad del proyectil

    private void Start()
    {
        // Inicia la corutina para lanzar proyectiles

        StartCoroutine(SpawnProjectiles());
    }
    

     public void ReturnProjectile(GameObject go)
    {
        go.SetActive(false);
        GameManager.gameManager.Push(go);
    }

    private IEnumerator SpawnProjectiles()
    {
        while (true) // Bucle infinito para lanzar proyectiles cada 4 segundos
        {
            GameObject proyectil = GameManager.gameManager.Pop(); // Saca un proyectil del GameManager
            if (proyectil != null)
            {
                // Configura la posici�n inicial del proyectil
                proyectil.transform.position = transform.position;
                proyectil.transform.rotation = transform.rotation;

                // Comienza a mover el proyectil
                StartCoroutine(MoveProjectile(proyectil));
            }
            yield return new WaitForSeconds(4f); // Espera 4 segundos antes de lanzar el siguiente proyectil
        }
    }

    private IEnumerator MoveProjectile(GameObject proyectil)
    {
        // Mueve el proyectil en la direcci�n negativa de x
        while (proyectil.transform.position.x > -10f) // Cambia -10f seg�n sea necesario para el l�mite
        {
            proyectil.transform.position += Vector3.left * speed * Time.deltaTime; // Avanza en x negativo
            yield return null; // Espera hasta el siguiente frame
        }

        // Cuando el proyectil sale del l�mite, devuelve el proyectil al GameManager
        ReturnProjectile(proyectil);
    }
}
