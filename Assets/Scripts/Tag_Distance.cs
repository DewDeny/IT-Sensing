using TMPro;
using UnityEngine;

public class Tag_Distance : MonoBehaviour
{
    public GameObject[] pointsPlaced;
    public bool placed = false;
    TextMeshProUGUI txt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        txt = transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>();
        // pointsPlaced = new GameObject[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (pointsPlaced[1] != null)
        {
            Vector2 tagPos = (pointsPlaced[1].transform.position + pointsPlaced[0].transform.position) / 2;
            transform.position = tagPos;

            if (!placed)
            {
                float theDistance = Vector2.Distance(pointsPlaced[1].GetComponent<Tag_Point>().pointWorldCoord, pointsPlaced[0].GetComponent<Tag_Point>().pointWorldCoord);
                txt.text = theDistance.ToString("f2");
            }
        }
    }
}