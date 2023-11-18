using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D rb;
    public float speed; //movement
    public float horizontal;
    public float vertical;
    public float jumpingPower; //jump
    public bool isFacingRight; //flip
    public Animator animator;
    private bool isWalking;
    private bool isJumping;

    public Transform groundCheck; //jump
    public LayerMask groundLayer; //jump

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Flip();
        HandleAnimation();
    }
    void PlayerMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
        Debug.Log("Horizontal Value: " + horizontal);
        if (Input.GetButtonDown("Jump") && IsGrounded()) // if button is hit and player is grounded
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower); //JUMP
            

        }

        isWalking = horizontal != 0 ? true : false;
        isJumping = rb.velocity.y != 0 ? true : false;
       
    }

    void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void HandleAnimation()
    {

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isJumping", isJumping);

    }

    private void LateUpdate()
    {
        
    }

    private bool IsGrounded() // jump
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void OnTriggerEnter2D(Collider2D collision) //DeadZone (GameOver)
    {
        if (collision.gameObject.tag == "Spike")
        {
            Die();
            Debug.Log("Dead");
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
