using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movpersonaje : MonoBehaviour
{
    public float velocidad = 5f;
    public float multiplicador = 5f;

    public static bool miraDerecha = true;

    private Rigidbody rb;
    private Animator animatorController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animatorController = GetComponent<Animator>();
    }

    void Update()
    {
        float movimientoVertical = Input.GetAxis("Horizontal"); // Se intercambia Horizontal por Vertical
        float movimientoHorizontal = Input.GetAxis("Vertical"); // Se intercambia Vertical por Horizontal

        // Invierte la dirección del movimiento vertical
        movimientoVertical *= -1f;

        Vector3 direccionMovimiento = new Vector3(movimientoHorizontal, 0, movimientoVertical).normalized;

        // Control de flipX del sprite y la animación de caminar
        if (movimientoHorizontal < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
            miraDerecha = false;
        }
        else if (movimientoHorizontal > 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
            miraDerecha = true;
        }

        // Control de la animación
        animatorController.SetBool("activaCamina", direccionMovimiento.magnitude >= 0.1f);

        // Verificar la posición Y para detección de muerte
        if (transform.position.y < -40)
        {
            // Cambiar a la escena "carta2"
            SceneManager.LoadScene("carta2");
        }
    }

    void FixedUpdate()
    {
        // Mueve el personaje en la dirección calculada
        float movimientoVertical = Input.GetAxis("Horizontal");
        float movimientoHorizontal = Input.GetAxis("Vertical");

        // Invierte la dirección del movimiento vertical
        movimientoVertical *= -1f;

        Vector3 direccionMovimiento = new Vector3(movimientoHorizontal, 0, movimientoVertical).normalized;
        Vector3 movimiento = direccionMovimiento * velocidad * Time.fixedDeltaTime;

        // Movimiento con física
        rb.MovePosition(rb.position + movimiento);

        // Rotación del personaje en el eje Y sin afectar el movimiento
        if (direccionMovimiento.magnitude >= 0.1f)
        {
            // Calcula la rotación hacia la dirección del movimiento
            Quaternion rotacion = Quaternion.LookRotation(direccionMovimiento);
            rb.MoveRotation(Quaternion.Euler(0, rotacion.eulerAngles.y, 0));
        }
    }
     // Método para detectar colisiones y ver si las paredes están correctamente detectadas
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisión con: " + collision.gameObject.name);
    }
}
