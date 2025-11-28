using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ControladorZombie : MonoBehaviour
{

    public int vida;
    private GameObject jugador;
    private Transform jugadorTransform;
    public float rangoAtaque = 1.2f;
    float tiempoEntreAtaques = 2f;
    private float tiempoSiguienteAtaque;
    public bool muerto;
    private Animator animator;
    private bool pausado;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jugador = GameObject.Find("Player");
        float vidaBase = 100f;     
        float factorCrecimiento = 0.07f;
        muerto = false;
        float vida = vidaBase * Mathf.Pow(1f + factorCrecimiento, jugador.GetComponent<CrearZombies>().ronda - 1);
        tiempoSiguienteAtaque = Time.time;
        jugadorTransform = jugador.transform;
        animator = gameObject.GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
         pausado = jugador.GetComponent<ControladorPlayer>().pausado;
        if (!pausado)
        {
            float distancia = Vector3.Distance(transform.position, jugadorTransform.position);

            if (distancia <= rangoAtaque && Time.time >= tiempoSiguienteAtaque && !muerto)
            {
                AtacarJugador();
            }
        }

        
    }

    void AtacarJugador()
    {
        animator.SetTrigger("Attack");
        jugador.GetComponent<ControladorPlayer>().HerirPlayer();

        tiempoSiguienteAtaque = Time.time + tiempoEntreAtaques;
    }
    public void HacerDaño(int daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Morir();
        }
    }
    
    public void Morir()
    {
        
        animator.SetBool("Muerto", true);
        muerto = true;
        jugador.GetComponent<CrearZombies>().zombiesActuales--;
        jugador.GetComponent<CrearZombies>().zombiesRonda--;
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
        GetComponentInParent<NavMeshAgent>().enabled = false;
        StartCoroutine(MorirDespues());
    }

private IEnumerator MorirDespues()
{
    yield return new WaitForSeconds(4f);

    jugador.GetComponent<ControladorPlayer>().ZombieMuerto();

    Destroy(gameObject);
}
}
