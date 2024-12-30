using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour{

    public Vector2 cellPosition;
    public Color aliveColor;
    public Color deadColor;

    public bool isAlive;

    void Start(){
        cellPosition = transform.position;
        isAlive = false;
        UpdateColor();
    }

    public void OnClickObject() {
        UpdateAlive();
    }
    
    void UpdateAlive() {
        isAlive = !isAlive;
        UpdateColor();
    }

    void UpdateColor() {
        GetComponent<SpriteRenderer>().color = isAlive ? aliveColor : deadColor;
    }

    public void SetAlive(bool status) {
        isAlive = status;
        UpdateColor();
    }

}

[SerializeField]
public class Cell {
    public bool isAlive;
    public Vector2 cellPosition;
}