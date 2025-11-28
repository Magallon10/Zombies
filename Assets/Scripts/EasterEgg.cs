using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    public int antorchasEncendidas;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(antorchasEncendidas == 5)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<SphereCollider>().enabled = true;
        }
        
    }

    void OnTriggerEnter(Collider other) {

        if (other.name.Equals("Player"))
        {
            
        }
    }
}
