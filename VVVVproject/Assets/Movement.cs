using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Movement : MonoBehaviour
{
    public static GameObject PlayerInstance;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    public float speed = 3f;
    private bool gravityInverted = false;
    private bool canJump = true;
    private Animator animController;
    private bool firstSpawn; // Indica si es el primer spawn en esta escena
    private efectosPlayer efectos; // Referencia al script de efectos de sonido

    private void Awake()
    {
        if (PlayerInstance == null)
        {
            PlayerInstance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (PlayerInstance != this.gameObject)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        animController = GetComponent<Animator>();
        efectos = GetComponent<efectosPlayer>(); // Obtiene el script de efectos de sonido
    }

    public void Firsttime()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (PlayerPrefs.GetInt(currentSceneName + "_firstTime", 1) == 1)
        {
            firstSpawn = true;
            Debug.Log("Primera vez en la escena: " + currentSceneName);
            PlayerPrefs.SetInt(currentSceneName + "_firstTime", 0);
            PlayerPrefs.Save();
        }
        else
        {
            firstSpawn = false;
            Debug.Log("Ya has visitado esta escena antes: " + currentSceneName);
        }
    }

    public void SetInitialSpawnPosition()
    {
        GameObject spawnObject = GameObject.Find("Respawn");
        if (firstSpawn)
        {
            Debug.Log("Primera vez en la escena.");
            transform.position = spawnObject.transform.position;
        }
        else
        {
            transform.position = GameManager.gameManager.getTruePosition();
        }
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

            // Emite el sonido de salto
            if (efectos != null)
            {
                efectos.PlayJumpSound();
            }
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
        transform.position = GameObject.Find("Respawn").transform.position;

        // Emite el sonido de "hit" al morir
        if (efectos != null)
        {
            efectos.PlayHitSound();
        }
    }

}