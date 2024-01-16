using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 7f;
    [SerializeField] private float jumpHeight = 150f;
    
    [SerializeField] private Transform GroundChecker;
    [SerializeField] private LayerMask GroundLayer;

    private Animator animator;
    private Rigidbody2D rigidbody2d;
    private bool isFacingRight;
    private bool isGrounded;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        isFacingRight = true;
        isGrounded = false;
    }

    private void Update()
    {
        if(isGrounded & Input.GetAxis("Jump") > 0)
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
            rigidbody2d.AddForce(new(x:0,y:jumpHeight));
        }
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        animator.SetFloat("speed",Mathf.Abs(move));
        rigidbody2d.velocity = new(
            x: move * maxSpeed,
            y:rigidbody2d.velocity.y);
        if((move > 0 && !isFacingRight) || (move < 0 && isFacingRight)) Flip();

        isGrounded = Physics2D.OverlapCircle(GroundChecker.position,15f,GroundLayer);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("VerticalSpeed", rigidbody2d.velocity.y);

    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new(
            x: transform.localScale.x * -1,
            y: transform.localScale.y);
    }
}
