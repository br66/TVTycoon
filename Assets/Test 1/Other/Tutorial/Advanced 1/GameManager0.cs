using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager0 : MonoBehaviour
{
    Beginning.CSharp.Player playerOne;
    Alien alienOne;

    private void Start()
    {
        playerOne = new Beginning.CSharp.Player();
        playerOne.Name = "Barney";
        playerOne.Score = 100;
        playerOne.Lives = 3;

        alienOne = new Alien();
        alienOne.IsAlive = true;
        alienOne.HitPoints = 1;
        alienOne.PointValue = 100;
    }

    private void OnDisable()
    {
        Debug.Log("Name: " + playerOne.Name + ", Score: " + playerOne.Score + ", Lives: " + playerOne.Lives);
    }
}
