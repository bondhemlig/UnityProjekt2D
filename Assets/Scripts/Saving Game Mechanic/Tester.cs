using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Tester : MonoBehaviour
{
    private LevelSave saveData; 
    private save_script saver; 
    private load_Script loader;
    public TMPro.TMP_Text textLabel;

    // Start is called before the first frame update
    void Start()
    {
        
        saveData = new LevelSave();
        saver = new save_script();
        loader = new load_Script();
        saver.save_game();
        loader.load_data();
        textLabel.text = "Loading..";
        textLabel.text = "Level integer loaded: " + saveData.currentLevel;
        print("Data Loaded: " + saveData.currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) {
            print(" current level incremented to " + saveData.currentLevel);
            saveData.currentLevel += 1;
            saver.save_game();
        }

    }
}
