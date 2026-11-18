using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class movimiento : MonoBehaviour
{
    private Rigidbody rb;
    public float velocidad = 10f;

    // Salto.
    public float fuerzaSalto = 5f;
    public float distanciaSuelo = 0.65f;
    private bool enSuelo;
    private bool saltoSolicitado;

    // Puntaje.
    private int puntos = 0;
    public Text puntaje;
    public int metaPuntos = 12;
    private bool reiniciando;

    // Caída y reaparición.
    public float limiteCaida = -5f;
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    // Cuenta regresiva.
    public float tiempoLimite = 90f;
    public Text textoTiempo;
    private float tiempoRestante;

    // Explosión.
    public ParticleSystem explosion;
    public float esperaReinicio = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        posicionInicial = rb.position;
        rotacionInicial = rb.rotation;
        tiempoRestante = tiempoLimite;

        if (explosion != null)
        {
            explosion.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );
        }

        ActualizarPuntaje();
        ActualizarTiempo();
    }

    void Update()
    {
        if (reiniciando)
        {
            return;
        }

        tiempoRestante = Mathf.Max(
            0f,
            tiempoRestante - Time.deltaTime
        );

        ActualizarTiempo();

        if (tiempoRestante <= 0f)
        {
            StartCoroutine(ExplotarYReiniciar());
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            saltoSolicitado = true;
        }
    }

    private void FixedUpdate()
    {
        if (reiniciando)
        {
            return;
        }

        if (rb.position.y < limiteCaida)
        {
            Reaparecer();
            return;
        }

        enSuelo = Physics.Raycast(
            transform.position,
            Vector3.down,
            distanciaSuelo,
            ~0,
            QueryTriggerInteraction.Ignore
        );

        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");

        Vector3 dirMovimiento = new Vector3(
            movimientoH,
            0f,
            movimientoV
        );

        rb.AddForce(dirMovimiento * velocidad);

        if (saltoSolicitado && enSuelo)
        {
            rb.AddForce(
                Vector3.up * fuerzaSalto,
                ForceMode.Impulse
            );
        }

        saltoSolicitado = false;
    }

    private void Reaparecer()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.position = posicionInicial;
        rb.rotation = rotacionInicial;

        saltoSolicitado = false;
        enSuelo = false;

        Debug.Log("El jugador cayó al vacío y volvió al inicio.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (reiniciando || !other.gameObject.activeSelf)
        {
            return;
        }

        int valor;

        if (other.CompareTag("Coleccionable"))
        {
            valor = 1;
        }
        else if (other.CompareTag("Especial"))
        {
            valor = 3;
        }
        else
        {
            return;
        }

        other.gameObject.SetActive(false);
        puntos += valor;

        ActualizarPuntaje();

        Debug.Log(
            "Recogido: " + other.gameObject.name +
            " | Valor: +" + valor +
            " | Puntaje acumulado: " + puntos
        );

        if (puntos >= metaPuntos)
        {
            reiniciando = true;
            Debug.Log("¡Meta alcanzada! Reiniciando la partida.");
            ReiniciarPartida();
        }
    }

    private void ActualizarPuntaje()
    {
        if (puntaje != null)
        {
            puntaje.text = "Puntos: " + puntos + " / " + metaPuntos;
        }
    }

    private void ActualizarTiempo()
    {
        if (textoTiempo != null)
        {
            textoTiempo.text =
                "Tiempo: " + Mathf.CeilToInt(tiempoRestante);

            textoTiempo.color = tiempoRestante <= 10f
                ? Color.red
                : Color.white;
        }
    }

    private IEnumerator ExplotarYReiniciar()
    {
        reiniciando = true;
        saltoSolicitado = false;

        // Detener la pelota durante la explosión.
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        // Ocultar la pelota sin desactivar este script.
        foreach (Renderer visual in GetComponentsInChildren<Renderer>())
        {
            visual.enabled = false;
        }

        foreach (Collider colision in GetComponentsInChildren<Collider>())
        {
            colision.enabled = false;
        }

        // Reproducir la explosión donde estaba la pelota.
        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.Play();
        }

        if (textoTiempo != null)
        {
            textoTiempo.text = "¡Tiempo agotado!";
        }

        Debug.Log("Tiempo agotado. La pelota explotó.");

        yield return new WaitForSeconds(esperaReinicio);

        ReiniciarPartida();
    }

    private void ReiniciarPartida()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}