using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int tileNumber = 0; // 깔 타일 번호
    public static int playerTileNumber = -1; // 플레이어가 서있는 타일 번호
    public static int playerDistance = 1;
    public Material sky;
    float tileX;
    public Vector3 originTilePosition;
    public float startPointZ;
    public GameObject tileObject;
    public GameObject playerObject;
    GameObject[] tiles;
    private void Start()
    {
        originTilePosition = tileObject.transform.position;
        tiles = new GameObject[10];
        // 타일 초기화
        for (int i = 0; i < 10; i++)
        {
            GameObject newTile = Instantiate(tileObject);
            newTile.name = "Ground " + (i + 1);
            tiles[i] = newTile;
            tiles[i].SetActive(false);
        }

    }

    private void Update()
    {
        sky.


        playerDistance = (int)(playerObject.transform.position.z - startPointZ)+15;
        if ((playerTileNumber + 4) % 11 != tileNumber % 11)
        //if((tileNumber-2)%10 <= playerTileNumber%10)
        {

            tileX = Random.Range(-4.41f, 4.89f);
            tiles[tileNumber % 10].SetActive(true);
            Vector3 tilePosition = new Vector3(originTilePosition.x, originTilePosition.y, originTilePosition.z + (tileNumber+1) * (3.81f)*2);
            tilePosition.x = tileX;

            tiles[tileNumber % 10].transform.position = tilePosition;
            
            tileNumber++;
        }
    }

}
