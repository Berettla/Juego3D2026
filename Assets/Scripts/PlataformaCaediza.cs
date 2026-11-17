using System.Collections;
using UnityEngine;

public class PlataformaCaediza : MonoBehaviour
{
    public float tiempoAntesDeCaer = 0.7f;
    public float tiempoParaReaparecer = 3f;
    private bool activada;

    private void OnCollisionEnter(Collision other)
    {
        if (!activada && other.gameObject.GetComponent<movimiento>() != null)
        {
            StartCoroutine(CaerYVolver());
        }
    }

    private IEnumerator CaerYVolver()
    {
        activada = true;
        yield return new WaitForSeconds(tiempoAntesDeCaer);

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(tiempoParaReaparecer);

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        activada = false;
    }
}