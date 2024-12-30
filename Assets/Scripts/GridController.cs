using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour{

    public Vector2 gridSize;
    public GameObject gridTilePrefab;

    public GameObject[,] cells;

    void Start(){
        cells = new GameObject[(int)gridSize.x, (int)gridSize.y];

        GenerateGrid();
    }

    void Update(){

    }

    void GenerateGrid() {
        
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                GameObject cell = Instantiate(gridTilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                cells[x, y] = cell;
            }
        }
        
    }

    public void ResetAllCells() {
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                cells[x, y].GetComponent<CellBehaviour>().SetAlive(false);
            }
        }
    }
    
    public List<GameObject> GetNeighbors(GameObject cell) {
        List<GameObject> neighbors = new List<GameObject>();

        Vector2 cellPosition = cell.transform.position;
        
        for (int x = Mathf.RoundToInt(cellPosition.x) - 1; x <= Mathf.RoundToInt(cellPosition.x) + 1; x++) {
            for (int y = Mathf.RoundToInt(cellPosition.y) - 1; y <= Mathf.RoundToInt(cellPosition.y) + 1; y++) {
                
                if (x == Mathf.RoundToInt(cellPosition.x) && y == Mathf.RoundToInt(cellPosition.y))
                    continue;

                if (x < 0 || x >= gridSize.x || y < 0 || y >= gridSize.y)
                    continue;

                if (cells[x, y].GetComponent<CellBehaviour>().isAlive)
                    neighbors.Add(cells[x, y]);

            }
        }

        return neighbors;
    }

    public int GetAliveNeighbors(GameObject cell) {
        
        int aliveNeighbors = 0;
        Vector2 cellPosition = cell.transform.position;


        for (int x = Mathf.RoundToInt(cellPosition.x) - 1; x <= Mathf.RoundToInt(cellPosition.x) + 1; x++) {
            for (int y = Mathf.RoundToInt(cellPosition.y) - 1; y <= Mathf.RoundToInt(cellPosition.y) + 1; y++) {

                if (x == Mathf.RoundToInt(cellPosition.x) && y == Mathf.RoundToInt(cellPosition.y))
                    continue;

                if (x < 0 || x >= gridSize.x || y < 0 || y >= gridSize.y)
                    continue;

                if (cells[x, y].GetComponent<CellBehaviour>().isAlive)
                    aliveNeighbors++;

            }
        }

        return aliveNeighbors;
    }
    
    public void UpdateGrid() {

        List<Cell> cellsToUpdate = new List<Cell>();

        foreach (GameObject cell in cells) {
            // Less than 2 neighbors, die by underpopulation
            // More than 3 neighbors, die by overpopulation
            // Exactly 3 neighbors, become alive
            // Exactly 2 or 3 neighbors, stay alive

            int aliveNeighbors = GetAliveNeighbors(cell);

            if (cell.GetComponent<CellBehaviour>().isAlive) {

                if (aliveNeighbors < 2 || aliveNeighbors > 3) {
                    Cell temp = new Cell();

                    temp.cellPosition = cell.GetComponent<CellBehaviour>().cellPosition;
                    temp.isAlive =false;

                    cellsToUpdate.Add(temp);

                }

            } else {
                if (aliveNeighbors == 3) {
                    Cell temp = new Cell();

                    temp.cellPosition = cell.GetComponent<CellBehaviour>().cellPosition;
                    temp.isAlive = true;

                    cellsToUpdate.Add(temp);

                }
            }
        }
        
        foreach (Cell cell in cellsToUpdate) {
            int x = Mathf.RoundToInt(cell.cellPosition.x);
            int y = Mathf.RoundToInt(cell.cellPosition.y);

            bool alive = cell.isAlive;
            
            cells[x, y].GetComponent<CellBehaviour>().SetAlive(alive);
        }
    }

}
