using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text myScore;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        myScore = GameObject.Find("Score").GetComponent<Text>();

        myScore.text = HoldPlayerStats.Score.ToString();
    }

}
