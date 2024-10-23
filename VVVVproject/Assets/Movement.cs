using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    public float speed = 3f;
    private bool gravityInverted = false;
    private bool canJump = true;
    public Animator animController;

    private Vector3 respawnPosition; // Guarda la posición de respawn
    private bool firstSpawn = true; // Indica si es el primer spawn en esta escena

    private void Awake()
    {
        
        if (GameManager.gameManager != null && GameManager.gameManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameManager.gameManager.SetPlayer(this); // Almacena la referencia al jugador
            DontDestroyOnLoad(this.gameObject); // No destruir este objeto al cargar una nueva escena
        }
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        animController = GetComponent<Animator>();

        // Establecer la posición de respawn al inicio de la escena
        SetInitialSpawnPosition();
    }

    private void SetInitialSpawnPosition()
    {
        GameObject spawnObject = GameObject.Find("Respawn"); // Suponiendo que hay un objeto con el nombre "Spawn"
        if (spawnObject != null && firstSpawn == true)
        {
            transform.position = spawnObject.transform.position; // Coloca al jugador en la posición del spawn
            respawnPosition = transform.position; // Actualiza la posición de respawn
        }
        else
        {
            Debug.LogError("No se ha encontrado un objeto 'Spawn' en la escena.");
        }
    }

    public void Respawn()
    {
        transform.position = respawnPosition; // Regresa al jugador a la última posición guardada
    }

    private void Update()
    {
        // Movimiento del jugador
        if (Input.GetKey(KeyCode.D))
        {
            _rb.velocity = new Vector3(speed, _rb.velocity.y, 0);
            animController.SetBool("Corriendo", true);
            if (_spriteRenderer.flipX) _spriteRenderer.flipX = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _rb.velocity = new Vector3(-speed, _rb.velocity.y, 0);
            animController.SetBool("Corriendo", true);
            if (!_spriteRenderer.flipX) _spriteRenderer.flipX = true;
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            animController.SetBool("Corriendo", false);
        }

        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            _rb.AddForce(new Vector3(_rb.velocity.x, speed * 50, 0));
            animController.SetTrigger("Saltando");
            canJump = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlipGravity();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    void FlipGravity()
    {
        gravityInverted = !gravityInverted;
        _rb.gravityScale = gravityInverted ? -1 : 1;
        _spriteRenderer.flipY = gravityInverted;
    }

    public void Die()
    {
        Debug.Log("El personaje ha muerto.");
        respawnPosition = GameObject.Find("Respawn").transform.position; // Guarda la nueva posición de respawn
        transform.position = respawnPosition; // Teletransporta al jugador a la nueva posición
    }

    public void UpdateSpawnPosition(Vector3 newPosition)
    {
        respawnPosition = newPosition; // Actualiza la posición de respawn cuando el jugador toca un objeto que cambia la escena
    }
}