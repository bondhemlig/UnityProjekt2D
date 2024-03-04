using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public GameObject Box;

    private Animator anim;
    private bool hasPlayedAnimation = false;
    private AudioSource audioSource;
    public AudioClip activateBox;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

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
