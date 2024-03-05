using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Tester : MonoBehaviour
{
    private save_script save_script; 
    private load_Script load_script;
    public TMPro.TMP_Text textLabel;
    string fileName = "TestData";
    

    // Start is called before the first frame update
    void Start()
    {
        
        save_script = new save_script();
        load_script = new load_Script();


        LevelSave savedData = load_script.load_data(fileName);
        Debug.Log("Saved data: " + savedData);
        if (savedData != null )
        {
            save_script.interLevelData = savedData;
        }

        textLabel.text = "Loading..";
        textLabel.text = "Level integer loaded: " + save_script.interLevelData.currentLevel;
        print("Data Loaded: " + save_script.interLevelData.currentLevel);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) {
            print(" current level incremented to " + save_script.interLevelData.currentLevel);
            save_script.interLevelData.currentLevel += 1;
        }
    }

    private void OnDisable()
    {
        print("Disable");
        save_script.save_game(fileName);
    }
}
