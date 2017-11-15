using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.playerTileNumber = int.Parse(transform.parent.gameObject.name.Split(' ')[1]);
    }


}
