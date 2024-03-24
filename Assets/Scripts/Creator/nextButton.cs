using UnityEngine;

public class nextButton : MonoBehaviour
{
    /*public Transform buildings;
    private static Vector3[] fixedTransform = new Vector3[3];
    public static int lastChild = 0;
    public static int lastChildBack = 0;
    public static bool reset = false;
    void Start()
    {
        if (this.gameObject.name == "next (1)")
        {
            for (int i = 0; i < 3; i++)
            {
                fixedTransform[i] = buildings.GetChild(i).position;
            }

            CleanMenu();
            MenuBack(lastChild);
            //print(lastChild);
            buildings.parent.GetChild(2).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //print(testbuttons.GetUpNum());
        if ((this.gameObject.name == "next (1)" || !buildings.parent.GetChild(2).gameObject.activeSelf) && reset)
        {
            reset = false;
            CleanMenu(lastChildBack);

            print("l" + lastChild);
            print("b" + lastChildBack);

        }
    }
*/
    private void OnMouseUpAsButton()
    {
        if (this.gameObject.name == "next (1)")
        {
            menuController.leftDown = true;
            /*CleanMenu(lastChildBack);
            MenuBack(lastChildBack);
            buildings.parent.GetChild(1).gameObject.SetActive(true);*/
        }
        else
        {
            menuController.rightDown = true;
            /*CleanMenu(lastChild);
            MenuBack(lastChild);
            buildings.parent.GetChild(2).gameObject.SetActive(true);*/

        }
        if (BuildingsGrid.CanvasFather.childCount != 0)
        {
            Destroy(BuildingsGrid.CanvasFather.GetChild(0).gameObject);
        }
        //print(lastChild);

    }
}
    /*void CleanMenu(int lastCh = 0)
    {
        int j = 0;
        bool find = true;
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
                    buildings.GetChild(i).position = fixedTransform[j];
                    j++;
                } 
            }
        }
        if (lastCh == lastChild)
        {
            buildings.parent.GetChild(1).gameObject.SetActive(false);
            
        }
        
            //else this.gameObject.SetActive(true);
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
        if(lastCh == 0)
        {
            buildings.parent.GetChild(2).gameObject.SetActive(false);
        }
        *//*int j = 2;
        bool find = true;
        for (int i = buildings.childCount - 1; i >= 0; i--)
        {
            
            if (buildings.GetChild(i).gameObject.activeSelf)
            {

                if (j == 0 && find)
                {
                    lastChild = i;
                    find = false;
                }

                if (i >= lastCh || j < 0)
                {
                    buildings.GetChild(i).SetPositionAndRotation(new Vector3(-2, 0, 0), buildings.GetChild(i).rotation);
                }
                else
                {
                    buildings.GetChild(i).position = fixedTransform[j];
                    j--;
                }
            }
        }
        if (lastChild == 0)
        {
            buildings.parent.GetChild(2).gameObject.SetActive(false);
            CleanMenu();
        }
        //else this.gameObject.SetActive(true);*//*
    }
}
*/