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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, layerGround);
    }

    private void FixedUpdate()
    {
        
    }
}
