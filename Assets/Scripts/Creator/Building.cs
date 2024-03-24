using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer MainRenderer;
    public Vector2Int Size = Vector2Int.one;
    public int typeofBuilding;

    public void SetTransparent(bool available)
    {
        if (available)
        {
            if(BuildingsGrid.typeofBuildingPlant == 1) MainRenderer.material.color = new Color(0, 0.96f, 0.475f, 1);
            else MainRenderer.material.color = Color.white;
        }
        else
        {
            MainRenderer.material.color = Color.red;
        }
    }


    public void SetNormal()
    {
        if (BuildingsGrid.typeofBuildingPlant == 1) MainRenderer.material.color = new Color(0, 0.96f, 0.475f, 1);
        else MainRenderer.material.color = Color.white;
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }
}