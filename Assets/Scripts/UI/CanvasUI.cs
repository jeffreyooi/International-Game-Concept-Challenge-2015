using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class CanvasUI : MonoBehaviour {

    //UI Objects  UIオブジェクト
    private GameObject UIObject_CountDown;          //CountDown         カウントダウンタイマー
    private GameObject UIObject_Panel;              //Panel             パネル
    private GameObject UIObject_ClearTime;          //ClearTime         クリア時間
    private GameObject UIObject_Score;              //Score             スコア
    
	public GameplayScript gamePlayScript;
	public GobballSpawnerScript gobballSpawnerScript;

	private int NumOfGoball;

	// Use this for initialization
	void Start () 
    {
        //Find Game Object
        UIObject_CountDown      = GameObject.Find("CountDown");
        UIObject_Panel          = GameObject.Find("Panel");
        UIObject_ClearTime      = GameObject.Find("ClearTime");
        UIObject_Score          = GameObject.Find("Score");

		//get num of gobball
		NumOfGoball = 0;
 
		NumOfGoball = gobballSpawnerScript.numOfGobball;



        //Unnecessary Objects Disabling
        UIObject_Panel.SetActive(false);
        UIObject_ClearTime.SetActive(false);
        UIObject_Score.SetActive(false);

		Debug.Log ("Num of Gobball : "+NumOfGoball);

    }
	
	// Update is called once per frame
	void Update () 
    {
		//get current time
        float CurrentTime = UIObject_CountDown.GetComponent<CountDownTimer>().GetCurrentTimeFloat();
		int score = gamePlayScript.Count;
        
		//if Once of the game has not been finished 
        if (CurrentTime == 0.0f || score == NumOfGoball)
        {
			//stop at count down timer
			UIObject_CountDown.GetComponent<CountDownTimer>().SetFlag(false);

            UIObject_Panel.SetActive(true);
            UIObject_ClearTime.SetActive(true);
            UIObject_Score.SetActive(true);

            //Calculates the time and score
            float ClearTime = (UIObject_CountDown.GetComponent<CountDownTimer>().GetTimeLimit()-1) - UIObject_CountDown.GetComponent<CountDownTimer>().GetCurrentTimeFloat();

            UIObject_ClearTime.GetComponent<Text>().text = "ClearTime  : " + ClearTime.ToString("N2") + "sec";
			UIObject_Score.GetComponent<Text>().text = "Score  :"+"  "+ score.ToString();

        }
    
    }

	void UpdateNumOfGobball (int newNum) {
		NumOfGoball = newNum;
		Debug.Log (NumOfGoball);
	}
}
