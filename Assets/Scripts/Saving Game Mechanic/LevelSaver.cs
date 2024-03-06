using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSaver : MonoBehaviour
{
    private save_script save_script;
    private load_Script load_script;
    string metaFileName = MenuController.META_SAVEFILENAME;
    string thisSaveFileName;


    // Start is called before the first frame update
    void Start()
    {
        save_script = new save_script();
        load_script = new load_Script();
        thisSaveFileName = load_script.load_data(metaFileName).selectedSaveName;
        LevelSave previousData = load_script.load_data(thisSaveFileName);
        if (previousData != null)
        {
            save_script.interLevelData = previousData;
        }
    }

    public void setCurrentLevel(int level)
    {
        save_script.interLevelData.currentLevel = level;
    }

    public void saveProgress()
    {
        save_script.save_game(thisSaveFileName);
    }

    public void savePlayerStatsOnLevelEnd(PlayerState playerState)
    {
        if (save_script.interLevelData.totalCoinsCollected == null) //backward compability :7
        {
            save_script.interLevelData.totalCoinsCollected = 0;
        }

        save_script.interLevelData.totalCoinsCollected += playerState.coinsCollected;
        print("Increased coinscollected by " + playerState.coinsCollected + " to " + playerState.coinsCollected);
        saveProgress();
    }

    private void OnDisable()
    {
        saveProgress();
    }


}
