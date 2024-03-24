using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteBuilding : MonoBehaviour
{
    public static int type = 0;
    private void OnMouseDown()
    {
        switch(gameObject.name)
        {
            case "delete(Clone)":
                BuildingsGrid.rem = true;
                Destroy(BuildingsGrid.CanvasFather.GetChild(0).gameObject);
                break;
            case "Image":
                if (BuildingsGrid.CanvasFather.childCount != 0) 
                {
                    Destroy(BuildingsGrid.CanvasFather.GetChild(0).gameObject);
                } 
                break;
        }
    }
    
}
