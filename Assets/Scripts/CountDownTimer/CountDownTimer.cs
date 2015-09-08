using UnityEngine;
using System.Collections;

using UnityEngine.UI;


//----------------------------------------------------------------//
//      Count Down Timer class  カウントダウンタイマークラス         //
//----------------------------------------------------------------//

public class CountDownTimer : MonoBehaviour {


    //-------------------------------------------//
    //         Time Limit  制限時間               //
    //-------------------------------------------//
    [SerializeField]
    private int TimeLimit;

    //-------------------------------------------//
    //  calculated for variable 計算用変数        //
    //-------------------------------------------//
    private float TimerCalculated;

	//-------------------------------------------//
	//	Animator								 //
	//-------------------------------------------//
	private Animator CountDowmAnimator;
 
    //-------------------------------------------//
    //           Active Flag 有効フラグ           //
    //-------------------------------------------//
    private bool isActive;

	// Use this for initialization
	void Start () 
    {
		TimeLimit = 15;
        //Get Text Component
        //コンポーネントを取得
		CountDowmAnimator = GameObject.Find("CountDown").GetComponent<Animator>();

		//Rebind Animator
		CountDowmAnimator.Rebind ();

        //Initialized variable
        //計算用変数を初期化
        TimerCalculated = TimeLimit;

       
        //Initialize flag
        isActive = true;

	}
	
	// Update is called once per frame
	void Update () 
    {

        if (isActive) {
			//before frame from the elapsed time subtracting
			//前フレームからの経過時間を減算
			TimerCalculated -= Time.deltaTime;


			//To apply a correction After less than equal to zero
			//0以下になったら補正をかける
			if (TimerCalculated <= 0.0f) {
				TimerCalculated = 0.0f;

			}
		} else {
				CountDownAnimationEnd();
		}

    

	}

	//Ending Count down Animation
	void CountDownAnimationEnd()
	{
		CountDowmAnimator.speed = 0;
	}

    

    // Get Current Time limit(float)
    public float GetCurrentTimeFloat()
    {
        return TimerCalculated;
    }

	//get time limit 
    public int GetTimeLimit()
    {
        return TimeLimit;
    }

    //Set active flag
    public void SetFlag(bool flag)
    {
        isActive = flag;
    }

}
