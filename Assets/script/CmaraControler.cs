using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmaraControler : MonoBehaviour
{
    public Transform player; // El transform del personaje
    public float sensitivity = 10f; // Sensibilidad del ratón
    public Vector3 offset; // Offset de la cámara respecto al personaje

    private float pitch = -250f; // Rotación en el eje X (vertical)
    private float yaw = 0f; // Rotación en el eje Y (horizontal)

    void Start()
    {
        // Inicializar el offset como la posición actual de la cámara respecto al personaje
        offset = transform.position - player.position;
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor al centro de la pantalla

        // Aplicar la rotación inicial a la cámara
        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }

    void Update()
    {
        // Capturar el movimiento del ratón
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;

        // Limitar el ángulo vertical para evitar que la cámara se voltee completamente
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // Aplicar la rotación a la cámara
        transform.rotation = Quaternion.Euler(pitch, yaw, 0);

        // Mantener la cámara en la posición adecuada respecto al personaje
        Vector3 posicionDeseada = player.position + offset;
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, 0.125f);
        transform.position = posicionSuavizada;
        
    }
}
