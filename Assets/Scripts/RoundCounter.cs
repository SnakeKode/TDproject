using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundCounter : MonoBehaviour
{
    public Text roundsText;

    void OnEnable()
    {
        StartCoroutine(animateText());
    }


    //coroutine gives the ability to stop the execution for some time
    IEnumerator animateText()
    {
        roundsText.text = "0";
        int rounds = 0;

        yield return new WaitForSeconds(0.7f);

        while (rounds < Stats.rounds) 
        {
            rounds++;
            roundsText.text = rounds.ToString();
            
            
            yield return new WaitForSeconds(0.07f);
        }
    }
    
}
