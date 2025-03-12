using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTerraforming : MonoBehaviour
{
    public Tilemap destroyableTerrainTilemap;
    public int hitsToDestroy = 10; // Number of hits required to destroy a block
    public float reachDistance = 1.5f; // Distance the player can reach to destroy blocks

    private int currentHits = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Assuming left mouse button for interaction
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickedCellPosition = destroyableTerrainTilemap.WorldToCell(mouseWorldPos);
            TileBase clickedTile = destroyableTerrainTilemap.GetTile(clickedCellPosition);

            if (clickedTile != null && CanDestroyBlock(clickedCellPosition))
            {
                currentHits++;
                if (currentHits >= hitsToDestroy)
                {
                    destroyableTerrainTilemap.SetTile(clickedCellPosition, null);
                    currentHits = 0; // Reset hit count after destroying the block
                }
            }
        }
    }

    bool CanDestroyBlock(Vector3Int position)
    {
        Vector3 playerPosition = transform.position;
        Vector3 blockPosition = destroyableTerrainTilemap.GetCellCenterWorld(position);

        return Vector3.Distance(playerPosition, blockPosition) <= reachDistance;
    }
}

