using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float startSpeed = 10f;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private Text playerRedScore;
    [SerializeField] private Text playerBlueScore;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject winRed;
    [SerializeField] private GameObject winBlue;
    [SerializeField] private GameObject goalRed;
    [SerializeField] private GameObject goalBlue;

    private int hitCounter;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }
    void Update()
    {
        if (playerRedScore.text == "5")
        {
            Destroy(GameObject.FindGameObjectWithTag("goal"));
            winPanel.SetActive(true);
            winRed.SetActive(true);
            Time.timeScale = 0;
        }
        else if (playerBlueScore.text == "5")
        {
            Destroy(GameObject.FindGameObjectWithTag("goal"));
            winPanel.SetActive(true);
            winBlue.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            winPanel.SetActive(false);
            winBlue.SetActive(false);
            winRed.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, startSpeed + (speedIncrease * (hitCounter / 2)));
    }

    private void StartBall()
    {
        int rand = Random.Range(-1, 1);
        if (rand == 0) { rand = 1; }
        rb.velocity = new Vector2(rand, 0) * (startSpeed + speedIncrease * hitCounter);
    }

    private void ResetBall()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        hitCounter = 0;
        Invoke("StartBall", 2f);
    }

    private void PlayerBounce(Transform myObject)
    {
        hitCounter++;

        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;
        float xDirection, yDirection;
        if (transform.position.x > 0)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }
        yDirection = (ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        if (yDirection == 0)
        {
            yDirection = 0.25f;
        }
        rb.velocity = new Vector2(xDirection, yDirection) * (startSpeed + (speedIncrease * hitCounter));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PlayerRed")
        {
            PlayerBounce(collision.transform);
        }
        else if (collision.gameObject.name == "PlayerBlue")
        {
            PlayerBounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.position.x > 0)
        {
            ResetBall();
            Instantiate(goalBlue, new Vector2(8.23f, 0), Quaternion.identity);
            playerRedScore.text = (int.Parse(playerRedScore.text) + 1).ToString();
        }
        else if (transform.position.x < 0)
        {
            ResetBall();
            Instantiate(goalRed, new Vector2(-8.23f, 0), Quaternion.identity);
            playerBlueScore.text = (int.Parse(playerBlueScore.text) + 1).ToString();
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
