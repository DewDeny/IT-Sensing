using UnityEngine;
using TMPro;

public class CoordTagger : MonoBehaviour
{
    public GameObject pointPlaced;
    Vector3 pointWorldCoor;
    public bool placed = false;
    TextMeshProUGUI txt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        txt = transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //  Vector2 pointAtScreen = Camera.main.WorldToScreenPoint(pointPlaced.transform.position);
        //  transform.position = pointAtScreen+Vector2.up*10;
        transform.position = new Vector2(pointPlaced.transform.position.x, pointPlaced.transform.position.y + 40);
       // Debug.Log(pointPlaced.GetComponent<PointTagger2>().pointWorldCoord);

        if (!placed)
            pointWorldCoor = pointPlaced.GetComponent<PointTagger2>().pointWorldCoord;
        txt.text = new string("xxx." + pointWorldCoor.x.ToString("f2") + " / xxx." + pointWorldCoor.z.ToString("f2") + " / " + pointWorldCoor.y.ToString("f2"));
    }
}