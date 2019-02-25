using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int jumpForce;
    public Transform groundCheck;
    public LayerMask layerGround;
    public float radiusCheck;
    public bool grounded;

    private bool jumping;
    private bool facingRight = true;
    private Rigidbody2D rb2d;
    private Animator anim;

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

        if (Input.GetButtonDown("Jump") && grounded)
        {
            //Comandos do pulo
            jumping = true;
        }

        PlayAnimations();
    }

    private void FixedUpdate()
    {
        float move = 0f;
        move = Input.GetAxis("Horizontal");

        rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);

        if ( (move < 0 && facingRight) || (move > 0 && !facingRight) ) {
            Flip();
        }

        if (jumping)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jumping = false;
        }
    }

    void PlayAnimations()
    {
        if (grounded && rb2d.velocity.x != 0)
        {
            anim.Play("Run");
        }
        else if (grounded && rb2d.velocity.x == 0)
        {
            anim.Play("Idle");
        }
        else if (!grounded)
        {
            anim.Play("Jump");
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
