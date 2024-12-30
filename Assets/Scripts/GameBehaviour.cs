using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour{

    [Header("Pause and Play Settings")]
    public bool isPaused;
    public float secondsPerGeneration;
    private float curSecondsPerGeneration;

    RaycastHit2D lastHit = new RaycastHit2D();

    void Start(){
        isPaused = true;
    }

    void Update(){
        if (Input.mousePresent && Input.GetMouseButton(0) && isPaused)
            GetClickObject();

        if (Input.touchCount > 0 && isPaused)
            GetTouchObject();

        if (Input.GetKeyDown(KeyCode.Space))
            isPaused = !isPaused;

        if (Input.GetKeyDown(KeyCode.R))
            this.GetComponent<GridController>().ResetAllCells();

        if (Input.GetKeyDown(KeyCode.G))
            this.GetComponent<GridController>().UpdateGrid();

        if (!isPaused) {
            curSecondsPerGeneration += Time.deltaTime;

            if (curSecondsPerGeneration >= secondsPerGeneration) {
                curSecondsPerGeneration = 0;
                this.GetComponent<GridController>().UpdateGrid();
            }
        } else {
            curSecondsPerGeneration = 0;
        }
    }

    void GetClickObject() {

        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

        if (hit.collider == lastHit.collider)
            return;

        if (hit.collider == null)
            return;

        if (hit.collider.CompareTag("Grid")) {
            //Debug.Log($"Hit - Grid X: {hit.transform.position.x} Y: {hit.transform.position.y}");
            hit.collider.GetComponent<CellBehaviour>().OnClickObject();
            lastHit = hit;
        }
    }

    void GetTouchObject() {
        
        Touch touch = Input.GetTouch(0);
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
        if (hit.collider == null)
            return;
        if (hit.collider.CompareTag("Grid")) {
            //Debug.Log($"Hit - Grid X: {hit.transform.position.x} Y: {hit.transform.position.y}");
            hit.collider.GetComponent<CellBehaviour>().OnClickObject();
        }
    }
}
