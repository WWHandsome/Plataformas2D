using UnityEngine;

public class CameraController : MonoBehaviour {

    // Guardamos la poscion del Player
    public Transform followPlayer;

    // Velocidad de la c�mara
    public float cameraSpeed = 0.1f;

    // Desplazamiento de la c�mara
    public Vector3 displacement;

    // Para que el movimiento de la c�mara sea lo m�s suave posible
    // lo hago en un LateUpdate
    private void LateUpdate() {
        // Guardo en un Vector la posicion del player + el desplazamiento
        Vector3 targetPosition = followPlayer.position + displacement;

        // Hago un Lerp para que la c�mara se mueva de forma suave
        Vector3 smoothPosition = Vector3.Lerp
            (transform.position, targetPosition, cameraSpeed);

        // Cambio la posici�n de la c�mara
        transform.position = smoothPosition;
    }
}

