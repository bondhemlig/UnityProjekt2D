using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public const string META_SAVEFILENAME = "SaveGameNames";


    public GameObject creditsPanel;
    public TMPro.TMP_InputField newGameInputField;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }

    private save_script save_script;
    private load_Script load_script;
    public TextMeshProUGUI continueButtonText;
    public TMPro.TMP_Dropdown loadSaveGames, removeSaveGames, resolutionDropdown;

    // Start is called before the first frame update
    void Start()
    {

        save_script = new save_script();
        load_script = new load_Script();
        LevelSave savedData = load_script.load_data(META_SAVEFILENAME);
        Debug.Log("Saved data loaded: " + savedData.saveNames);
        if (savedData != null)
        {
            save_script.interLevelData = savedData;
        }
        print(new LevelSave().saveNames + "initials");
        populateDropdown(loadSaveGames);
        populateDropdown(removeSaveGames);

        print("Last save game: " + save_script.interLevelData.selectedSaveName);
        if (save_script.interLevelData.selectedSaveName  == null)
        {
            return;
        }

        LevelSave lastSaveData = load_script.load_data(save_script.interLevelData.selectedSaveName); //load the latest save and retrieve information to display below
        print(lastSaveData.totalCoinsCollected);
        continueButtonText.text = "Continue '" + save_script.interLevelData.selectedSaveName + "' \n Level: " + lastSaveData.currentLevel + ", " + lastSaveData.totalCoinsCollected + " Total Coins";
    }

    public void loadSaveGame()
    {
        string saveName = loadSaveGames.options[loadSaveGames.value].text;
        save_script.interLevelData.selectedSaveName = saveName;
        continue_game();
    }

    public void populateDropdown(TMPro.TMP_Dropdown dropdown)
    {
        dropdown.ClearOptions();
        List<string> m_DropOptions = save_script.interLevelData.saveNames.ToList<string>();
        m_DropOptions.RemoveAll(x => string.IsNullOrEmpty(x)); //clear empty strings.
        dropdown.AddOptions(m_DropOptions);
    }

    public void create_save_game()
    {
        string saveName = newGameInputField.text;
        print(save_script.interLevelData.saveNames);
        Array.Resize(ref save_script.interLevelData.saveNames, save_script.interLevelData.saveNames.Length + 1);

        ArrayUtility.Insert<string>(ref save_script.interLevelData.saveNames, save_script.interLevelData.saveNames.Length - 1, saveName);
        save_script.interLevelData.selectedSaveName = saveName;

        StartGame();
    }

    public void remove_save_game()
    {
       string saveName = removeSaveGames.options[removeSaveGames.value].text;
          
        print(saveName + " savename detected");
        ArrayUtility.Remove(ref save_script.interLevelData.saveNames, saveName);
        load_script.delete_data(saveName);
        save_script.save_game(META_SAVEFILENAME);
        populateDropdown(removeSaveGames);
        populateDropdown(loadSaveGames);
    }

    public void continue_game() {

        SceneManager.LoadScene(load_script.load_data(save_script.interLevelData.selectedSaveName).currentLevel);
    }

    private void OnDisable()
    {
        save_script.save_game(META_SAVEFILENAME);
    }

}