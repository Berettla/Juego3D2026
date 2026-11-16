using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class movimiento : MonoBehaviour
{
    private Rigidbody rb;//creamos el objeto
    private float velocidad = 10;
    // Start is called before the first frame update
    public Text puntaje;
    private int contador_coleccionables = 0;
    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        //Para sincronizar los frame del motor de la física
        //Variables para capturar el movimiento horizontal y vertical en nuestro teclado
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");
        //Un vector 3 es un trio de posiciones en x, y, z y corresponde al movmiento del jugador
        Vector3 dirmovimiento = new Vector3(movimientoH, 0.0f, movimientoV);
        //Asignamos el movimiento al rigidbody
        rb.AddForce(dirmovimiento*velocidad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coleccionable"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("Recogidos: " + ++contador_coleccionables);
            puntaje.text = contador_coleccionables + "";
        }
    }
}
