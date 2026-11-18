using UnityEngine;

public class Rotador : MonoBehaviour
{
    public Vector3 velocidadGiro = new Vector3(15f, 30f, 45f);

    void Update()
    {
        transform.Rotate(velocidadGiro * Time.deltaTime);
    }
}