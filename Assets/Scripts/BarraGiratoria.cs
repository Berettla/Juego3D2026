using UnityEngine;

public class BarraGiratoria : MonoBehaviour
{
    public float gradosPorSegundo = 60f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Quaternion giro = Quaternion.Euler(0f, gradosPorSegundo * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * giro);
    }
}