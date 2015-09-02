using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour {


    //-------------------------------------------//
    //         Time Limit  制限時間              //
    //-------------------------------------------//
    [SerializeField]
    private int TimeLimit;

    //-------------------------------------------//
    //  calculated for variable 計算用変数       //
    //-------------------------------------------//
    private float TimerCalculated;


    //-------------------------------------------//
    //           Show Text 表示テキスト          //
    //-------------------------------------------//
    private Text CountDownText;


	// Use this for initialization
	void Start () 
    {
        //Get Text Component
        //コンポーネントを取得
        CountDownText = GetComponent<Text>();

        //Initialized variable
        //計算用変数を初期化
        TimerCalculated = TimeLimit;

        //Set text of the Time Limit
        //テキストにタイムリミットをセット
        CountDownText.text = TimeLimit.ToString();

	}
	
	// Update is called once per frame
	void Update () 
    {
        //before frame from the elapsed time subtracting
        //前フレームからの経過時間を減算
        TimerCalculated -= Time.deltaTime;


        //To apply a correction After less than equal to zero
        //0以下になったら補正をかける
        if(TimerCalculated <= 0.0f)
        {
            TimerCalculated = 0.0f;

        }

        //calculated variable is Conversion to "int"
        //計算用変数をint型に変換
        int TimeNow = Mathf.FloorToInt(TimerCalculated);

        //It is set in the text of the current time
        //現在時間をテキストにセット
        CountDownText.text = TimeNow.ToString();


	}




}
