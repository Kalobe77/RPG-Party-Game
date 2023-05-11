/********************************************************************************
 *   Filename:   SaveWrapper.cs
 *   Date:       2023-05-10
 *   Authors:    Kaleb Gearinger and Adam Stefan
 *   Email:      kgearinger@muhlenberg.edu and astefan@muhlenberg.edu
 *   Description:
 *       This file handles both reading from text files to set the values of the
 *       PlayerCharacterStatus object to the proper values as well as writing the
 *       values to a txt files when saving.
 ********************************************************************************/

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
        Debug.Log(Application.dataPath + "saveFile.txt");
        File.WriteAllText(Application.dataPath + "/Saves/saveFile.txt", jSONresult);
    }

    public void readFromFile(string fileName)
    {
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + fileName),pcs);
    }
}
