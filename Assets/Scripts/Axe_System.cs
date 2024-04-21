using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_System : MonoBehaviour
{
    public Man_Controller man_script;
    public int TreeDamage = 25;
    public int MonsterDamage = 30;

    private GameObject monster;
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
        if(collision.tag == "Tree")
        {
            if(man_script.isAttack == true)
            {
                collision.GetComponent<Tree_System>().HP -= TreeDamage;
                collision.GetComponent<Tree_System>().EffectCollision();
                collision.GetComponentInParent<Animator>().Play("Tree_Hit");
                GameObject.Find("Main Camera").GetComponent<Animator>().Play("Camera_Shake");
            }
        }

        if(collision.tag == "Enemy")
        {
            if (man_script.isAttack == true)
            {
                monster = collision.gameObject;
                collision.GetComponent<Monster_System>().HP -= MonsterDamage;
                StartCoroutine(wait());
                GameObject.Find("Main Camera").GetComponent<Animator>().Play("Camera_Shake");
            }
        }
    }

    IEnumerator wait()
    {
        for(int i = 0; i < monster.GetComponent<Monster_System>().sprites.Length; i++)
        {
            monster.GetComponent<Monster_System>().sprites[i].color = new Color32(255, 182, 174, 255);
        }
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < monster.GetComponent<Monster_System>().sprites.Length; i++)
        {
            monster.GetComponent<Monster_System>().sprites[i].color = new Color32(255, 255, 255, 255);
        }
    }
}
