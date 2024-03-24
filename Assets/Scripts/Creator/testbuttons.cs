using UnityEngine;
using UnityEngine.UI;

public class testbuttons : MonoBehaviour
{
    //public Building buildingPrefab;
    public int typeofBuilding;
    public int count = 10;
    public Text countText;

    private void Start()
    {
        countText.text = count.ToString();
    }
    void OnMouseDown()
    {
        if (count > 0)
        {
            count--;
            countText.text = count.ToString();
            BuildingsGrid.butBuildDown = true;
            BuildingsGrid.typeofBuildingPlant = typeofBuilding;
            BuildingsGrid.maymove = false;
            if (BuildingsGrid.CanvasFather.childCount != 0)
            {
                Destroy(BuildingsGrid.CanvasFather.GetChild(0).gameObject);
            }
            menuController.updateMenu = true;
        }
    } 
}
    
