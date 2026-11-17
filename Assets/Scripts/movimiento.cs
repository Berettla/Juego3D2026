using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movimiento : MonoBehaviour
{
    private Rigidbody rb;
    public float velocidad = 10f;
    // para el salto
    public float fuerzaSalto = 5f;
    public float distanciaSuelo = 0.65f;   // radio de la esfera (0.5) + margen
    private bool enSuelo;
    private bool saltoSolicitado;

    // para el puntaje
    private int contador_coleccionables = 0;
    public Text puntaje;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            saltoSolicitado = true;
        }
    }

    private void FixedUpdate()
    {
        // 1. para saber si está en el suelo
        enSuelo = Physics.Raycast(transform.position, Vector3.down,
                                  distanciaSuelo, ~0, QueryTriggerInteraction.Ignore);

        // 2. Movimiento horizontal
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");
        Vector3 dirMovimiento = new Vector3(movimientoH, 0.0f, movimientoV);
        rb.AddForce(dirMovimiento * velocidad);

        // 3. para realizar el salto, si se solicitó y está en el suelo
        if (saltoSolicitado && enSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
        saltoSolicitado = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coleccionable"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("Recogidos: " + ++contador_coleccionables);
            puntaje.text = contador_coleccionables + "";
        }
    }
}