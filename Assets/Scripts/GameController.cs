using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
     public void IniciarOReinciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Mapa1");
        GameObject player = GameObject.Find("Player");
        Destroy(player);
    }
    public void SalirDelJuego()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
      public void SalirAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicial");
        Destroy(GameObject.Find("Player"));
       
    }
}
