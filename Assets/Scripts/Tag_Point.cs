using UnityEngine;
//using TMPro;

public class Tag_Point : MonoBehaviour
{
    public Vector3 pointWorldCoord;

    // Update is called once per frame
    void Update()
    {
        Vector2 pointAtScreen = Camera.main.WorldToScreenPoint(pointWorldCoord);
        transform.position = pointAtScreen;
    }
}