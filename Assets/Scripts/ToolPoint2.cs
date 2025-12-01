using UnityEngine;
using UnityEngine.InputSystem.HID;

public class ToolPoint2 : MonoBehaviour
{
    public GameObject pointPref, coordPref;
    public Canvas pointCanvas, coordCanvas;
    GameObject pointBeingDragged, coordBeingDragged;
    Vector3 hitPos;
    public bool startPointing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit, Mathf.Infinity) && hit.transform.gameObject.layer != 5 && startPointing)
        {
            hitPos = hit.point;
            pointBeingDragged.GetComponent<PointTagger2>().pointWorldCoord = hitPos;
            //pointBeingDragged.transform.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonDown(0) && startPointing)
        {
            //   pointBeingDragged.GetComponent<PointTagger2>().placed = true;
            coordBeingDragged.GetComponent<CoordTagger>().placed = true;
            startPointing = false;
        }
    }

    public void StartPointing()
    {
        Vector3 hidePos = Camera.main.transform.position - Vector3.forward * 10;
        pointBeingDragged = Instantiate(pointPref, hidePos, Quaternion.identity, pointCanvas.transform);
        coordBeingDragged = Instantiate(coordPref, hidePos, Quaternion.identity, coordCanvas.transform);
        coordBeingDragged.GetComponent<CoordTagger>().pointPlaced = pointBeingDragged;
        startPointing = true;
    }
}