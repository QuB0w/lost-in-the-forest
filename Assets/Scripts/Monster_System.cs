using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster_System : MonoBehaviour
{
    public int ID = 0;

    private NavMeshAgent agent;
    private Transform player;

    public float stopDest;
    public float retriDest;

    public bool isDead = false;

    public bool isFollow = false;

    public GameObject point;
    private GameObject point2;

    private bool isCanAttack = true;
    private bool isCanAnim = true;

    [Header("Sprites")]
    public SpriteRenderer[] sprites;

    public int HP = 100;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Man").transform;
        player.GetComponent<Man_Controller>().ID += 1;
        ID = player.GetComponent<Man_Controller>().ID;
        point2 = Instantiate(point, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), new Quaternion(0,0,0,0));
        point2.name = ID.ToString();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            isCanAttack = false;
            agent.speed = 0;
            if (isDead == false)
            {
                player.GetComponent<Man_Controller>().Coin_Count += 1;
            }
            isDead = true;
            gameObject.GetComponent<Animator>().Play("Skeleton_Dead");
            Destroy(gameObject, 1f);
        }

        if (gameObject.transform.position.x < player.position.x)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].flipX = false;
            }
        }
        else
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].flipX = true;
            }
        }

        if (isDead == false && player.GetComponent<Man_Controller>().isCanFollow == true)
        {
            agent.SetDestination(player.position);
            if(isCanAnim == true)
            {
                gameObject.GetComponent<Animator>().Play("Skeleton_Run");
            }
        }
        else if(player.GetComponent<Man_Controller>().isCanFollow == false)
        {
            agent.SetDestination(point2.transform.position);
            if (gameObject.transform.position.x < point2.transform.position.x)
            {
                for (int i = 0; i < sprites.Length; i++)
                {
                    sprites[i].flipX = false;
                }
            }
            else
            {
                for (int i = 0; i < sprites.Length; i++)
                {
                    sprites[i].flipX = true;
                }
            }
        }

        //if (Vector2.Distance(transform.position, player.position) > stopDest)
        //{
        //    agent.SetDestination(player.position);
        //    gameObject.GetComponent<Animator>().Play("Skeleton_Run");
        //}
        //else if(Vector2.Distance(transform.position, player.position) < stopDest && Vector2.Distance(transform.position, player.position) > retriDest)
        //{
        //    transform.position = this.transform.position;
        //}
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if (isCanAttack == true)
            {
                isCanAnim = false;
                gameObject.GetComponent<Animator>().Play("Skeleton_Attack");
                isCanAttack = false;
                StartCoroutine(waitForAnim());
                StartCoroutine(waitForAttack());
            }
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if(isCanAttack == true)
            {
                isCanAnim = false;
                gameObject.GetComponent<Animator>().Play("Skeleton_Attack");
                isCanAttack = false;
                StartCoroutine(waitForAnim());
                StartCoroutine(waitForAttack());
            }
        }
    }

    IEnumerator waitForAttack()
    {
        player.GetComponent<Man_Controller>().HP -= 25;
        yield return new WaitForSeconds(1f);
        isCanAttack = true;
    }

    IEnumerator waitForAnim()
    {
        yield return new WaitForSeconds(0.667f);
        isCanAnim = true;
    }
}
