using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GroundDistance : MonoBehaviour {

    public Text text;
    public Text last;
    public static int lastScore = 0;
	// Use this for initialization
	void Start () {

        
        	
	}
    private void Update()
    {
        text.text =  GameManager.playerDistance.ToString();
        last.text = "Last Score : " + lastScore.ToString();
    }
}
