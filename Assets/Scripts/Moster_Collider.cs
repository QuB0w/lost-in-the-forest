using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moster_Collider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject.Find("Man").GetComponent<Man_Controller>().isCanFollow = true;
        }
    }
}
