using UnityEngine;

public class AutoMenuArrange : MonoBehaviour
{
    float sidePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            RectTransform childWidth = transform.GetChild(i).GetComponent<RectTransform>();

            if (i == 0)
                sidePosition = (transform.GetChild(i).GetComponent<RectTransform>().rect.x / 2);
            else
                sidePosition = (transform.GetChild(i).GetComponent<RectTransform>().rect.x / 2) + (transform.GetChild(i - 1).GetComponent<RectTransform>().rect.x / 2) + 1;

            transform.GetChild(i).transform.localPosition = new Vector2(-sidePosition, 0);
        }
    }
}