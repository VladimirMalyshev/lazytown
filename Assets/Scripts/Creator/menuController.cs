using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuController : MonoBehaviour
{
    public Transform buildings;
    private Vector3[] fixedTransform = new Vector3[3];
    public int lastChild = 0;
    public int lastChildBack = 0;
    public static bool reset = false;
    public static bool rightDown = false;
    public static bool leftDown = false;
    public static bool updateMenu = false;
    private bool lastLeft = false;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            fixedTransform[i] = buildings.GetChild(i).localPosition;
        }

        for (int i = 0; i < Timer.itemofProgress.Length; i++)
        {
            if (Timer.itemofProgress[i] == 0) continue;           
            buildings.GetChild(Timer.itemofProgress[i]).gameObject.GetComponent<testbuttons>().count = Timer.scoreofProgress[i];
            buildings.GetChild(Timer.itemofProgress[i]).gameObject.SetActive(true);
            for (int j = 0; j < BuildingsGrid.typeofBuilding.Length; j++)
            {
                if (BuildingsGrid.typeofBuilding[j] - 1 == Timer.itemofProgress[i] && BuildingsGrid.typeofBuilding[j] != 1)
                {
                    buildings.GetChild(Timer.itemofProgress[i]).gameObject.GetComponent<testbuttons>().count--;
                }
            }
            
        }

        CleanMenu();
        MenuBack(lastChild);
        buildings.parent.GetChild(2).gameObject.SetActive(false);
      
        updateMenu = true;
    }

    void Update()
    {
        if (reset)
        {
            reset = false;
            //int cache = lastChild;
            if (lastLeft)
            {
                MenuBack(lastChild + 1);
                lastLeft = false;
            }
            CleanMenu(lastChildBack);
            
            MenuBack(lastChildBack);
            
        }
        if (leftDown)
        {
            leftDown = false;
            lastLeft = true;
            CleanMenu(lastChildBack);
            MenuBack(lastChildBack);
            buildings.parent.GetChild(1).gameObject.SetActive(true);
        }

        if (rightDown)
        {
            rightDown = false;
            lastLeft = false;
            CleanMenu(lastChild);
            MenuBack(lastChild);
            buildings.parent.GetChild(2).gameObject.SetActive(true);
        }
        if (updateMenu)
        {
            updateMenu = false;
            for (int i = 0; i < buildings.childCount; i++)
            {
                GameObject item = buildings.GetChild(i).gameObject;
                if (item.GetComponent<testbuttons>().count == 0 && item.activeSelf)
                {
                    item.SetActive(false);
                    reset = true;
                }
                else if (item.GetComponent<testbuttons>().count != 0 && !item.activeSelf)
                {
                    item.SetActive(true);
                    reset = true;
                    
                }
            }
            
        }


        if (BuildingsGrid.ScoreUp)
        {
            for (int i = 0; i < buildings.childCount; i++)
            {
                GameObject item = buildings.GetChild(i).gameObject;
                int itemType = item.GetComponent<testbuttons>().typeofBuilding;
                if (BuildingsGrid.typeofBuildingPlant == itemType || ((BuildingsGrid.lasttypeofBuildingPlant == itemType) && (BuildingsGrid.lasttypeofBuildingPlant != 0)))
                {
                    item.GetComponent<testbuttons>().count++;
                    item.GetComponent<testbuttons>().countText.text = item.GetComponent<testbuttons>().count.ToString();
                    
                    BuildingsGrid.lasttypeofBuildingPlant = 0;
                    updateMenu = true;
                }

            }
            BuildingsGrid.ScoreUp = false;
        }
    }

    void CleanMenu(int lastCh = 0)
    {
        int j = 0;
        bool find = true;
        int buf = lastChild;
        for (int i = 0; i < buildings.childCount; i++)
        {

            if (buildings.GetChild(i).gameObject.activeSelf)
            {

                if (j == 3 && find)
                {
                    lastChild = i;
                    find = false;
                }

                if (i < lastCh || j > 2)
                {
                    buildings.GetChild(i).SetPositionAndRotation(new Vector3(-2, 0, 0), buildings.GetChild(i).rotation);
                }
                else
                {
                    buildings.GetChild(i).localPosition = fixedTransform[j];
                    j++;
                }
            }
        }       
        if (buf == lastChild)
        {
            buildings.parent.GetChild(1).gameObject.SetActive(false);
            print("zhopa");
        }       
    }
    void MenuBack(int lastCh = 0)
    {
        int j = 0;
        bool find = true;
        for (int i = lastCh - 1; i >= 0; i--)
        {
            if (buildings.GetChild(i).gameObject.activeSelf)
            {
                j++;
                if (j == 3 && find)
                {
                    lastChildBack = i;
                    find = false;
                }
            }
        }
        if (lastCh == 0)
        {
            buildings.parent.GetChild(2).gameObject.SetActive(false);
        }
    }
}
