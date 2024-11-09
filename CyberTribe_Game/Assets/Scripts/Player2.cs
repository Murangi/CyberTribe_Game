using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public GameObject Ball2;
    // Start is called before the first frame update

    public void Awake()
    {
        Ball2 = GameObject.FindGameObjectWithTag("Marble_2");
    }
}
