using UnityEngine;
using UnityEngine.UI;


public class LevelSelector : MonoBehaviour
{
    public sceneFader sceneFader;
    public Button[] lvlButtons;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i=levelReached ; i < lvlButtons.Length ; i++)
        {
            lvlButtons[i].interactable = false;
        }
    }


    public void Select(string lvl)
    {
        sceneFader.fadeTo(lvl);
    }

}
