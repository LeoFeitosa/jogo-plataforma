using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Transform groundCheck;
    public LayerMask layerGround;
    public float radiusCheck;
    public bool grounded;

    private bool facingRight = true;
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool isVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, layerGround);

        if (!grounded)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (isVisible)
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        speed *= -1;
    }

    private void OnBecameVisible()
    {
        Invoke("MoveEnemy", 3f);
    }

    private void OnBecameInvisible()
    {
        Invoke("StopEnemy", 3f);
    }

    void MoveEnemy()
    {
        isVisible = true;
        anim.Play("run");
    }

    void StopEnemy()
    {
        isVisible = false;
        anim.Play("idle");
    }

}
