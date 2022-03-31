using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public GameObject ui;
    public string menuScene = "MainMenu";
    public sceneFader sceneFader;

    public void Resume()
    {
        Time.timeScale = 1f;
        ui.SetActive(false);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        sceneFader.fadeTo(SceneManager.GetActiveScene().name);
    }




    public void Menu()
    {
        Time.timeScale = 1f;
        sceneFader.fadeTo(menuScene);
    }

}
