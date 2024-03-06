using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour
{
    private LevelSaver saveLevel;
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

        saveLevel = gameObject.AddComponent<LevelSaver>();
        audioSource = gameObject.AddComponent<AudioSource>();
        animationController = GetComponent<Animator>();
        checkpointCollider = GetComponent<BoxCollider2D>();
    }

    private void onLevelCompleted()
    {
        saveLevel.setCurrentLevel(LevelToLoad);
        
    }

    public void checkJumpToLevel()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {

                print("Tab");
            print(!levelIsLoading);
            //no this does not work
                if ( Input.GetKeyDown(KeyCode.Keypad0) && !levelIsLoading )
                {
                    levelIsLoading = true;
                    int levelNumber = int.Parse("0");
                    print(levelNumber + "큗ound");
                    SceneManager.LoadScene(levelNumber);
                }
                if (Input.GetKeyDown(KeyCode.Alpha1) && !levelIsLoading)
                {
                    levelIsLoading = true;
                    int levelNumber = int.Parse("1");
                    print(levelNumber + "큗ound");
                    SceneManager.LoadScene(levelNumber);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2) && !levelIsLoading)
                {
                    levelIsLoading = true;
                    int levelNumber = int.Parse("2");
                    print(levelNumber + "큗ound");
                    SceneManager.LoadScene(levelNumber);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3) && !levelIsLoading)
                {
                    levelIsLoading = true;
                    int levelNumber = int.Parse("3");
                    print(levelNumber + "큗ound");
                    SceneManager.LoadScene(levelNumber);
                }

        }
    }

    private void Update()
    {
        checkJumpToLevel();
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
                    levelIsLoading = true;
                    saveLevel.savePlayerStatsOnLevelEnd(state);
                }
                
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
