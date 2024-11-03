using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public float chaseSpeed = 5f;
    public float detectionDistance = 3f;
    public LayerMask obstacleLayer;

    private Vector2 direction = Vector2.right;
    private bool isChasingPlayer = false;

    // Referencia al objeto Inicio_Raycast
    public GameObject inicioRaycast; // Asigna este objeto en el Inspector

    void Update()
    {
        Move();
        DetectObstacleOrPlayer();
    }

    void Move()
    {
        float currentSpeed = isChasingPlayer ? chaseSpeed : speed;
        transform.Translate(direction * currentSpeed * Time.deltaTime);
    }

    void DetectObstacleOrPlayer()
    {
        // Determina la posici�n inicial del Raycast, sumando o restando 4 en el eje X seg�n la direcci�n
        float offsetX = direction.x > 0 ? 4 : -4; // Suma 4 si la direcci�n es positiva, resta 4 si es negativa
        Vector2 raycastOrigin = new Vector2(transform.position.x + offsetX, transform.position.y);

        // Utiliza la posici�n ajustada para el Raycast
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, direction, detectionDistance, obstacleLayer);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject != gameObject) // Ignora su propio collider
            {
                if (hit.collider.CompareTag("Player"))
                {
                    isChasingPlayer = true;
                }
                else if (((1 << hit.collider.gameObject.layer) & obstacleLayer) != 0)
                {
                    Debug.Log(hit.collider.tag);
                    direction = -direction; // Cambia la direcci�n

                    // Voltea el sprite
                    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.flipX = !spriteRenderer.flipX; // Alterna el flipX
                    }

                    isChasingPlayer = false; // Sale del modo persecuci�n
                }
            }
        }
        else
        {
            isChasingPlayer = false; // Si no hay colisi�n, desactiva el modo persecuci�n
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Determina la posici�n inicial del Raycast, sumando o restando 4 en el eje X seg�n la direcci�n
        float offsetX = direction.x > 0 ? 4 : -4; // Suma 4 si la direcci�n es positiva, resta 4 si es negativa
        Vector2 raycastOrigin = new Vector2(transform.position.x + offsetX, transform.position.y);

        // Dibuja la l�nea desde la posici�n ajustada
        Gizmos.DrawLine(raycastOrigin, (Vector3)raycastOrigin + (Vector3)direction * detectionDistance);
    }
}
