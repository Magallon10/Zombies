using TMPro;
using UnityEngine;

public class Interactuar: MonoBehaviour
{

    
    public float distanciaInteraccion = 3f; 
    public Camera camaraFPS;              
    public TMP_Text textoMensaje;           
    private GameObject puertaActual = null;
    private GameObject pocionVidaActual = null;
    private GameObject pocionDañoActual= null;
    private GameObject antorchaActual = null;
    private GameObject portal;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textoMensaje.enabled = false;
        portal = GameObject.Find("Portal");
    }

    // Update is called once per frame
    void Update()
    {
        DetectarObjetoInteractivo();

        
        if (puertaActual != null && Input.GetKeyDown(KeyCode.E))
        {
            if(GetComponent<ControladorPlayer>().puntos >= puertaActual.GetComponent<AbrirPuerta>().puntosNecesarios)
            {
                puertaActual.GetComponent<AbrirPuerta>().Abrir();
                Comprar(puertaActual.GetComponent<AbrirPuerta>().puntosNecesarios);
            }
            
        }
         if (pocionVidaActual != null && Input.GetKeyDown(KeyCode.E))
        {
            if(GetComponent<ControladorPlayer>().puntos >= 500)
            {
                GetComponent<ControladorPlayer>().vida = 200;
                GetComponent<ControladorPlayer>().vidaMax = 200;
                Comprar(500);
                Destroy(pocionVidaActual);
            }
            
        }
        if (pocionDañoActual != null && Input.GetKeyDown(KeyCode.E))
        {
            if(GetComponent<ControladorPlayer>().puntos >= 300)
            {
                GetComponent<Disparar>().daño = 50;
                Comprar(300);
                Destroy(pocionDañoActual);
            }
            
        }
         if (antorchaActual != null && Input.GetKeyDown(KeyCode.E))
        {
            Color colorNuevo = new Color32(0, 93, 251, 255);
            if(antorchaActual.GetComponentInChildren<Light>().color != colorNuevo)
            {
                antorchaActual.GetComponentInChildren<Light>().color = colorNuevo;
                var main = antorchaActual.GetComponentInChildren<ParticleSystem>().main;
                main.startColor = colorNuevo;
                portal.GetComponent<EasterEgg>().antorchasEncendidas++;
            }
            
            
        }
    

    }

    void DetectarObjetoInteractivo()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(camaraFPS.transform.position, camaraFPS.transform.forward, out hit, distanciaInteraccion))
        {
            if (hit.transform.CompareTag("Puerta"))
            {
                GameObject puerta = hit.collider.gameObject;
                if (puerta != null && !puerta.GetComponent<AbrirPuerta>().abierta)
                {
                    puertaActual = puerta;
                    
                    
                    textoMensaje.text = "E para abrir por " + puerta.GetComponent<AbrirPuerta>().puntosNecesarios + " puntos";
                    textoMensaje.enabled = true;
                    return; 
                }
            }
            if (hit.transform.CompareTag("PocionVida"))
            {
                GameObject pocion = hit.transform.parent.gameObject.transform.parent.gameObject;
                if (pocion != null)
                {
                    pocionVidaActual = pocion;
                    textoMensaje.text = "E para comprar pocion de Vida por 500 puntos";
                    textoMensaje.enabled = true;
                    return; 
                }
            }
            if (hit.transform.CompareTag("PocionDaño"))
            {
                GameObject pocion = hit.transform.parent.gameObject.transform.parent.gameObject;
                if (pocion != null)
                {
                    pocionDañoActual = pocion;
                    textoMensaje.text = "E para comprar pocion de Daño por 300 puntos";
                    textoMensaje.enabled = true;
                    return; 
                }
            }
            if (hit.transform.CompareTag("Antorcha"))
            {
                GameObject antorcha = hit.collider.gameObject;
                if (antorcha != null)
                {
                    antorchaActual = antorcha;
                    
                    return; 
                }
            }
        }

        if (puertaActual != null)
        {
            puertaActual = null;
            textoMensaje.enabled = false;
        }
         if (pocionDañoActual != null)
        {
            pocionDañoActual = null;
            textoMensaje.enabled = false;
        }
         if (pocionVidaActual != null)
        {
            pocionVidaActual = null;
            textoMensaje.enabled = false;
        }

        textoMensaje.enabled = false;
        
    }

    public void Comprar(int precio)
    {
        gameObject.GetComponent<ControladorPlayer>().puntos -= precio;
    }
    
}
