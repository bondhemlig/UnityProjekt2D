using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting.FullSerializer;

public class save_script
{

    public LevelSave interLevelData = new LevelSave();//this is the instance of the serializable class
    
    FileStream my_stream;//data stream connection to file


    public void save_game(string fileName)
    {

        string file_path = Path.Combine(Application.persistentDataPath + "/" + fileName +".txt");//path of your file
        Debug.Log(file_path +  " File path");
        BinaryFormatter file_converter = new BinaryFormatter();
        if (File.Exists(file_path))
        {
            my_stream = new FileStream(file_path, FileMode.Truncate); //Truncate rather than Append.. ?
            file_converter.Serialize(my_stream, interLevelData);
            my_stream.Close();
        }
        else
        {
            my_stream = new FileStream(file_path, FileMode.CreateNew);
            
            file_converter.Serialize(my_stream, interLevelData);
            
            my_stream.Close();
        }
    }
}