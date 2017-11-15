using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Die : MonoBehaviour {
    public Text text;
    public GameObject playerObject;
    public GameObject scoreUI;
    private void Start()
    {
        LoadBestScore();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Main Camera")
        {
            if (GameManager.playerDistance > int.Parse(text.text.Split(' ')[3]))
            {
                text.text = "Top Score : " + GameManager.playerDistance.ToString();

                SaveBestScore();
            }
            playerObject.transform.position = new Vector3(0f, 2.11f, -12f);
            GroundDistance.lastScore = GameManager.playerDistance;
            GameManager.playerDistance = 0;
            GameManager.tileNumber = 0;
            GameManager.playerTileNumber = 0;
            transform.position = new Vector3(0f, 0f, -171f);
            scoreUI.transform.position = new Vector3(scoreUI.transform.position.x, scoreUI.transform.position.y, 0f);
        }
    }
    void SaveBestScore()
    {
        PlayerPrefs.SetInt("Top Score", GameManager.playerDistance);

    }
    void LoadBestScore()
    {
        text.text = "Top Score : " +  PlayerPrefs.GetInt("Top Score",0).ToString();
    }
}
