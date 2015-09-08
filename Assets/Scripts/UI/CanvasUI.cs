﻿using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class CanvasUI : MonoBehaviour {

    //UI Objects  UIオブジェクト
    private GameObject UIObject_CountDown;          //CountDown         カウントダウンタイマー
    private GameObject UIObject_Panel;              //Panel             パネル
    private GameObject UIObject_ClearTime;          //ClearTime         クリア時間
    private GameObject UIObject_Score;              //Score             スコア
	private GameObject UIObject_Image;				//Image

	public GameplayScript gamePlayScript;
	public GobballSpawnerScript gobballSpawnerScript;

	private int NumOfGoball;
	private bool isGameEnd;

	// Use this for initialization
	void Start () 
    {
        //Find Game Object
        UIObject_CountDown      = GameObject.Find("CountDown");
        UIObject_Panel          = GameObject.Find("Panel");
        UIObject_ClearTime      = GameObject.Find("ClearTime");
        UIObject_Score          = GameObject.Find("Score");
		UIObject_Image			= GameObject.Find ("ResultImage");

		//get num of gobball
		NumOfGoball = 0;
 
		NumOfGoball = gobballSpawnerScript.numOfGobball;

		isGameEnd = false;

        //Unnecessary Objects Disabling
        UIObject_Panel.SetActive(false);
        UIObject_ClearTime.SetActive(false);
        UIObject_Score.SetActive(false);
		UIObject_Image.SetActive (false);

		//Debug.Log ("Num of Gobball : "+NumOfGoball);

    }
	
	// Update is called once per frame
	void Update () 
    {
		//get current time
        float CurrentTime = UIObject_CountDown.GetComponent<CountDownTimer>().GetCurrentTimeFloat();
		int score = gamePlayScript.Count;
		Debug.Log (gamePlayScript.GetTotalGobball ());
		//if Once of the game has not been finished 
		if (CurrentTime == 0.0f || score == gamePlayScript.GetTotalGobball())
        {

			AudioSource audioSourceEnd = GetComponent<AudioSource>();
			if(!isGameEnd){
				audioSourceEnd.PlayOneShot(audioSourceEnd.clip);
				isGameEnd = true;
			}
			//stop at count down timer
			UIObject_CountDown.GetComponent<CountDownTimer>().SetFlag(false);

            UIObject_Panel.SetActive(true);
            UIObject_ClearTime.SetActive(true);
            UIObject_Score.SetActive(true);
			UIObject_Image.SetActive(true);

            //Calculates the time and score
            float ClearTime = (UIObject_CountDown.GetComponent<CountDownTimer>().GetTimeLimit()-1) - UIObject_CountDown.GetComponent<CountDownTimer>().GetCurrentTimeFloat();

            UIObject_ClearTime.GetComponent<Text>().text = ClearTime.ToString("N2") + "sec";
			UIObject_Score.GetComponent<Text>().text = score.ToString();


        }
    
    }

	void UpdateNumOfGobball (int newNum) {
		NumOfGoball = newNum;
		Debug.Log (NumOfGoball);
	}
}
