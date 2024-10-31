using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarblePlayer1 : MonoBehaviour
{

    private GameObject MousePointA;
    private GameObject MousePointB;
    private GameObject Arrow;
    private GameObject Circle;

    //Current Distance Variables
    private float CurrentDistance;
    public  float MaxDistance = 3f;
    private float SafeSpace;
    private float ShootPower;

    private Vector3 ShootDirection;
    
    private void Awake()
    {
        MousePointA = GameObject.FindGameObjectWithTag("PointA");
        MousePointB = GameObject.FindGameObjectWithTag("PointB");
    }

    private void OnMouseDrag()
    {
        CurrentDistance = Vector3.Distance(MousePointA.transform.position, transform.position);

        if(CurrentDistance <= MaxDistance)
        {
            SafeSpace = CurrentDistance;
        }
        else
        {
            SafeSpace = MaxDistance;
        }

        //Calculate shot power and direction
        ShootPower = Mathf.Abs(SafeSpace) * 100;
        Vector3 DimensionsXY = MousePointA.transform.position - transform.position;
        float Difference = DimensionsXY.magnitude;

        MousePointB.transform.position = transform.position + ((DimensionsXY / Difference) * CurrentDistance * -1);

        //This is a restraint so that it doesnt move outside the boundaries.
        MousePointB.transform.position = new Vector3( MousePointB.transform.position.x, MousePointB.transform.position.y, -0.8f);

        ShootDirection = Vector3.Normalize(MousePointA.transform.position - transform.position);
    }

    private void OnMouseUp()
    {
        Vector3 Push = ShootDirection * ShootPower * -1;
        GetComponent<Rigidbody>().AddForce(Push, ForceMode.Impulse);
    }
}

