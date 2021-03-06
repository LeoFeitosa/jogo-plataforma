﻿using System.Collections;
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
    private bool isAlive = true;
    private bool levelComplete = false;
    private bool timeIsOver = false;

    private Rigidbody2D rb2d;
    private Animator anim;

    public AudioClip fxWin;
    public AudioClip fxDie;
    public AudioClip fxJump;

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
            if(isAlive && !levelComplete)
                SoundManager.instance.PlayFxPlayer(fxJump);
        }

        if (((int)GameManager.instance.time <= 0) && !timeIsOver)
        {
            timeIsOver = true;
            PlayerDie();
        }

        PlayAnimations();
    }

    private void FixedUpdate()
    {
        if (isAlive && !levelComplete)
        {
            float move = Input.GetAxis("Horizontal");

            rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);

            if ((move < 0 && facingRight) || (move > 0 && !facingRight))
            {
                Flip();
            }

            if (jumping)
            {
                rb2d.AddForce(new Vector2(0f, jumpForce));
                jumping = false;
            }
        } else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
    }

    void PlayAnimations()
    {
        if (levelComplete)
        {
            anim.Play("Celebrate");
        }
        else if (!isAlive)
        {
            anim.Play("Die");
        }
        else if (grounded && rb2d.velocity.x != 0)
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        isAlive = false;
        Physics2D.IgnoreLayerCollision(9, 10); // interrompe a colisao
        SoundManager.instance.PlayFxPlayer(fxDie);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Exit"))
        {
            levelComplete = true;
            SoundManager.instance.PlayFxPlayer(fxWin);
        }
    }

    void DieAnimationFinished()
    {
        if (timeIsOver)
            GameManager.instance.SetOverlay(GameManager.GameStatus.LOSE);
        else
            GameManager.instance.SetOverlay(GameManager.GameStatus.DIE);
    }

    void CelebrateAnimationFinished()
    {
        GameManager.instance.SetOverlay(GameManager.GameStatus.WIN);
    }
}
