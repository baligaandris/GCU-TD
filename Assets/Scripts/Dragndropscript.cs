using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Dragndropscript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private int towerPrice;
    public GameObject tower;
    GameObject hoverTower;

    private GameDataScript gameData;

    // Use this for initialization
    void Start()
    {
        towerPrice = tower.GetComponentInChildren<TowerShootsScript>().cost;
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataScript>();
        hoverTower = Instantiate(tower);
        hoverTower.SetActive(false);
        RemoveFunctionFromPrefab();
    }

    void RemoveFunctionFromPrefab()
    {
        Component[] components = hoverTower.GetComponentsInChildren<TowerShootsScript>();
        foreach (Component component in components)
        {
            Destroy(component);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gameData.usac >= towerPrice)
        {
            RaycastHit[] hits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hits = Physics.RaycastAll(ray, 50f);
            if (hits != null && hits.Length > 0)
            {
                int terrainCollderQuadIndex = GetTerrainColliderQuadIndex(hits);
                if (terrainCollderQuadIndex != -1)
                {
                    hoverTower.transform.position = hits[terrainCollderQuadIndex].point;
                    hoverTower.SetActive(true);
                    // Debug.Log (hits [terrainCollderQuadIndex].point);
                }
                else
                {
                    hoverTower.SetActive(false);
                }
            }
        }
    }

    int GetTerrainColliderQuadIndex(RaycastHit[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.name.Equals("TerrainColliderQuad"))
            {
                return i;
            }
        }

        return -1;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // If the prefab instance is active after dragging stopped, it means
        // it's in the arena so (for now), just drop it in.
        if (hoverTower.activeSelf)
        {
            Instantiate(tower, hoverTower.transform.position, Quaternion.identity);
            gameData.ChangeUsac(-towerPrice);
        }

        // Then set it to inactive ready for the next drag!
        hoverTower.SetActive(false);
    }
}
