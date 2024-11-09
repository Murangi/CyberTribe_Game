using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{   
    public GameObject Ball1;
    // Start is called before the first frame update

    public void Awake()
    {
        Ball1 = GameObject.FindGameObjectWithTag("Marble_1");
    }

}
