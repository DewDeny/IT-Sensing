using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PH_circleDraw : MonoBehaviour
{
    public GameObject[] dots;
    public GameObject sample;

    public Vector2[] dotsPos;
    public Vector2 circleCenter;

    RectTransform rectTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = sample.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            dotsPos[i] = dots[i].transform.position;
        }

        Vector2 v1 = dotsPos[1] - dotsPos[0];
        Vector2 v2 = dotsPos[2] - dotsPos[0];
        float v1v1 = Vector2.Dot(v1, v1);
        float v2v2 = Vector2.Dot(v2, v2);
        float v1v2 = Vector2.Dot(v1, v2);

        float b = 0.5f / (v1v1 * v2v2 - v1v2 * v1v2);
        float k1 = b * v2v2 * (v1v1 - v1v2);
        float k2 = b * v1v1 * (v2v2 - v1v2);
        circleCenter = dotsPos[0] + v1 * k1 + v2 * k2;

        float radius = Vector2.Distance(circleCenter, dotsPos[0]);

        sample.transform.position = circleCenter;

        rectTransform.sizeDelta = new Vector2(radius*2, radius*2);
    }
}