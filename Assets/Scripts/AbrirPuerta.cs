using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AbrirPuerta : MonoBehaviour
{
     public int puntosNecesarios = 200;
    private float tiempoApertura = 1f;
    public bool abierta;
    private NavMeshObstacle obstaculo;

    private MeshCollider collider;

    public string Spawn1;
    public string Spawn2;
    public string Spawn3;


    private GameObject jugador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        abierta = false;
        jugador = GameObject.Find("Player");
        obstaculo = GetComponent<NavMeshObstacle>();
        collider = GetComponent<MeshCollider>();
    }

    public void Abrir()
    {
     
        if (abierta)
        {
            return;
        }

        abierta = true;
        obstaculo.enabled = false;
        collider.enabled = false;
        
        Quaternion rotacionInicio = transform.localRotation;
        Quaternion rotacionFin = Quaternion.Euler(0, -180f, 0);

        if(Spawn1 != "null")
        jugador.GetComponent<CrearZombies>().DesbloquearSpawn(Spawn1);
        if(Spawn2 != "null")
        jugador.GetComponent<CrearZombies>().DesbloquearSpawn(Spawn2);
        if(Spawn3 != "null")
        jugador.GetComponent<CrearZombies>().DesbloquearSpawn(Spawn3);


       
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
