using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ControladorPlayer : MonoBehaviour
{
    [Header("Referencias")]
    public Camera camaraFPS;              // La cámara principal del jugador
    public Transform puntoDeDisparo;      // Objeto vacío en la boca del cañón

    [Header("Parámetros")]
    public float rangoMaximo = 100f;     // Rango máximo para la puntería
    public float rangoDeDisparo = 150f;  // Rango máximo para el proyectil

    private ControladorZombie controladorZombie;



    private CharacterController controller;
    public float jumpSpeed = 8.0F;
    public float speed = 6.0f;
    public float gravity = 20.0F;
    private float vidaMax = 100f;
    private float vida = 100f;
    public int puntos = 0;
    private Coroutine regenerando;
    public TMP_Text textoVida;
    public TMP_Text textoPuntos;
    public TMP_Text textoGameOver;
    public Canvas hud;
    public Canvas gameOver;
    private Vector3 moveDirection = Vector3.zero;
    public ParticleSystem flashDisparo;
    private int contadorBajas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gameOver.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Visualizar vida y puntos
        textoVida.text = "Vida: " + Mathf.RoundToInt(vida);
        textoPuntos.text = "Puntos: " + puntos;


        //Disparar si hace click
        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<Disparar>().Disparo();
        }

        //Esprintar
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 10f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 6f;
        }

        // Movimiento horizontal siempre
        Vector3 horizontalMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        horizontalMove = transform.TransformDirection(horizontalMove);
        horizontalMove *= speed;


        // Solo salto si está en el suelo
        if (controller.isGrounded)
        {
            moveDirection.y = 0; // Reiniciar velocidad vertical cuando está en el suelo

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Aplicar movimiento horizontal al moveDirection manteniendo el valor vertical actual
        moveDirection.x = horizontalMove.x;
        moveDirection.z = horizontalMove.z;

        // Aplicar gravedad
        moveDirection.y -= gravity * Time.deltaTime;

        // Mover el personaje
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void HerirPlayer()
    {
        //Quitar vida
        vida -= 40;

        //Si se está regenerando vida, parar el proceso y volver a empezar
        if (regenerando != null)
        StopCoroutine(regenerando);

        regenerando = StartCoroutine(RegenerarVida());

        //Si la vida está a 0 se acaba el juego
        if (vida < 1)
            GameOver();
    }

     private IEnumerator RegenerarVida()
    {
        //Espera 3 segundos para empezar a regenerar
        yield return new WaitForSeconds(3f);

        //Va regenerando la vida hasta que llega al máximo
        while (vida < vidaMax)
        {
            vida += 15f * Time.deltaTime;
            vida = Mathf.Clamp(vida, 0, vidaMax);
            yield return null;
        }

        regenerando = null;
    }
            
    public void ZombieMuerto()
    {
        contadorBajas++;
    }
    void GameOver()
    {
        textoGameOver.text = "Zombies matados: " + contadorBajas;
        hud.enabled = false;
        gameOver.enabled = true;
        
    }
   
}
