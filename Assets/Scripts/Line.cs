using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject ballSound;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Instantiate(ballSound, transform.position, Quaternion.identity);
            anim.SetTrigger("Ball");
        }
    }

}
