using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{

    private float Offset = -0.8f;
    private Vector3 TemperorayPosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Offset);
    }

    // Update is called once per frame
    void Update()
    {
        TemperorayPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        transform.position = new Vector3(TemperorayPosition.x, TemperorayPosition.y, Offset);

    }
}
