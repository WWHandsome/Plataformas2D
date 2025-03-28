using UnityEngine;

public class FruitLogic : MonoBehaviour {

    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            // La imagen de la fruta desparece
            GetComponent<SpriteRenderer>().enabled = false;
            // Se reproduce el efecto visual, que es el primer hijo de la fruta
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            // Se reproduce el sonido de la fruta
            audioSource.Play();
            // Destruimos la fruta después de 0.3 segundos
            Destroy(gameObject, 0.3f);
        }
    }
}
