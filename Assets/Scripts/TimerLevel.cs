using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerLevel : MonoBehaviour {

    private float timer;
    private bool timerOn;

	// Use this for initialization
	void Start () {
        timer = 100f;
        int min = (int)(timer / 60);
        int sec = (int)(timer % 60);
        GameObject.Find("UI/TextTimer").GetComponent<Text>().text = min + ":" + sec;
        timerOn = false;
    }
	
	// Update is called once per frame
	void Update () {

        if(timerOn)
        {
            timer -= Time.deltaTime;
            int min = (int)(timer / 60);
            int sec = (int)timer % 60;
            GameObject.Find("UI/TextTimer").GetComponent<Text>().text = min + ":" + sec;
            if (timer <= 0f)
            {
                timerEnded();
            }
        }
	}

    void timerEnded()
    {
        //afficher menu
        print("Time out !");
    }

    public void setTimerOn(bool v)
    {
        timerOn = v;
    }
}
