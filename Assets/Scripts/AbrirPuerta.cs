using System.Collections;
using UnityEngine;

public class AbrirPuerta : MonoBehaviour
{
     public int puntosNecesarios = 200;
    private float tiempoApertura = 1f;
    public bool abierta;

    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        abierta = false;
        player = GameObject.Find("Player");
    }

    public void Abrir()
    {
     
        if (abierta)
        {
            return;
        }

        abierta = true; 
        
        Quaternion rotacionInicio = transform.localRotation;
        Quaternion rotacionFin = Quaternion.Euler(0, -180f, 0);

       
        StartCoroutine(AnimarRotacion(rotacionInicio, rotacionFin));

        

    }

    IEnumerator AnimarRotacion(Quaternion inicio, Quaternion fin)
    {
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < tiempoApertura)
        {
            float t = tiempoTranscurrido / tiempoApertura;
            

            transform.localRotation = Quaternion.Slerp(inicio, fin, t);

            tiempoTranscurrido += Time.deltaTime;
            yield return null; 
        }

        transform.localRotation = fin; 
    }
}
