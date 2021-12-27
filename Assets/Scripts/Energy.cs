using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    private GameObject sakura;
    private GameObject kero;
    private float bulletSpeed = 2.0f;
    // Start is called before the first frame update
    private Transform bulletFlipX;
    private bool bulletFace;
    void Start()
    {
        sakura = GameObject.Find("Sakura");
        kero = GameObject.Find("kero");
    }

    // Update is called once per frame
    void Update()
    {
        if (sakura.GetComponent<SpriteRenderer>().flipX && !bulletFace)
        {
            flip();
        } else if ( (!sakura.GetComponent<SpriteRenderer>().flipX) && bulletFace)
        {
            flip();
        }

        if (bulletFace)
        {
            gameObject.transform.Translate(new Vector2(bulletSpeed * Time.deltaTime, 0));
        } else if (!bulletFace)
        {
            gameObject.transform.Translate(new Vector2(-bulletSpeed * Time.deltaTime, 0));
        }
    }

    void flip()
    {
        bulletFace = !bulletFace;
        Vector3 scale = this.transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
