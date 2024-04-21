using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koster_Collider : MonoBehaviour
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
            var player = collision.gameObject;
            player.GetComponent<Man_Controller>().isCanFollow = false;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var player = collision.gameObject;
            player.GetComponent<Man_Controller>().isCanFollow = false;
        }
    }
}
