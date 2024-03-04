using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour
{
    LevelSave saveData = new LevelSave();
    bool gameSaving = false;

    public GameObject dialougeBox, finishedText, unFinishedText;
    public int coinsQuestGoal = 20;
    public int LevelToLoad;
    private bool levelIsLoading;

    private BoxCollider2D checkpointCollider;
    private Animator animationController;
    private AudioSource audioSource;
    public AudioClip finishQuestAudioClip;



    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        animationController = GetComponent<Animator>();
        checkpointCollider = GetComponent<BoxCollider2D>();
    }

    private void onLevelCompleted()
    {
        if (!gameSaving)
        {
            gameSaving = true;
            saveData.currentLevel = LevelToLoad;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollision)
    {
        if (otherCollision.CompareTag("Player"))
        {
            PlayerState state = otherCollision.GetComponent<PlayerState>();

            if (state.coinsCollected >= coinsQuestGoal)
            {
                animationController.SetTrigger("RaiseThisFlag");

                finishedText.SetActive(true);
                unFinishedText.SetActive(false);
                onLevelCompleted();

                Invoke("LoadNextLevel", 7.5f);
                if (!levelIsLoading)
                {
                    
                }
                levelIsLoading = true;
                
            }
            else
            {
                finishedText.SetActive(false);
                unFinishedText.SetActive(true);
            }
            dialougeBox.SetActive(true);


        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(LevelToLoad);

    }

    private void OnTriggerExit2D(Collider2D otherCollision)
    {
        if (otherCollision.CompareTag("Player") && checkpointCollider.IsTouching(otherCollision) == false && !levelIsLoading)
        {
            finishedText.SetActive(false);
            unFinishedText.SetActive(false);
            dialougeBox.SetActive(false);

        }
        
    }

}
