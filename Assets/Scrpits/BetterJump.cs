using UnityEngine;

public class BetterJump : MonoBehaviour {


    float fallMultiplier = 1.5f;
    float lowJumpMultiplier = 10;
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update() {
        // Compruebo si el jugador está cayendo
        if (rb.linearVelocityY < 0) {
            // Aumento un poco la velocidad de caída
            // Es como si cayera de mi salto de manera normal
            rb.linearVelocity += Vector2.up
                * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

            // Compuebo si el jugador está subiendo y no pulsa espacio
            // Si al saltar he dejado de pulsa
        } else if (rb.linearVelocity.y > 0 && !Input.GetKey("space")) {
            // Le aplico una una fuerza considerable para que baje
            rb.linearVelocity += Vector2.up
              * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
  