using System.Collections;
using UnityEngine;

public class Disparar : MonoBehaviour
{
     [Header("Referencias")]
    public Camera camaraFPS;              // La cámara principal del jugador
    public Transform puntoDeDisparo;      // Objeto vacío en la boca del cañón
    public ParticleSystem flashDisparo;

    public LineRenderer tracer;

    [Header("Parámetros")]
    public float rangoMaximo = 100f;     // Rango máximo para la puntería
    public float rangoDeDisparo = 150f;  // Rango máximo para el proyectil

    public int daño;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        daño = 25;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Disparo()
    {
        RaycastHit hitPunteria;
        Vector3 objetivo; 

        if (Physics.Raycast(camaraFPS.transform.position, camaraFPS.transform.forward, out hitPunteria, rangoMaximo))
        {
         
            objetivo = hitPunteria.point;
        }
        else
        {

            objetivo = camaraFPS.transform.position + camaraFPS.transform.forward * rangoMaximo;
        }

  
        RaycastHit hitDisparo;
        Vector3 direccionDisparo = (objetivo - puntoDeDisparo.position).normalized;


        float distancia = Vector3.Distance(puntoDeDisparo.position, objetivo);
        distancia = Mathf.Min(distancia, rangoDeDisparo);
        flashDisparo.Play();
        StartCoroutine(MostrarTracer(puntoDeDisparo.position, objetivo));


        if (Physics.Raycast(puntoDeDisparo.position, direccionDisparo, out hitDisparo, distancia))
        {
            GameObject objetoImpactado = hitDisparo.collider.gameObject;

 
            if (objetoImpactado.CompareTag("Enemigo"))
            {
                if (objetoImpactado.name == "Bip001")
                {
                  
                    GetComponent<ControladorPlayer>().puntos += 300;
                    objetoImpactado.GetComponentInParent<ControladorZombie>().HacerDaño(daño * 2);

                }
                else if (objetoImpactado.name == "Body_01_tanktop")
                {
                   
                    GetComponent<ControladorPlayer>().puntos += 100;
                    objetoImpactado.GetComponentInParent<ControladorZombie>().HacerDaño(daño);

                }
            }

            
        }
        
    }

    IEnumerator MostrarTracer(Vector3 inicio, Vector3 fin)
    {
        tracer.SetPosition(0, inicio);
        tracer.SetPosition(1, fin);
        tracer.enabled = true;

        yield return new WaitForSeconds(0.05f);

        tracer.enabled = false;
    }
}
