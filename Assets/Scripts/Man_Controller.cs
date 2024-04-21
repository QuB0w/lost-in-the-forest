using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Man_Controller : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform tr;
    [SerializeField] private Animator anim;
    [SerializeField] public float Speed;
    [SerializeField] public Transform Axe;

    [SerializeField] public int ID = 0;

    [SerializeField] public bool isAttack = false;
    [SerializeField] public bool isRot = false;

    [SerializeField] public bool isCanFollow = true;

    private NavMeshSurface2d nav;

    [Header("Sprites")]
    [SerializeField] public SpriteRenderer[] man_sprites;

    [Header("UI")]
    public Text brevnoText;
    public Text coinText;
    public Text hpText;

    [Header("Statistics")]
    [SerializeField] public int HP = 100;
    [SerializeField] public int Brevno_Count = 0;
    [SerializeField] public int Coin_Count = 0;

    void Start()
    {
        nav = GameObject.Find("NavMesh").GetComponent<NavMeshSurface2d>();
        
        rb = gameObject.GetComponent<Rigidbody2D>();
        tr = gameObject.GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
    }

    public void GenerateNav()
    {
        nav.BuildNavMesh();
    }

    private void Update()
    {
        if(HP <= 0)
        {
            Destroy(gameObject);
        }

        hpText.text = HP.ToString();
        brevnoText.text = Brevno_Count.ToString();
        coinText.text = Coin_Count.ToString();

        Vector2 cam = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(cam.x < gameObject.transform.position.x)
        {
            //Axe.localPosition = new Vector3(-1.174f, Axe.localPosition.y, Axe.localPosition.z);
            var obj = GameObject.Find("Axe").GetComponent<Transform>();
            obj.parent = GameObject.Find("Left_Pivot").GetComponent<Transform>().transform;
            if(isRot == false)
            {
                Axe.position = new Vector3(GameObject.Find("Left_Pivot").GetComponent<Transform>().position.x - 0.653f, GameObject.Find("Left_Pivot").GetComponent<Transform>().position.y, 0);
                isRot = true;
            }       
            obj.parent = Axe.transform;
            obj.localPosition = new Vector3(-0.4f, 0.242f, 1f);
            for (int i = 0; i < man_sprites.Length; i++)
            {
                man_sprites[i].flipX = true;
            }
        }

        if (cam.x > gameObject.transform.position.x)
        {
            //Axe.localPosition = new Vector3(0, Axe.localPosition.y, Axe.localPosition.z);
            var obj = GameObject.Find("Axe").GetComponent<Transform>();
            obj.parent = GameObject.Find("Left_Pivot").GetComponent<Transform>().transform;
            if(isRot == true)
            {
                Axe.position = new Vector3(GameObject.Find("Left_Pivot").GetComponent<Transform>().position.x - 0.4f, GameObject.Find("Left_Pivot").GetComponent<Transform>().position.y, 1f);
                isRot = false;
            }
            obj.parent = Axe.transform;
            obj.localPosition = new Vector3(0.644f, 0.242f, -0.8f);
            for (int i = 0; i < man_sprites.Length; i++)
            {
                man_sprites[i].flipX = false;
            }
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && Input.GetMouseButtonDown(0) && isAttack == false)
        {
            if (cam.x < gameObject.transform.position.x)
            {
                GameObject.Find("Axe").GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(waitAxeCollider());
                isAttack = true;
                anim.SetTrigger("Run_Attack_2");
                StopCoroutine(waitForAttack());
                StartCoroutine(waitForAttack());
            }
        }

        if (Input.GetMouseButtonDown(0) && isAttack == false)
        {
            if(cam.x < gameObject.transform.position.x)
            {
                GameObject.Find("Axe").GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(waitAxeCollider());
                isAttack = true;
                anim.SetTrigger("Idle_Attack_2");
                StopCoroutine(waitForAttack());
                StartCoroutine(waitForAttack());
            }
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && Input.GetMouseButtonDown(0) && isAttack == false)
        {
            if (cam.x > gameObject.transform.position.x)
            {
                GameObject.Find("Axe").GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(waitAxeCollider());
                isAttack = true;
                anim.SetTrigger("Run_Attack");
                StopCoroutine(waitForAttack());
                StartCoroutine(waitForAttack());
            }
        }

        if (Input.GetMouseButtonDown(0) && isAttack == false)
        {
            if(cam.x > gameObject.transform.position.x)
            {
                GameObject.Find("Axe").GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(waitAxeCollider());
                isAttack = true;
                anim.SetTrigger("Idle_Attack");
                StopCoroutine(waitForAttack());
                StartCoroutine(waitForAttack());
            }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (isAttack == false)
            {
                anim.Play("Man_Run");
            }
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            if (isAttack == false)
            {
                anim.Play("Man_Idle");
            }
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);

        rb.AddForce(movement * Speed);
    }

    IEnumerator waitForAttack()
    {
        yield return new WaitForSeconds(0.667f);
        isAttack = false;
    }

    IEnumerator waitAxeCollider()
    {
        yield return new WaitForSeconds(0.6f);
        GameObject.Find("Axe").GetComponent<BoxCollider2D>().enabled = true;
    }
}
