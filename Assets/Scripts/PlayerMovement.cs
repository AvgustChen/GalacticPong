using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isAI;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject playerBlueButton;
    private Rigidbody2D rb;
    private Vector2 playerMove;
    private int move;
    private Animator anim;
    [SerializeField] private GameObject ballSound;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        move = 1;
        if (GameMode.twoPlayer == true && gameObject.name == "PlayerBlue")
        {
            isAI = false;
            playerBlueButton.SetActive(true);
        }
        else if (GameMode.twoPlayer == false && gameObject.name == "PlayerBlue")
        {
            playerBlueButton.SetActive(false);
            isAI = true;
            if (GameMode.levelAI == 1)
            {
                moveSpeed = 2;
            }
            else if (GameMode.levelAI == 2)
            {
                moveSpeed = 4;
            }
            else if (GameMode.levelAI == 1)
            {
                moveSpeed = 7;
            }
        }
    }

    void Update()
    {
        if (isAI)
        {
            AIControl();
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.LeftControl) && gameObject.name == "PlayerRed")
                MoveButton();
            if(Input.GetKeyDown(KeyCode.RightControl) && gameObject.name == "PlayerBlue")
                MoveButton();
            PlayerControl();

        }
    }

    private void PlayerControl()
    {
        playerMove = new Vector2(0, move);

    }

    private void AIControl()
    {
        if (ball.transform.position.y > transform.position.y + 0.5f)
        {
            playerMove = new Vector2(0, 1);
        }
        else if (ball.transform.position.y < transform.position.y - 0.5f)
        {
            playerMove = new Vector2(0, -1);
        }
        else
        {
            playerMove = new Vector2(0, 0);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = playerMove * moveSpeed;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Instantiate(ballSound, transform.position, Quaternion.identity);
            anim.SetTrigger("Ball");
        }
    }
    public void MoveButton()
    {
        if (move == 1)
        {
            move = -1;
        }
        else if (move == -1)
        {
            move = 1;
        }
    }
}
