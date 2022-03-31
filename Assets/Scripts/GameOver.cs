using UnityEngine;
using UnityEngine.SceneManagement;



public class GameOver : MonoBehaviour
{
    
    
    
    public string menuScene = "MainMenu";
    public sceneFader sceneFader;

    

    public void Retry()
    {
        sceneFader.fadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.fadeTo(menuScene);
    }

}
