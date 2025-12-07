using UnityEngine;
using UnityEngine.SceneManagement;

public class EasterEgg : MonoBehaviour
{
    public int antorchasEncendidas;
    private AudioSource audio;
    private bool portalActivo = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 1. Comprobar la condición de activación
        if (antorchasEncendidas == 5)
        {
            // 2. Comprobar si ya está activo
            if (!portalActivo)
            {
               
                GetComponent<MeshRenderer>().enabled = true;
                GetComponent<SphereCollider>().enabled = true;

                if (audio != null)
                {
                    audio.Play();
                }
                
              
                portalActivo = true;
            }
        }
    }

    void OnTriggerEnter(Collider other) {

        if (other.name.Equals("Player"))
        {
            SceneManager.LoadScene("EasterEgg");
        }
    }
}
