using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTilemap : MonoBehaviour
{
    public float offsetX;
    public float offsetY;

    public GameObject breakeffects;
    public GameObject grid;

    private Tilemap destructibleTilemap;

    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 pos3;
    private Vector3 pos4;
    private Vector3 pos5;
    private Vector3 pos6;
    private Vector3 pos7;
    private Vector3 pos8;

    // Start is called before the first frame update
    void Start()
    {
        destructibleTilemap = GetComponent<Tilemap>();
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            Vector3 hitPos = other.ClosestPoint(other.transform.position);
            pos1 = new Vector3(hitPos.x + offsetX, hitPos.y, 0f);
            pos2 = new Vector3(hitPos.x - offsetX, hitPos.y, 0f);
            pos3 = new Vector3(hitPos.x, hitPos.y + offsetY, 0f);
            pos4 = new Vector3(hitPos.x, hitPos.y - offsetY, 0f);
            pos5 = new Vector3(hitPos.x + offsetX, hitPos.y + offsetY, 0f);
            pos6 = new Vector3(hitPos.x + offsetX, hitPos.y - offsetY, 0f);
            pos7 = new Vector3(hitPos.x - offsetX, hitPos.y + offsetY, 0f);
            pos8 = new Vector3(hitPos.x - offsetX, hitPos.y - offsetY, 0f);
            Vector3Int position = destructibleTilemap.WorldToCell(pos1);
            if (destructibleTilemap.GetTile(position) != null)
            {
                destructibleTilemap.SetTile(position, null);
                GameObject emmiter1 = Instantiate(breakeffects, position, Quaternion.identity, grid.transform);
            }
            position = destructibleTilemap.WorldToCell(pos2);
            if (destructibleTilemap.GetTile(position) != null)
            {
                destructibleTilemap.SetTile(position, null);
                GameObject emmiter2 = Instantiate(breakeffects,position, Quaternion.identity, grid.transform);
            }
            position = destructibleTilemap.WorldToCell(pos3);
            if (destructibleTilemap.GetTile(position) != null)
            {
                destructibleTilemap.SetTile(position, null);
                GameObject emmiter3 = Instantiate(breakeffects, position, Quaternion.identity, grid.transform);
            }
            position = destructibleTilemap.WorldToCell(pos4);
            if (destructibleTilemap.GetTile(position) != null)
            {
                destructibleTilemap.SetTile(position, null);
                GameObject emmiter4 = Instantiate(breakeffects, position, Quaternion.identity, grid.transform);
            }
            position = destructibleTilemap.WorldToCell(pos5);
            if (destructibleTilemap.GetTile(position) != null)
            {
                destructibleTilemap.SetTile(position, null);
                GameObject emmiter5 = Instantiate(breakeffects, position, Quaternion.identity, grid.transform);
            }
            position = destructibleTilemap.WorldToCell(pos6);
            if (destructibleTilemap.GetTile(position) != null)
            {
                destructibleTilemap.SetTile(position, null);
                GameObject emmiter6 = Instantiate(breakeffects, position, Quaternion.identity, grid.transform);
            }
            position = destructibleTilemap.WorldToCell(pos7);
            if (destructibleTilemap.GetTile(position) != null)
            {
                destructibleTilemap.SetTile(position, null);
                GameObject emmiter7 = Instantiate(breakeffects, position, Quaternion.identity, grid.transform);
            }
            position = destructibleTilemap.WorldToCell(pos8);
            if (destructibleTilemap.GetTile(position) != null)
            {
                destructibleTilemap.SetTile(position, null);
                GameObject emmiter8 = Instantiate(breakeffects, position, Quaternion.identity, grid.transform);
            }
        }
    }
}
