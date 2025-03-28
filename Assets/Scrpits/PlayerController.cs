using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour {

    /*******************************
     * VARIABLES PARA EL RayCast *
     *******************************/

    // Ajustaré el tamaño del BoxCast en el Inspector
    public Vector2 boxSize;
    // Ajustaré la distancia del BoxCast en el Inspector
    public float castDistance = 0.1f;
    // La Layer que le asignaré al suelo en el Inspector
    public LayerMask groundLayer;



    /******************************
     * VARIABLES PARA COYOTE TIME *
     ******************************/

    // Tiempo que el jugador puede saltar después de tocar el suelo
    float coyoteTime = 0.12f;
    // Contador para controlar el tiempo de coyoteTime
    float coyoteTimeCounter = 0;

    // Variable para controlar si hemos saltado recientemente
    bool justJumped = false;
    // Tiempo en el que ignoramos isGrounded() tras saltar
    float groundBufferTime = 0.1f;
    // Contador para controlar el tiempo de groundBufferTime
    float groundBufferTimeCounter = 0f;



    void Start() {

    }


    void Update() {
        // Movemos el jugador hacia la izquierda
        if (Input.GetKey("left")) {
            gameObject.GetComponent<Rigidbody2D>().AddForce
                (new Vector2(-600f * Time.deltaTime, 0));

            // Cambiao el parámetro moving del animator a true
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            
            // Hacemos que el sprite mire hacia la izquierda
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        // Movemos el jugador hacia la derecha
        if (Input.GetKey("right")) {
            gameObject.GetComponent<Rigidbody2D>().AddForce
                (new Vector2(600f * Time.deltaTime, 0));

            // Cambiao el parámetro moving del animator a true
            gameObject.GetComponent<Animator>().SetBool("moving", true);

            // Hacemos que el sprite mire hacia la derecha
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        // Compruebo si toco suelo y si no he saltado recientemente
        if (isGrounded() && groundBufferTimeCounter <= 0) {
            // Cuando toco suelo, reseteo el counter de coyoteTime
            coyoteTimeCounter = coyoteTime;
            // Ya podemos detectar el suelo correctamente
            justJumped = false;
            gameObject.GetComponent<Animator>().SetBool("jumping", false);
        } else {
            // Cuando estoy en el aire, reduzco el counter de coyoteTime
            coyoteTimeCounter -= Time.deltaTime;
            gameObject.GetComponent<Animator>().SetBool("jumping", true);
        }

        // Para saltar lo hago con GetKeyDown para que solo lo detecte una vez
        if (Input.GetKeyDown("space") && coyoteTimeCounter > 0) {
            coyoteTimeCounter = 0;
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 650f);
            // Restará el tiempo asignado en groundBufferTimeCounter
            justJumped = true;
            // Añado 0.1f de tiempo para que no se detecte el suelo
            groundBufferTimeCounter = groundBufferTime;
        } 

        // Resto el tiempo que me permite ignorar la colision del suelo
        if (justJumped) {
            groundBufferTimeCounter -= Time.deltaTime;
        }


        // Si no pulsamos ninguna tecla, moving = false
        if (!Input.GetKey("left") && !Input.GetKey("right")) {
            gameObject.GetComponent<Animator>().SetBool("moving", false);
        }
    }

    // Método para comprobar si estamos en el suelo
    public bool isGrounded() {
        // Hago uso de un BoxCast para comprpbar si estamos en el suelo
        return (Physics2D.BoxCast(transform.position, boxSize, 0,
            Vector2.down, castDistance, groundLayer));
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}


