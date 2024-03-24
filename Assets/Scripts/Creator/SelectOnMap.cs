using UnityEngine;
using UnityEngine.UI;

public class SelectOnMap : MonoBehaviour
{
    public int curType;
    public deleteBuilding delPref;
    private deleteBuilding delButton;
    public Vector3 position;

    void OnMouseUpAsButton()
    {
        BuildingsGrid.build = this.transform;
        for (int i = 0; i < BuildingsGrid.CanvasFather.childCount; i++)
        {
            Destroy(BuildingsGrid.CanvasFather.GetChild(i).gameObject);
        }
        delButton = Instantiate(delPref, BuildingsGrid.build.position + position, BuildingsGrid.CanvasFather.rotation, BuildingsGrid.CanvasFather);
        deleteBuilding.type = curType;
    }
    void Update()
    {
        if (BuildingsGrid.rem && deleteBuilding.type == curType)
        {
            BuildingsGrid.lasttypeofBuildingPlant = curType;
            BuildingsGrid.ScoreUp = true;
            Destroy(BuildingsGrid.build.gameObject);
            BuildingsGrid.rem = false;
        }
    }

}