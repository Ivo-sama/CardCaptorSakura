using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kero : MonoBehaviour
{
    // Start is called before the first frame update
    private bool face;
    private bool stalk;
    private GameObject sakura;
    private Rigidbody2D sakuraRig;
    private Rigidbody2D keroRig;
    private float keroSpeed = 0.4f;
    private float jumpPower = 1.0f;

    void Start()
    {
        sakura = GameObject.Find("Sakura");
        sakuraRig = sakura.GetComponent<Rigidbody2D>();
        keroRig = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( (sakura.transform.position.x < this.transform.position.x) && !face)
        {
            flip();
        } else if ( (sakura.transform.position.x > this.transform.position.x) && face)
        {
            flip();
        }

        if (stalk)
        {
            if (face)
            {
                transform.Translate(Vector2.left * keroSpeed * Time.deltaTime);
            } else if (!face)
            {
                transform.Translate(Vector2.right * keroSpeed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sakura"))
        {
            stalk = true;
            Debug.Log("Deve ser culpa de uma carta Clow...");
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Sakura"))
        {
            sakuraRig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            gameObject.GetComponent<Animator>().SetBool("Kero_Dead", true);
            Destroy(gameObject, 0.85f);
        }
    }

    void flip()
    {
        face = !face;
        Vector3 scale = this.transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
