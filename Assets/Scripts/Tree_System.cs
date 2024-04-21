using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tree_System : MonoBehaviour
{
    public int HP = 100;

    public GameObject pivot;

    public GameObject brevno;

    public bool isDead = false;

    public bool isCan = false;

    public Vector3 tree_pos;

    [Header("Particles")]
    public ParticleSystem hit;
    // Start is called before the first frame update
    void Start()
    {
        isCan = true;
        tree_pos = gameObject.transform.localPosition;
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            gameObject.GetComponentInParent<Animator>().Play("Tree_Dead");
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            if(isDead == false)
            {
                if (isCan == true)
                {
                    StartCoroutine(wait());
                    isDead = true;
                }
            }
            //Destroy(pivot.gameObject, 1f);
            HP = 100;
        }
    }

    //public void OnDestroy()
    //{
    //    GameObject.Find("Man").GetComponent<Man_Controller>().GenerateNav();
    //}

    public void EffectCollision()
    {
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.9f);
        GameObject.Find("Man").GetComponent<Man_Controller>().Brevno_Count += 1;
        isCan = false;
        gameObject.GetComponentInParent<TreeParent_Controller>().StartCoroutine("clock");
        gameObject.SetActive(false);
        GameObject.Find("Man").GetComponent<Man_Controller>().GenerateNav();
    }
}
