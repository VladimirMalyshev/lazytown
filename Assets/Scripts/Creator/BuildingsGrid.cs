using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingsGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(100, 100);

    public static Building[,] grid;
    public Building[,] platforms;
    public Building flyingBuilding;
    public Building Nbuilding;
    private Camera mainCamera;

    public static int typeofBuildingPlant = 0;
    public static int lasttypeofBuildingPlant = 0;
    public static bool maymove = true;
    public static bool ScoreUp = false;
    public static bool butBuildDown = false;

    public static bool rem = false;
    public static Transform build = null;

    public Transform Canvas;
    public static Transform CanvasFather;
    

    public static int[] posBuildingsx = new int[40];
    public static int[] posBuildingsy = new int[40];
    public static int[] typeofBuilding = new int[40];
    

    
    public Building Platform;
    public Building Farm1;
    public Building House1;
    public Building Tower1;
    public Building Farm2;
    public Building House2;
    public Building Tower2;
    public Building Farm3;
    public Building House3;
    public Building Farm4;


    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];

        platforms = new Building[GridSize.x, GridSize.y];


        mainCamera = Camera.main;
        
    }

    private void Start()
    {
        
        CanvasFather = Canvas;
        LoadBuildings();
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            ScoreUp = true;
            Destroy(flyingBuilding.gameObject);
            
        }

        flyingBuilding = Instantiate(buildingPrefab);
    }

    private void Update()
    {
        if (butBuildDown)
        {
            switch (typeofBuildingPlant)
            {
                case 1:
                    StartPlacingBuilding(Platform);
                    break;
                case 2:
                    StartPlacingBuilding(House1);
                    break;
                case 3:
                    StartPlacingBuilding(Farm1);
                    break;
                case 4:
                    StartPlacingBuilding(House2);
                    break;
                case 5:
                    StartPlacingBuilding(Farm2);
                    break;
                case 6:
                    StartPlacingBuilding(Tower1);
                    break;
                case 7:
                    StartPlacingBuilding(Farm3);
                    break;
                case 8:
                    StartPlacingBuilding(Tower2);
                    break;
                case 9:
                    StartPlacingBuilding(House3);
                    break;
                case 10:
                    StartPlacingBuilding(Farm4);
                    break;

            }
            butBuildDown = false;
        }
        if (flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool available = true;

                if (x < 0 || x > GridSize.x - flyingBuilding.Size.x) available = false;
                if (y < 0 || y > GridSize.y - flyingBuilding.Size.y) available = false;

                if (available && IsPlaceTaken(x, y)) available = false;

                flyingBuilding.transform.position = new Vector3(x, 0, y);
                flyingBuilding.SetTransparent(available);

                if (available && Input.GetMouseButtonUp(0))
                {
                    PlaceFlyingBuilding(x, y);
                    maymove = true;
                }
                if (!available && Input.GetMouseButtonUp(0))
                {
                    ScoreUp = true;
                    Destroy(flyingBuilding.gameObject); 
                    lasttypeofBuildingPlant = typeofBuildingPlant;
                    typeofBuildingPlant = 0;
                    maymove = true;
                }
            }
        }
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                if (typeofBuildingPlant > 1)
                {
                    if (platforms[placeX + x, placeY + y] == null | grid[placeX + x, placeY + y] != null) return true;               
                }
                else if(typeofBuildingPlant == 1)
                {
                    if (platforms[placeX + x, placeY + y] != null) return true;
                }

                
            }
        }

        return false;
    }

    private void OnApplicationQuit()
    {
        SaveBuilding();
    }

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                if (typeofBuildingPlant > 1) grid[placeX + x, placeY + y] = flyingBuilding;
                else if (typeofBuildingPlant == 1) platforms[placeX + x, placeY + y] = flyingBuilding;
                
            }
        }
        
        flyingBuilding.SetNormal();
        flyingBuilding = null;
        typeofBuildingPlant = 0;
    }

    public void SaveBuilding()
    {
        int i = 0;
        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y] == grid[x, y - 1] || grid[x - 1, y] == grid[x, y]) continue;
                    posBuildingsx[i] = x;
                    posBuildingsy[i] = y;
                    typeofBuilding[i] = grid[x, y].typeofBuilding; 
                    i++;

                }

            }
        }
        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                if (platforms[x, y] != null)
                {
                    if (platforms[x, y] == platforms[x, y - 1] || platforms[x - 1, y] == platforms[x, y]) continue;
                    posBuildingsx[i] = x;
                    posBuildingsy[i] = y;
                    typeofBuilding[i] = platforms[x, y].typeofBuilding;
                    i++;
                }
            }
        }
        while (i < 40)
        {
            posBuildingsx[i] = 0;
            posBuildingsy[i] = 0;
            typeofBuilding[i] = 0;
            i++;
        }
    }


    public void LoadBuildings()
    {
        for (int i = 0; i < 40; i++)
        {           
           switch (typeofBuilding[i])
           {
                case 1:
                    Nbuilding = Instantiate(Platform);
                    break;
                case 2:
                    Nbuilding = Instantiate(House1);
                    break;
                case 3:
                    Nbuilding = Instantiate(Farm1);
                    break;
                case 4:
                    Nbuilding = Instantiate(House2);
                    break;
                case 5:
                    Nbuilding = Instantiate(Farm2);
                    break;
                case 6:
                    Nbuilding = Instantiate(Tower1);
                    break;
                case 7:
                    Nbuilding = Instantiate(Farm3);
                    break;
                case 8:
                    Nbuilding = Instantiate(Tower2);
                    break;
                case 9:
                    Nbuilding = Instantiate(House3);
                    break;
                case 10:
                    Nbuilding = Instantiate(Farm4);
                    break;

            }
            if (typeofBuilding[i] != 0)
            {
                Nbuilding.transform.position = new Vector3(posBuildingsx[i], 0, posBuildingsy[i]);
                for (int x = 0; x < Nbuilding.Size.x; x++)
                {
                    for (int y = 0; y < Nbuilding.Size.y; y++)
                    {
                        if (typeofBuilding[i] > 1) grid[posBuildingsx[i] + x, posBuildingsy[i] + y] = Nbuilding;
                        else if (typeofBuilding[i] == 1) platforms[posBuildingsx[i] + x, posBuildingsy[i] + y] = Nbuilding;

                    }
                }
                Nbuilding = null;
            }
        }           
    }

    public void loadMenu()
    {
        SaveBuilding();
        SaveProgress.loadMenu = true;
        SceneManager.LoadScene("Menu");
        
    }

}
