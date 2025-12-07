using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrearZombies : MonoBehaviour
{

    public GameObject prefabZombie;
    public List<Transform> SpawnsDisponibles;

    private int limiteZombies = 15;
    private int zombiesBase = 10;
    public int zombiesRonda = 0;
    public int zombiesActuales;
    public int ronda = 0;
    public bool spawnEnProgreso = false;

    private float tiempoRespawn;
    private float tiempoRespawnBase = 4f;
    public TMP_Text textoRonda;

    private bool pausado;

    

    void Awake()
    {
            SceneManager.sceneLoaded += OnSceneLoaded;
       
    }

   public void InicializarZombiesEnEscena()
{
    Debug.Log("HOla");
    SpawnsDisponibles.Clear();
    ronda = 0;
    zombiesActuales = 0;
    zombiesRonda = 0; 
    

    GameObject spawn4 = GameObject.Find("Spawn4");
    GameObject spawn5 = GameObject.Find("Spawn5");

    if (spawn4 != null) SpawnsDisponibles.Add(spawn4.transform);
    if (spawn5 != null) SpawnsDisponibles.Add(spawn5.transform);
    
    
    // Opcional: Reasignar el prefabZombie (Si el prefab en sí se pierde o cambia)
    // Asegúrate de que prefabZombie es una referencia arrastrada en el Inspector.

 
    if (!spawnEnProgreso)
    {

        StopAllCoroutines(); 
        StartCoroutine(SpawnearContinuamente());
    }
    Debug.Log("Ha sudado");
}

    // private void InicializarLogicaZombies()
    // {
    //     SpawnsDisponibles.Clear(); 
        
    //     SpawnsDisponibles.Add(GameObject.Find("Spawn4").transform);
    //     SpawnsDisponibles.Add(GameObject.Find("Spawn5").transform);
        

    //     ronda = 0;
    //     zombiesActuales = 0;
    //     zombiesRonda = 0; 
        
    //     if (!spawnEnProgreso)
    //     {
    //         StartCoroutine(SpawnearContinuamente());
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        pausado = GetComponent<ControladorPlayer>().pausado;
        textoRonda.text = "Ronda: "+ronda;
    }

   private IEnumerator SpawnearContinuamente()
    {
        Debug.Log("hola");
        spawnEnProgreso = true;
        
        while (true) 
        {
            string nombreEscenaActual = SceneManager.GetActiveScene().name;

            if (nombreEscenaActual == "EasterEgg")
            {
                spawnEnProgreso = false; 
                yield break; 
            }
            if(zombiesRonda == 0)
            {
                ronda++;
                zombiesRonda = zombiesBase + 2 * (ronda -1);
                tiempoRespawn = tiempoRespawnBase- tiempoRespawnBase * (ronda / 50);
                yield return new WaitForSeconds(6f);
            }
            if (zombiesActuales < limiteZombies)
            {
                Transform puntoSpawnAleatorio = SpawnsDisponibles[Random.Range(0, SpawnsDisponibles.Count)];
                
                Instantiate(prefabZombie, puntoSpawnAleatorio.position, puntoSpawnAleatorio.rotation);
                zombiesActuales++;
                
            }
            
            yield return new WaitForSeconds(tiempoRespawn);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Mapa1" || scene.name == "EasterEgg")
        {
    
            GameObject textoObj = GameObject.Find("TextoRonda"); 
            if (textoObj != null)
            {
                textoRonda = textoObj.GetComponent<TMP_Text>();
            }

        }
        // else if (scene.name == "MenuInicial") // O cualquier escena donde no debe estar activo
        // {
        //      StopAllCoroutines();
        //      spawnEnProgreso = false;
        // }
    }
    public void DesbloquearSpawn(string nombreSpawn)
    {
        if (!SpawnsDisponibles.Contains(GameObject.Find(nombreSpawn).transform))
        {
             SpawnsDisponibles.Add(GameObject.Find(nombreSpawn).transform);
        }
       
    }
}
