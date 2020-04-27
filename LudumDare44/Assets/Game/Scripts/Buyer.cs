using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyer : MonoBehaviour
{
    public float Reach = 5f;

    public bool canBuy = false;

    private Shop _shop;
    private Transform target;

    private BoxCollider hitBox;

    private bool showText = false;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.Find("Look At Target");

        hitBox = gameObject.AddComponent<BoxCollider>();
        hitBox.size = new Vector3(1f, 1f, 3f);
        hitBox.center = new Vector3(0.1f, 0f, 2.5f);
        hitBox.isTrigger = true;
    }

    public bool Purchase(out float cost)
    {
        if (_shop)
        {
            cost = _shop.Buy();
            return true;
        }
        cost = 0f;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shop"))
        {
            _shop = other.GetComponent<Shop>();
            canBuy = true;
            showText = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shop"))
        {
            _shop = null;
            canBuy = false;
            showText = false;
        }
    }

    void OnGUI()
    {
        if (showText)
        {
            GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            centeredStyle.fontSize = 50;
            GUI.Label(
                new Rect(0, 0, Screen.width, Screen.height),
                "Press F to buy");
        }
    }
}
