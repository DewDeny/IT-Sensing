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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        Vector3 _direction = (dotsPos[1] - circleCenter).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);
        Quaternion _toTheSide = Quaternion.LookRotation(Vector3.down);

        Vector3 tilt_dir = (dotsPos[2] - dotsPos[0]).normalized;
        Quaternion tilt_lookRot=Quaternion.LookRotation(tilt_dir);

        // sample.transform.rotation = new Quaternion(_lookRotation.x,tilt_lookRot.y,_toTheSide.z,_lookRotation.w);
        sample.transform.rotation = _lookRotation * _toTheSide * tilt_lookRot;
    }
}