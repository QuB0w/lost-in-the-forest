using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    public GameObject light;
    public string dayCycle = "Утро";
    public int timeDay = 0;

    public Text dayText;
    public GameObject BG;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
    }

    // Update is called once per frame
    void Update()
    {
        if(timeDay < 60)
        {
            dayCycle = "Утро";
            dayText.text = "Утро";
        }
        else if(timeDay >= 60 && timeDay < 120)
        {
            dayCycle = "День";
            dayText.text = "День";
        }
        else if (timeDay >= 120 && timeDay < 180)
        {
            dayCycle = "Вечер";
            dayText.text = "Вечер";
        }
        else if (timeDay >= 180)
        {
            dayCycle = "Ночь";
            dayText.text = "Ночь";
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        if(timeDay < 240)
        {
            timeDay += 1;
            StartCoroutine(wait());
        }
    }

    public void Sleep()
    {
        StartCoroutine(sleepWait());
    }

    IEnumerator sleepWait()
    {
        BG.SetActive(true);
        BG.GetComponent<Animator>().Play("FadeBG");
        yield return new WaitForSeconds(2f);
        timeDay = 0;
        BG.GetComponent<Animator>().Play("FadeOutBG");
        yield return new WaitForSeconds(2f);
        BG.SetActive(false);
    }
}
