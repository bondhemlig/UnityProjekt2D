using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class load_Script
{
    LevelSave sav = new LevelSave();

    string file_path = Path.Combine(Application.persistentDataPath + "/gamedata.mine");
    FileStream my_stream;


    public LevelSave load_data()
    {
        BinaryFormatter file_converter = new BinaryFormatter();
        if (File.Exists(file_path))
        {
            my_stream = new FileStream(file_path, FileMode.Open);
            LevelSave lod = file_converter.Deserialize(my_stream) as LevelSave;
            my_stream.Close();
            return lod;
        }
        else
        {
            Debug.Log("File doesn't exist");
            return null;
        }


    }
}