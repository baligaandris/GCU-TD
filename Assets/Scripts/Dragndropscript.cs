using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Dragndropscript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private int towerPrice;
    public GameObject tower;
    GameObject hoverTower;
    public GameObject[] buildableAreas;
    private GameDataScript gameData;
    public GameObject[] allTowers;
    public GameObject[] freeSlots;
    GameObject activeSlot;

    // Use this for initialization
    void Start()
    {
        towerPrice = tower.GetComponentInChildren<TowerShootsScript>().cost;
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataScript>();
        hoverTower = Instantiate(tower);
        hoverTower.SetActive(false);
        hoverTower.GetComponentInChildren<SpriteRenderer>().enabled = true;
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
              //  foreach (GameObject buildableArea in buildableAreas)
                //    foreach (GameObject tower in allTowers)
                      //  if (hits != null)
                        {
                            MaybeShowHoverTower(hits);

                int slotIndex = GetSlotIndex(hits);
                if (slotIndex != -1)
                {
                    GameObject slotQuad = hits[slotIndex].collider.gameObject;
                    activeSlot = slotQuad;
                    EnableSlot(slotQuad);
                }
                else
                {
                    activeSlot = null;
                    DisableAllSlots();
                }
           }
        }
    }

    void EnableSlot(GameObject slot)
    {
        foreach(GameObject freeSlot in freeSlots)
        {
            if (slot.name.Equals(freeSlot.name))
            {
                freeSlot.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                freeSlot.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    void DisableAllSlots()
    {
        foreach(GameObject freeSlot in freeSlots)
        {
            freeSlot.GetComponent<MeshRenderer>().enabled = false;
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

    int GetSlotIndex(RaycastHit[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if(hits[i].collider.gameObject.name.StartsWith("Slot"))
            {
                return i;
            }
        }
        return -1;
    }

    void MaybeShowHoverTower(RaycastHit[] hits)
    {
        int terrainColliderQuadIndex = GetTerrainColliderQuadIndex(hits);
        if (terrainColliderQuadIndex != -1)
        {
            hoverTower.transform.position = hits[terrainColliderQuadIndex].point;
            hoverTower.SetActive(true);
        }
        else
        {
            hoverTower.SetActive(false);
        }
    }

  

    public void OnEndDrag(PointerEventData eventData)
    {
        if(activeSlot != null)
        {
            Vector3 quadCenter = GetQuadCenter(activeSlot);
            Instantiate(tower, quadCenter, Quaternion.identity);
            activeSlot.SetActive(false);
            gameData.ChangeUsac(-towerPrice);
        }

        // Then set it to inactive ready for the next drag!
        hoverTower.SetActive(false);
    }

    Vector3 GetQuadCenter(GameObject quad)
    {
        Vector3[] meshVerts = quad.GetComponent<MeshFilter>().mesh.vertices;
        Vector3[] verticalRealWorldPositions = new Vector3[meshVerts.Length];

        for(int i = 0; i < meshVerts.Length; i++)
        {
            verticalRealWorldPositions[i] = quad.transform.TransformPoint(meshVerts[i]);
        }
        Vector3 midPoint = Vector3.Slerp(verticalRealWorldPositions[0], verticalRealWorldPositions[1], 0.5f);
        return midPoint;
    }
}