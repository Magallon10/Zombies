using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrearZombies : MonoBehaviour
{

    public GameObject prefabZombie;
    public List<Transform> SpawnsDisponibles;

    private int limiteZombies = 15;
    private int zombiesBase = 10;
    public int zombiesRonda = 0;
    public int zombiesActuales;
    public int ronda = 0;
    private bool spawnEnProgreso = false;

    private float tiempoRespawn;
    private float tiempoRespawnBase = 4f;
    public TMP_Text textoRonda;

    

    void Start()
    {
        SpawnsDisponibles.Add(GameObject.Find("Spawn4").transform);
        SpawnsDisponibles.Add(GameObject.Find("Spawn5").transform);
        ronda = 0;
        zombiesActuales = 0;
        if (!spawnEnProgreso)
        {
            StartCoroutine(SpawnearContinuamente());
        }
    }

    // Update is called once per frame
    void Update()
    {
        textoRonda.text = "Ronda: "+ronda;
    }

   private IEnumerator SpawnearContinuamente()
    {
        spawnEnProgreso = true;
        
        while (true) 
        {
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

    public void DesbloquearSpawn(string nombreSpawn)
    {
        if (!SpawnsDisponibles.Contains(GameObject.Find(nombreSpawn).transform))
        {
             SpawnsDisponibles.Add(GameObject.Find(nombreSpawn).transform);
        }
       
    }
}
