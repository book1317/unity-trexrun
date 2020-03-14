using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    public float jumpForce = 10.0f;
    private bool canJump = true;

    private LevelController theLevel;
    [SerializeField] Sprite crouchSprite;
    [SerializeField] Sprite standSprite;
    [SerializeField] SpriteRenderer theSprite;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        theLevel = FindObjectOfType<LevelController>();
        theSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (theLevel.currentState == LevelController.GameState.Playing)
        {
            if (Input.anyKey)
            {
                if (Input.GetButtonDown("Jump") && canJump)
                {
                    Jump();
                }
            }

            if (Input.GetButtonDown("Down") && canJump)
            {
                Crouch();
            }
            if (Input.GetButtonUp("Down") && canJump)

            {
                Stand();
            }
        }
    }

    void Jump()
    {
        Stand();
        myRigidbody.velocity = new Vector3(0, jumpForce, 0);
        canJump = false;
    }

    void Crouch()
    {
        theSprite.sprite = crouchSprite;
        GetComponents<BoxCollider2D>()[1].enabled = true;
        GetComponents<BoxCollider2D>()[0].enabled = false;
    }

    void Stand()
    {
        theSprite.sprite = standSprite;
        GetComponents<BoxCollider2D>()[1].enabled = false;
        GetComponents<BoxCollider2D>()[0].enabled = true;
    }

    void Death()
    {
        theLevel.OnDeath();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Death();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}
