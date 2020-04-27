using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public bool CanBuy()
    {
        return true;
    }

    // Buy object returns cost
    public float Buy()
    {
        return 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
