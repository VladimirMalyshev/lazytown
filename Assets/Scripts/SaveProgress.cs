using UnityEngine;
using System.IO;
using System;
 
public class SaveProgress : MonoBehaviour
{
    public Save sv = new Save();
    public static bool loadMenu = false;
    public static bool loadCity = false;
    private string path;

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        path = Path.Combine(Application.dataPath, "Save.json");
#endif
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
            Array.Copy(sv.progress, Timer.scoreofProgress, 9);
            Array.Copy(sv.normprogress, Timer.normofProgress, 9);
            Array.Copy(sv.typeprogress, Timer.itemofProgress, 9);
            Array.Copy(sv.posBuildingsx, BuildingsGrid.posBuildingsx, 40);
            Array.Copy(sv.posBuildingsy, BuildingsGrid.posBuildingsy, 40);
            Array.Copy(sv.typeofBuilding, BuildingsGrid.typeofBuilding, 40);
        }
        else Debug.Log("file doesnt exist");
    }


#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            File.WriteAllText(path, JsonUtility.ToJson(sv));
            Array.Copy(sv.progress, Timer.scoreofProgress, 9);
            Array.Copy(sv.normprogress, Timer.normofProgress, 9);
            Array.Copy(sv.typeprogress, Timer.itemofProgress, 9);
            Array.Copy(sv.posBuildingsx, BuildingsGrid.posBuildingsx, 40);
            Array.Copy(sv.posBuildingsy, BuildingsGrid.posBuildingsy, 40);
            Array.Copy(sv.typeofBuilding, BuildingsGrid.typeofBuilding, 40);
        }
    }
#endif
    private void OnApplicationQuit()
    {
        
        Array.Copy(Timer.scoreofProgress, sv.progress, 9);
        Array.Copy(Timer.normofProgress, sv.normprogress, 9);
        Array.Copy(Timer.itemofProgress, sv.typeprogress, 9);
        Array.Copy(BuildingsGrid.posBuildingsx, sv.posBuildingsx, 40);
        Array.Copy(BuildingsGrid.posBuildingsy, sv.posBuildingsy, 40);
        Array.Copy(BuildingsGrid.typeofBuilding, sv.typeofBuilding, 40);
        File.WriteAllText(path, JsonUtility.ToJson(sv));
        
    }

    private void Update()
    {
        if (loadMenu)
        {           
            Array.Copy(BuildingsGrid.posBuildingsx, sv.posBuildingsx, 40);
            Array.Copy(BuildingsGrid.posBuildingsy, sv.posBuildingsy, 40);
            Array.Copy(BuildingsGrid.typeofBuilding, sv.typeofBuilding, 40);
            File.WriteAllText(path, JsonUtility.ToJson(sv));
            loadMenu = false;
        }
        if (loadCity)
        {
            Array.Copy(Timer.scoreofProgress, sv.progress, 9);
            Array.Copy(Timer.itemofProgress, sv.typeprogress, 9);
            Array.Copy(Timer.normofProgress, sv.normprogress, 9);           
            File.WriteAllText(path, JsonUtility.ToJson(sv));
            loadCity = false;
        }
    }
}
[Serializable]
public class Save
{
    public int[] progress = new int[9];
    public int[] normprogress = new int[9];
    public int[] typeprogress = new int[9];
    public int[] posBuildingsx = new int[40];
    public int[] posBuildingsy = new int[40];
    public int[] typeofBuilding = new int[40];
}