//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MapTriggerManager : MonoBehaviour {

//    public GameObject[] maps;
//    private void Start()

//    {

//        for (int i = 1; i < maps.Length; i++)
//        {
//            maps[i].SetActive(false);

//        }



//    }
//    private void OnTriggerEnter(Collider other)
//    {
//        MapManager.afterMap = Random.Range(0, 3);
//        if (MapManager.currentMap == MapManager.afterMap)
//            return;
//        Debug.Log(MapManager.afterMap);
//        maps[MapManager.afterMap].SetActive(true);
//        maps[MapManager.afterMap].transform.position = new Vector3(MapManager.startPoint.x, MapManager.startPoint.y, MapManager.startPoint.z + 258 * (MapManager.MapCount - 1));
//        MapManager.MapCount++;


//    }
//}
