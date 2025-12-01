using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Tool_Point : MonoBehaviour
{
    //Base functions
    public GameObject pointPref, coordPref, distancePref, linePref;
    public Canvas pointCanvas, tagCanvas;
    public GameObject pointBeingDragged, tagBeingDragged, lineBeingDragged;
    int tool;
    Vector3 hitPos;
    public bool startPointing;

    //Tools function
    public int pointAmount;

    // Update is called once per frame
    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit, Mathf.Infinity) && hit.transform.gameObject.layer != 5 && startPointing) //Dragging the point marker
        {
            hitPos = hit.point;
            pointBeingDragged.GetComponent<Tag_Point>().pointWorldCoord = hitPos;
        }

        if (Input.GetMouseButtonDown(0) && startPointing) //Placing down the point marker
        {
            switch (tool)
            {
                case 1:
                    tagBeingDragged.GetComponent<Tag_Coord>().placed = true;
                    break;

                case 2:
                    if (tagBeingDragged != null)
                        tagBeingDragged.GetComponent<Tag_Distance>().placed = true;
                    break;
            }

            pointAmount -= 1;
            if (pointAmount == 0)
            {
                pointBeingDragged = null;
                tagBeingDragged = null;
                lineBeingDragged = null;
                startPointing = false;
            }
            else
                StartPointing(tool);
        }

        if (Input.GetMouseButtonDown(1) | Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(pointBeingDragged);
            Destroy(tagBeingDragged);
            Destroy(lineBeingDragged);
            pointAmount = 0;
            startPointing = false;
        }
    }

    public void StartPointing(int toolNumber) //Spawn a point marker
    {
        tool = toolNumber;
        Vector2 hidePos = Vector2.zero;

        switch (tool)
        {
            case 1://Point measurement
                pointBeingDragged = Instantiate(pointPref, hidePos, Quaternion.identity, pointCanvas.transform);
                tagBeingDragged = Instantiate(coordPref, hidePos, Quaternion.identity, tagCanvas.transform);
                tagBeingDragged.GetComponent<Tag_Coord>().pointPlaced = pointBeingDragged;
                break;

            case 2://Distance measurement
                if (pointAmount == 1)
                {
                    lineBeingDragged = Instantiate(linePref, hidePos, Quaternion.identity, pointCanvas.transform);
                    lineBeingDragged.GetComponent<Tag_Line>().pointsPlaced[0] = pointBeingDragged;
                    tagBeingDragged = Instantiate(distancePref, hidePos, Quaternion.identity, tagCanvas.transform);
                    tagBeingDragged.GetComponent<Tag_Distance>().pointsPlaced[0] = pointBeingDragged;
                }

                pointBeingDragged = Instantiate(pointPref, hidePos, Quaternion.identity, pointCanvas.transform);

                if (pointAmount == 1)
                {
                    lineBeingDragged.GetComponent<Tag_Line>().pointsPlaced[1] = pointBeingDragged;
                    tagBeingDragged.GetComponent<Tag_Distance>().pointsPlaced[1] = pointBeingDragged;
                    pointAmount += 1;
                }
                break;
        }
        startPointing = true;
    }

    public void Measure_Point()
    {
        StartPointing(1);
        pointAmount = 1;
    }

    public void Measure_Distance()
    {
        StartPointing(2);
        pointAmount = 2;
    }
}