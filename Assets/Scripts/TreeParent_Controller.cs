using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeParent_Controller : MonoBehaviour
{
    public GameObject tree;

    public GameObject Clock;
    public GameObject tree_fantom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator clock()
    {
        Debug.Log("Started");
        Clock.SetActive(true);
        tree_fantom.SetActive(true);
        yield return new WaitForSeconds(30f);
        tree.SetActive(true);
        tree.GetComponent<PolygonCollider2D>().enabled = true;
        Clock.SetActive(false);
        tree_fantom.SetActive(false);
        tree.GetComponent<Tree_System>().isCan = true;
        tree.GetComponent<Tree_System>().isDead = false;
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        GameObject.Find("Man").GetComponent<Man_Controller>().GenerateNav();
    }
}
