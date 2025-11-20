using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent agente;
    private float speed;
    public Transform objetivoJugador; 
    public float frecuenciaActualizacion = 0.5f; 
    private float ultimoTiempoActualizado; 
    private Animator animator;

    void Start()
    {
        agente = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        // 1. Encontrar al Jugador
        if (objetivoJugador == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                objetivoJugador = player.transform;
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
        speed = agente.velocity.magnitude;

        // 2. Lógica de Actualización de Ruta
        if (objetivoJugador != null)
        {
            // Solo actualiza la ruta cada 0.5 segundos para optimizar el rendimiento.
            if (Time.time >= ultimoTiempoActualizado + frecuenciaActualizacion)
            {
                // La función clave: le dice al NavMeshAgent que calcule la ruta y se mueva.
                agente.SetDestination(objetivoJugador.position);
                ultimoTiempoActualizado = Time.time;
            }
        }
        animator.SetFloat("Speed",speed);

    }
}
