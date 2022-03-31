using UnityEngine;


public class LevelWon : MonoBehaviour
{
    public string levelsScene = "LevelSelect";
    public string nextLevel = "Level02";
    public sceneFader sceneFader;



    public void Continue()
    {
        sceneFader.fadeTo(nextLevel);
    }

    public void levelSelect()
    {
        sceneFader.fadeTo(levelsScene);
    }
}
