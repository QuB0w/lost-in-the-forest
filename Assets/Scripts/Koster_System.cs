using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Koster_System : MonoBehaviour
{
    public float time = 120;
    public float needtime = 0;
    public int brevnaInKoster = 0;

    private bool isActive = true;

    public ParticleSystem part;
    public GameObject light;
    public CircleCollider2D trigger;

    public Text kosterText;
    public GameObject buttonToSleep;
    // Start is called before the first frame update
    void Start()
    {
        time = 120;
        StartCoroutine(fuelIsGone());
    }

    // Update is called once per frame
    void Update()
    {
        kosterText.text = time.ToString();

        if (isActive == true)
        {
            while ((time % 10 == 0 && time < 120) && brevnaInKoster > 0)
            {
                brevnaInKoster -= 1;
                time += 10;
            }
        }
        
        if(time <= 0)
        {
            trigger.enabled = false;
            GameObject.Find("Man").GetComponent<Man_Controller>().isCanFollow = true;
            part.gameObject.SetActive(false);
            light.SetActive(false);
        }
        else if(time > 0)
        {
            trigger.enabled = true;
            GameObject.Find("Man").GetComponent<Man_Controller>().isCanFollow = false;
            part.gameObject.SetActive(true);
            light.SetActive(true);
        }

        if (time > 120)
        {
            time = 120;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(GameObject.Find("Directional Light").GetComponent<DayNightCycle>().timeDay >= 180)
            {
                buttonToSleep.SetActive(true);
            }
        }

        if(collision.tag == "Player")
        {
            var player = collision.gameObject;
            if (player.GetComponent<Man_Controller>().Brevno_Count > 0)
            {
                brevnaInKoster += player.GetComponent<Man_Controller>().Brevno_Count;
                //needtime = 120 - time;
                //if (needtime < 10)
                //{
                //    brevnaInKoster -= 1;
                //    time += 10;
                //}
                //else if (needtime > 10)
                //{
                //    isActive = false;
                //    if (brevnaInKoster >= Convert.ToInt32(Math.Ceiling(needtime / 10)))
                //    {
                //        brevnaInKoster -= Convert.ToInt32(Math.Ceiling(needtime / 10));
                //        time += Convert.ToInt32(Math.Ceiling(needtime));
                //        Debug.Log("End");
                //    }
                //    isActive = true;
                //}
                while (time < 120)
                {
                    time += 10;
                    brevnaInKoster -= 1;
                }

                player.GetComponent<Man_Controller>().Brevno_Count = 0;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            buttonToSleep.SetActive(false);
        }
    }

    IEnumerator fuelIsGone()
    {
        yield return new WaitForSeconds(1f);
        if(time > 0)
        {
            time -= 1;
        }
        StartCoroutine(fuelIsGone());
    }
}
