using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    public float speed = 3f;
    private bool gravityInverted = false;
    private Vector3 respawnPosition = new Vector3(-8.16f, -1.01f, 0);
    public Animator animController;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        animController = GetComponent<Animator>();
        //velocitat (en el start, la velocitat es manté sempre que no hi hagi col·lisions)

        //_rb.velocity = new Vector2(speed, _rb.velocity.y);
        //acceleració
        //_rb.AddForce(new Vector2(speed, 0));
        //_rb.AddForce(new Vector2(speed, 9), ForceMode2D.Impulse);//pots triar forceMode*/
    }
    private void Update()
    {
        //canvi de posició
        //if (Input.GetKey(KeyCode.D))
        //transform.position = transform.position + new Vector3(speed, 0,0) * Time.deltaTime;
        //Canvi de velocitat
        if (Input.GetKey(KeyCode.D))
        {
            _rb.velocity = new Vector3(speed, _rb.velocity.y, 0);
            animController.SetBool("Corriendo",true);

            // Cambiar la orientación solo si no está mirando hacia la derecha
            if (_spriteRenderer.flipX == true)
            {
                _spriteRenderer.flipX = false; // No voltear el sprite al moverse a la derecha
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            _rb.velocity = new Vector3(-speed, _rb.velocity.y, 0);
            animController.SetBool("Corriendo", true);

            // Cambiar la orientación solo si no está mirando hacia la izquierda
            if (_spriteRenderer.flipX == false)
            {
                _spriteRenderer.flipX = true; // Voltear el sprite al moverse a la izquierda
            }
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            animController.SetBool("Corriendo", false);
        }
        //força o acceleració
        //if (Input.GetKey(KeyCode.D))
        //_rb.AddForce(new Vector3(speed, 0, 0));
        //salt amb força, se utilizara para el jetpack mas adelante
        //if (Input.GetKey(KeyCode.W))
        //_rb.AddForce(new Vector3(0, speed, 0));
        if (Input.GetKeyDown(KeyCode.W))
        {
            _rb.AddForce(new Vector3(_rb.velocity.x, speed * 50, 0));
            animController.SetTrigger("Saltando");
        }
            
        //salt amb velocitat
        //if (Input.GetKeyDown(KeyCode.W))
        //  _rb.velocity = new Vector3(0, speed * 4, 0);


        // cambio de gravedad
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlipGravity();
        }
    }

    void FlipGravity()
    {
        // Invertimos el valor de la gravedad (de 1 a -1 y viceversa)
        gravityInverted = !gravityInverted;
        _rb.gravityScale = gravityInverted ? -1 : 1;

        // Invertimos el valor del eje Y en el flip del SpriteRenderer
        _spriteRenderer.flipY = gravityInverted;
    }

    public void Die()
    {
        Debug.Log("El personaje ha muerto.");
        transform.position = respawnPosition;
        

    }
}

