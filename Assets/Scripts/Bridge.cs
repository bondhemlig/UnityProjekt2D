using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public GameObject Box;

    private Animator anim;
    private bool hasPlayedAnimation = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !hasPlayedAnimation)
        {
            hasPlayedAnimation = true;
            Box.SetActive(false);
            anim.SetTrigger("Move");

        }
    }
}
