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

    private int puntos = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Disparo()
    {
        RaycastHit hitPunteria;
        Vector3 objetivo; // El punto final al que debe apuntar el disparo

        // 1. Raycast de Puntería (Desde el centro de la cámara)
        if (Physics.Raycast(camaraFPS.transform.position, camaraFPS.transform.forward, out hitPunteria, rangoMaximo))
        {
            // El jugador está apuntando a un objeto.
            // Establece el objetivo como el punto de impacto.
            objetivo = hitPunteria.point;
        }
        else
        {
            // El jugador no está apuntando a un objeto dentro del rango (al aire).
            // Establece el objetivo como un punto lejano en la dirección de la cámara.
            objetivo = camaraFPS.transform.position + camaraFPS.transform.forward * rangoMaximo;
        }

        // --- Lógica del Disparo Real ---

        RaycastHit hitDisparo;
        Vector3 direccionDisparo = (objetivo - puntoDeDisparo.position).normalized;

        // 2. Raycast de Disparo (Desde la boca del cañón hacia el objetivo)
        // La distancia será la distancia entre el arma y el objetivo, o el rango de disparo, el que sea menor.
        float distancia = Vector3.Distance(puntoDeDisparo.position, objetivo);
        distancia = Mathf.Min(distancia, rangoDeDisparo);
        flashDisparo.Play();
        StartCoroutine(MostrarTracer(puntoDeDisparo.position, objetivo));


        if (Physics.Raycast(puntoDeDisparo.position, direccionDisparo, out hitDisparo, distancia))
        {
            GameObject objetoImpactado = hitDisparo.collider.gameObject;
            // El disparo ha impactado con un Collider
            Debug.Log("Disparo impactó en: " + objetoImpactado.name);

             


            // Verificamos si pertenece a un zombie
            if (objetoImpactado.CompareTag("Enemigo"))
            {
                if (objetoImpactado.name == "Bip001")
                {
                  
                    GetComponent<ControladorPlayer>().puntos += 300;
                    objetoImpactado.GetComponentInParent<ControladorZombie>().HacerDaño(50);

                }
                else if (objetoImpactado.name == "Body_01_tanktop")
                {
                   
                    GetComponent<ControladorPlayer>().puntos += 100;
                    objetoImpactado.GetComponentInParent<ControladorZombie>().HacerDaño(25);

                }
            }

            

            // Puedes usar hitDisparo.point y hitDisparo.normal para efectos y decals.
        }
        else
        {
            Debug.Log("El disparo salió del arma pero no golpeó nada.");
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
