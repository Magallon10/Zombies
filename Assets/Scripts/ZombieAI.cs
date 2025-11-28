using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent agente;
    private float speed;
    private Transform objetivoJugador; 
    public float frecuenciaActualizacion = 0.5f; 
    private float ultimoTiempoActualizado; 
    private Animator animator;
    private bool pausado;

    private GameObject jugador;

    void Start()
    {
        agente = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        
        // 1. Encontrar al Jugador
        if (objetivoJugador == null)
        {
            jugador = GameObject.Find("Player");
            
            if (jugador != null)
            {
                objetivoJugador = jugador.transform;
            }
            
        }
        
        // Configuración inicial para forzar la primera actualización
        if (objetivoJugador != null)
        {
            ultimoTiempoActualizado = -frecuenciaActualizacion; 
        }
    }

    void Update()
    {
        pausado = jugador.GetComponent<ControladorPlayer>().pausado;
        if (!pausado)
        {
            speed = agente.velocity.magnitude;

            if (objetivoJugador != null && !gameObject.GetComponent<ControladorZombie>().muerto)
            {
                // Solo actualiza la ruta cada 0.5 segundos para optimizar el rendimiento.
                if (Time.time >= ultimoTiempoActualizado + frecuenciaActualizacion)
                {
                    agente.SetDestination(objetivoJugador.position);
                    ultimoTiempoActualizado = Time.time;
                }
            }
            animator.SetFloat("Speed",speed);
        }
        

    }
}
