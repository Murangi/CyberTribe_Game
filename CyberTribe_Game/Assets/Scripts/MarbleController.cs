using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isMouseDown();
    }

    bool isMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log($"Marble clicked: {gameObject.name}");
            return true;
        }

        return false;
    }
}
