using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura : MonoBehaviour
{
    private GameObject sakura;
    private float speed = 5f;
    private Rigidbody2D sakuraRig;
    private Animator anim;
    private SpriteRenderer flipSprite;
    public Transform shotPoint;
    public GameObject bala;
    private string shotdirection;
    private float jumpPower = 18f;
    private bool pesNoChao;
    private Transform shotPointFlip;

    private Transform backShotPoint;

    //VARIABLES ABOUT ATTACK
    private float timeAttack;
    private float startTimeAttack = 0.3f;
    private bool isAttack;

    //VARIÁVEIS CONTROLADORAS DE EVENTOS DE INIMIGO
    private Animator enemySprite;


    // Start is called before the first frame update
    void Start()
    {
        sakura = GameObject.Find("Sakura");
        sakuraRig = sakura.GetComponent<Rigidbody2D>();
        anim = sakura.GetComponent<Animator>();
        flipSprite = GetComponent<SpriteRenderer>();
        shotPoint = GameObject.Find("ShotPoint").GetComponent<Transform>();
        backShotPoint = GameObject.Find("backShotPoint").GetComponent<Transform>();
    }

    //Capture Keyboard/Mouse events
    void Update()
    {
        //Moves the player to right or left
        if(Input.GetKey(KeyCode.RightArrow) && !isAttack)
        {
            sakuraRig.velocity = new Vector2(speed, sakuraRig.velocity.y);
            flipSprite.flipX = false;
            anim.SetBool("isWalking", true);
        }
        else if(Input.GetKey(KeyCode.LeftArrow) && !isAttack)
        {
            sakuraRig.velocity = new Vector2(-speed, sakuraRig.velocity.y);
            flipSprite.flipX = true;
            anim.SetBool("isWalking", true);
        }
        else
        {
            sakuraRig.velocity = new Vector2(0, sakuraRig.velocity.y);
            anim.SetBool("isWalking", false);
        }

        if (timeAttack <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetBool("isAttack", true);
                timeAttack = startTimeAttack;
                isAttack = true;
                GameObject bullet = Instantiate(bala);
                if (!gameObject.GetComponent<SpriteRenderer>().flipX)
                {
                    bullet.transform.position = shotPoint.position;
                } else
                {
                    bullet.transform.position = backShotPoint.position;
                }
            }
        }
        else
        {
            isAttack = false;
            timeAttack = timeAttack - Time.deltaTime;
            anim.SetBool("isAttack", false);
        }

        if (Input.GetMouseButtonDown(1) && pesNoChao)
        {
            sakuraRig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            sakuraRig.gravityScale = 4;
            pesNoChao = !pesNoChao;
            anim.SetBool("isJumping", true);
            if(isAttack)
            {
                anim.SetTrigger("isAttack");
            }
        }
    }

     void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.layer == 3)
        {
            pesNoChao = true;
            anim.SetBool("isJumping", false);
        }
    }
}
