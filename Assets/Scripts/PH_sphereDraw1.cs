using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PH_sphereDraw : MonoBehaviour
{
    public GameObject[] dots;
    public GameObject sample;

    public Vector3[] dotsPos;
    public Vector3 circleCenter;

    //2d circle 3d rotation test
    public GameObject circleScrTag;
    RectTransform circleScrRect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circleScrRect=circleScrTag.GetComponent<RectTransform>();   
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            dotsPos[i] = dots[i].transform.position;
        }

        Vector3 v1 = dotsPos[1] - dotsPos[0];
        Vector3 v2 = dotsPos[2] - dotsPos[0];
        float v1v1 = Vector3.Dot(v1, v1);
        float v2v2 = Vector3.Dot(v2, v2);
        float v1v2 = Vector3.Dot(v1, v2);

        float b = 0.5f / (v1v1 * v2v2 - v1v2 * v1v2);
        float k1 = b * v2v2 * (v1v1 - v1v2);
        float k2 = b * v1v1 * (v2v2 - v1v2);
        circleCenter = dotsPos[0] + v1 * k1 + v2 * k2;

        float radius = Vector3.Distance(circleCenter, dotsPos[0]);

        sample.transform.position = circleCenter;
        sample.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);

        //rotation
        Vector3 mid_direction = (dotsPos[1] - circleCenter).normalized;
        Vector3 first_direction= (dotsPos[0] - circleCenter).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(mid_direction, first_direction); 
        sample.transform.rotation = _lookRotation ;

        //2D circle
        Vector2 circleScrCenter = Camera.main.WorldToScreenPoint(circleCenter);
        Vector3 pseudoPoint = circleCenter + (Vector3.up * radius);
        Vector2 circleScrRim = Camera.main.WorldToScreenPoint(pseudoPoint);
        float circleScrRadius = Vector2.Distance(circleScrCenter, circleScrRim);

        Quaternion circleScrCorrectiveAngle = Quaternion.Euler(90,0,0);

        circleScrTag.transform.position = circleScrCenter;
        circleScrRect.sizeDelta = new Vector2(circleScrRadius*2, circleScrRadius*2);
        circleScrTag.transform.rotation = sample.transform.rotation;
    }
}