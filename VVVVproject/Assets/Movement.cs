using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Movement : MonoBehaviour
{
    public static GameObject Player;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    public float speed = 3f;
    private bool gravityInverted = false;
    private bool canJump = true;
    public Animator animController;
    public float stepInterval = 0.1f; // Intervalo de tiempo entre pasos
   

    private AudioSource audioSource;
    private float stepTimer;

    // Guarda la posición de respawn
    private bool firstSpawn ; // Indica si es el primer spawn en esta escena

    private void Awake()
    {
        
        if (Player != null && Player != this)
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
        audioSource = GetComponent<AudioSource>();
    }

    public void Firsttime()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Verificar si es la primera vez en esta escena
        if (PlayerPrefs.GetInt(currentSceneName + "_firstTime", 1) == 1)
        {
            firstSpawn = true;
            Debug.Log("Primera vez en la escena: " + currentSceneName);

            // Marcamos que ya ha entrado a la escena
            PlayerPrefs.SetInt(currentSceneName + "_firstTime", 0);
            PlayerPrefs.Save(); // Guardar cambios
        }
        else
        {
            firstSpawn = false;
            Debug.Log("Ya has visitado esta escena antes: " + currentSceneName);
        }
    }

    public void SetInitialSpawnPosition()
    {
        GameObject spawnObject = GameObject.Find("Respawn"); // Suponiendo que hay un objeto con el nombre "Spawn"
        if ( firstSpawn == true)
        {
            Debug.Log("primera vez");
            transform.position = spawnObject.transform.position; // Coloca al jugador en la posición del spawn
           
        }
        else if(firstSpawn == false)
        {
            Debug.Log("No  primera vez");
            transform.position = GameManager.gameManager.getTruePosition();
           
        }
        else
        {
            Debug.LogError("No se ha encontrado un objeto 'Respawn' en la escena.");
        }
    }

   

    private void Update()
    {
        bool isMoving = false;

        // Movimiento del jugador
        if (Input.GetKey(KeyCode.D))
        {
            _rb.velocity = new Vector3(speed, _rb.velocity.y, 0);
            animController.SetBool("Corriendo", true);
            if (_spriteRenderer.flipX) _spriteRenderer.flipX = false;
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _rb.velocity = new Vector3(-speed, _rb.velocity.y, 0);
            animController.SetBool("Corriendo", true);
            if (!_spriteRenderer.flipX) _spriteRenderer.flipX = true;
            isMoving = true;
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

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                stepTimer = stepInterval; // Reinicia el temporizador para el siguiente paso
            }
        }
        else
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            stepTimer = 0f; // Reinicia el temporizador si el jugador no se está moviendo
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
        transform.position = GameObject.Find("Respawn").transform.position;
       
    }

    
}