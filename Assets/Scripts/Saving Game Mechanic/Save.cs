using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class save_script
{

    LevelSave sav = new LevelSave();//this is the instance of the serializable class
    string file_path = Path.Combine(Application.persistentDataPath + "/gamedata.mine");//path of your file
    FileStream my_stream;//data stream connection to file


    public void save_game()
    {
        BinaryFormatter file_converter = new BinaryFormatter();
        if (File.Exists(file_path))
        {
            my_stream = new FileStream(file_path, FileMode.Append);
            file_converter.Serialize(my_stream, sav);
            my_stream.Close();
        }
        else
        {
            my_stream = new FileStream(file_path, FileMode.CreateNew);
            file_converter.Serialize(my_stream, sav);
            my_stream.Close();
        }


    }
}