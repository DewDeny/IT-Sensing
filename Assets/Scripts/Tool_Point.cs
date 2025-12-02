using System;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Tool_Point : MonoBehaviour
{
    //Base functions
    public GameObject pointPref, coordPref, distancePref, anglePref,linePref,lineGreenPref;
    public GameObject pointsGroup, tagsGroup;
    GameObject pointBeingDragged, tagBeingDragged, lineBeingDragged;
  public  GameObject[] temp_PointStorage,temp_TagStorage;
    int tool;
    Vector3 hitPos;
    bool startPointing;
    int pointAmount;

    // Start is called blablabla
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit, Mathf.Infinity) && hit.transform.gameObject.layer != 5 && startPointing) //Dragging the point marker
        {
            hitPos = hit.point;
            pointBeingDragged.GetComponent<Tag_Point>().pointWorldCoord = hitPos;

            switch (tool)
            {
                case 4:
                    if (pointAmount == 1)
                    {

                    }
                    break;
            }
        }

        if (Input.GetMouseButtonDown(0) && startPointing) //Placing down the point marker
        {
            switch (tool)
            {
                case 1:
                    if (pointAmount == 1)
                    {
                        for(int i = 0; i < 3; i++)
                        {
                            temp_TagStorage[i].GetComponent<Tag_Angle>().placed = true;
                        }
                    }
                    break;

                case 2:
                    tagBeingDragged.GetComponent<Tag_Coord>().placed = true;
                    break;

                case 3:
                    if (tagBeingDragged != null)
                        tagBeingDragged.GetComponent<Tag_Distance>().placed = true;
                    break;
            }

            pointAmount -= 1;
            if (pointAmount == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    temp_PointStorage[i]=null;
                    temp_TagStorage[i]=null;
                }

                pointBeingDragged = null;
                lineBeingDragged = null;
                tagBeingDragged = null;

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
        Vector2 hidePos = hitPos;

        switch (tool)
        {
            case 1://Angle Measurement

                if (pointAmount < 3)
                {
                    lineBeingDragged = Instantiate(linePref, hidePos, Quaternion.identity, pointsGroup.transform);
                    lineBeingDragged.GetComponent<Tag_Line>().pointsPlaced[0] = pointBeingDragged;
                }

                pointBeingDragged = Instantiate(pointPref, hidePos, Quaternion.identity, pointsGroup.transform);
                int temp_SlotNumberAngle = 3 - pointAmount;
                temp_PointStorage[temp_SlotNumberAngle] = pointBeingDragged;

                if (pointAmount < 3)
                    lineBeingDragged.GetComponent<Tag_Line>().pointsPlaced[1] = pointBeingDragged;

                if (pointAmount == 1)
                {
                    lineBeingDragged = Instantiate(linePref, hidePos, Quaternion.identity, pointsGroup.transform);
                    lineBeingDragged.GetComponent<Tag_Line>().pointsPlaced[0] = pointBeingDragged;
                    lineBeingDragged.GetComponent<Tag_Line>().pointsPlaced[1] = temp_PointStorage[0];

                    for (int i = 0; i < 3; i++)
                    {
                        tagBeingDragged = Instantiate(anglePref, hidePos, Quaternion.identity, tagsGroup.transform);
                        temp_TagStorage[i] = tagBeingDragged;

                        tagBeingDragged.GetComponent<Tag_Angle>().pointsPlaced[0] = temp_PointStorage[Mathf.RoundToInt(Mathf.Repeat(i, 3))];
                        tagBeingDragged.GetComponent<Tag_Angle>().pointsPlaced[1] = temp_PointStorage[Mathf.RoundToInt(Mathf.Repeat(i + 1, 3))];
                        tagBeingDragged.GetComponent<Tag_Angle>().pointsPlaced[2] = temp_PointStorage[Mathf.RoundToInt(Mathf.Repeat(i + 2, 3))];
                    }
                }
                break;

            case 2://Point measurement
                pointBeingDragged = Instantiate(pointPref, hidePos, Quaternion.identity, pointsGroup.transform);
                tagBeingDragged = Instantiate(coordPref, hidePos, Quaternion.identity, tagsGroup.transform);
                tagBeingDragged.GetComponent<Tag_Coord>().pointPlaced = pointBeingDragged;
                break;

            case 3://Distance measurement
                if (pointAmount == 1)
                {
                    lineBeingDragged = Instantiate(linePref, hidePos, Quaternion.identity, pointsGroup.transform);
                    lineBeingDragged.GetComponent<Tag_Line>().pointsPlaced[0] = pointBeingDragged;
                    tagBeingDragged = Instantiate(distancePref, hidePos, Quaternion.identity, tagsGroup.transform);
                    tagBeingDragged.GetComponent<Tag_Distance>().pointsPlaced[0] = pointBeingDragged;
                }

                pointBeingDragged = Instantiate(pointPref, hidePos, Quaternion.identity, pointsGroup.transform);

                if (pointAmount == 1)
                {
                    lineBeingDragged.GetComponent<Tag_Line>().pointsPlaced[1] = pointBeingDragged;
                    tagBeingDragged.GetComponent<Tag_Distance>().pointsPlaced[1] = pointBeingDragged;
                    pointAmount += 1;
                }
                break;

            case 4:
                if (pointAmount == 1)
                {
                    lineBeingDragged = Instantiate(linePref, hidePos, Quaternion.identity, pointsGroup.transform);
                    lineBeingDragged.GetComponent<Tag_Line>().pointsPlaced[0] = pointBeingDragged;
                }

                pointBeingDragged = Instantiate(pointPref, hidePos, Quaternion.identity, pointsGroup.transform);
                int temp_SlotNumberHeight = 2 - pointAmount;
                temp_PointStorage[temp_SlotNumberHeight] = pointBeingDragged;

                if (pointAmount == 1)
                {
                    lineBeingDragged.GetComponent<Tag_Line>().pointsPlaced[1] = pointBeingDragged;

                    lineBeingDragged = Instantiate(lineGreenPref, hidePos, Quaternion.identity, pointsGroup.transform); //green line
                    lineBeingDragged.GetComponent<Tag_LineGreen>().SourceTargetDirection(temp_PointStorage[0], temp_PointStorage[1], 2);
                // problem happened here
                //use direct reference instead of calling method
                    Debug.Log("Green line moved!");

                    lineBeingDragged = Instantiate(lineGreenPref, hidePos, Quaternion.identity, pointsGroup.transform); //green line
                    lineBeingDragged.GetComponent<Tag_LineGreen>().SourceTargetDirection(temp_PointStorage[1], temp_PointStorage[0], 1);

                    tagBeingDragged = Instantiate(distancePref, hidePos, Quaternion.identity, tagsGroup.transform);
                    tagBeingDragged.GetComponent<Tag_Distance>().pointsPlaced[0] = pointBeingDragged;
                    tagBeingDragged.GetComponent<Tag_Distance>().pointsPlaced[1] = pointBeingDragged;
                }
                break;
        }
        startPointing = true;
    }

    // ACCESSED BY BUTTONS

    public void Measure_Angle()
    {
        pointAmount = 3;
        StartPointing(1);
    }

    public void Measure_Point()
    {
        pointAmount = 1;
        StartPointing(2);
    }

    public void Measure_Distance()
    {
        pointAmount = 2;
        StartPointing(3);
    }

    public void Measure_Height()
    {
        pointAmount = 2;
        StartPointing(4);
    }

    public void Measure_Remove_All()
    {
        foreach (Transform offsprings in pointsGroup.transform)
        {
            Destroy(offsprings.gameObject);
        }

        foreach (Transform offsprings in tagsGroup.transform)
        {
            Destroy(offsprings.gameObject);
        }
    }
}