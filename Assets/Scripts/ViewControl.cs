using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewControl : MonoBehaviour
{
    Vector2 mouseInitPos, mousePos;
    Vector3 cameraInitPos, cameraInitRot;
  GameObject cameraObj;

    // Start is called before the first frame update
    void Start()
    {
        cameraInitPos = transform.localPosition;
        cameraObj = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation control
        if (Input.GetMouseButtonDown(0))
            mouseInitPos = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
            cameraInitRot = transform.localEulerAngles;

        if (Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;

            Quaternion thisRotation = Quaternion.identity;
            thisRotation.eulerAngles = new Vector3(
            cameraInitRot.x - (mousePos.y - mouseInitPos.y) / 10,
            cameraInitRot.y + (mousePos.x - mouseInitPos.x) / 10,
            cameraInitRot.z);

            transform.localEulerAngles = thisRotation.eulerAngles;
        }

        //Strafing control
        if (Input.GetMouseButtonDown(1))
            mouseInitPos = Input.mousePosition;

        if (Input.GetMouseButtonUp(1))
            cameraInitPos = transform.localPosition;

        if (Input.GetMouseButton(1))
        {
            mousePos = Input.mousePosition;

            transform.position = cameraInitPos
                - (transform.right * (mousePos.x - mouseInitPos.x) / 8)
                - (transform.up * (mousePos.y - mouseInitPos.y) / 8);
        }

        //Zooming control
        cameraObj.transform.position = new Vector3(cameraObj.transform.position.x,
            cameraObj.transform.position.y,
            cameraObj.transform.position.z * Input.mouseScrollDelta.x);

        Debug.Log(Input.mouseScrollDelta);
    }
}