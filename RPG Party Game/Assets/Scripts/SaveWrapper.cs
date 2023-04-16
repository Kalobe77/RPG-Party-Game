using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveWrapper : MonoBehaviour
{
    // Allow Access to Player Character Status
    public PlayerCharacterStatus pcs;

    public void writeToFile()
    {
        string jSONresult = pcs.SaveToString();
        File.WriteAllText(Application.dataPath + "/Saves/saveFile.txt", jSONresult);
    }

    public void readFromFile(string fileName)
    {
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + fileName),pcs);
    }
}
