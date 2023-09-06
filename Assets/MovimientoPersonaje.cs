using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorMovimiento : MonoBehaviour
{
    public string nombre;
    public float velocidad = 5.0f;
    public float fuerzaSalto = 5.0f;
    public float sensibilidadMouse = 2.0f; // Sensibilidad del mouse para rotación
    public Transform camara; // Referencia al transform de la cámara

    private Rigidbody fisicas;
    private float rotacionX = 0.0f;

    void Start()
    {
        // Obtener el componente Rigidbody del GameObject
        fisicas = GetComponent<Rigidbody>();

        // Bloquear y ocultar el cursor del mouse para una experiencia de juego más inmersiva
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Si no se asigna una cámara en el Inspector, intenta encontrarla automáticamente
        if (camara == null)
        {
            camara = Camera.main.transform;
        }
    }

    void Update()
    {
        // Capturar la entrada del jugador para el movimiento
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calcular el vector de movimiento relativo al jugador
        Vector3 movimiento = new Vector3(horizontal, 0, vertical).normalized * velocidad * Time.deltaTime;
        transform.Translate(movimiento);

        // Controlar el salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Aplicar una fuerza hacia arriba al Rigidbody para simular el salto
            fisicas.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }

        // Capturar el movimiento del mouse para la rotación horizontal
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotación horizontal del jugador
        transform.Rotate(Vector3.up * mouseX * sensibilidadMouse);

        // Rotación vertical de la cámara
        rotacionX -= mouseY * sensibilidadMouse;
        rotacionX = Mathf.Clamp(rotacionX, -90, 90); // Limitar la rotación vertical de la cámara
        camara.localRotation = Quaternion.Euler(rotacionX, 0, 0);
    }
}
