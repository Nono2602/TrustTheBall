using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerLevel : MonoBehaviour {

    [SerializeField] float timer = 10f;
    [SerializeField] TextMesh timerText;
    private bool timerOn;
    private Vector3 originLobbyButton;
    private Vector3 originPlayAgainButton;
    private AudioSource soundEndLevel;
    private bool end;

    // Use this for initialization
    private void Awake()
    {
        originLobbyButton = GameObject.FindGameObjectWithTag("lobby").transform.position;
        originPlayAgainButton = GameObject.FindGameObjectWithTag("playAgain").transform.position;
        GameObject.FindGameObjectWithTag("lobby").transform.position = new Vector3(-100,-100,-100);
        GameObject.FindGameObjectWithTag("playAgain").transform.position = new Vector3(-100, -100, -100);
    }

    void Start () {
        end = false;
        int min = (int)(timer / 60);
        int sec = (int)(timer % 60);
        timerText.text = min + ":" + sec;
        if (timer > 0f)
        {
            if (sec < 10)
            {
                timerText.text = min + ":0" + sec;
            }
            else
            {
                timerText.text = min + ":" + sec;
            }
        }
        timerOn = false;
        soundEndLevel = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        if(timerOn)
        {
            timer -= Time.deltaTime;
            int min = (int)(timer / 60);
            int sec = (int)timer % 60;
            if(timer > 0f)
            {
                if(sec < 10)
                {
                    timerText.text = min + ":0" + sec;
                } else
                {
                    timerText.text = min + ":" + sec;
                }
            }
            else
            {
                timerEnded();
            }
        }
	}

    void timerEnded()
    {
        if(!end)
        {
            soundEndLevel.Play();
            print("Time out !");
            Destroy(GameObject.FindGameObjectWithTag("Ball"));
            GameObject.FindGameObjectWithTag("lobby").transform.position = originLobbyButton;
            GameObject.FindGameObjectWithTag("playAgain").transform.position = originPlayAgainButton;
            end = true;
        }        
    }

    public void setTimerOn(bool v)
    {
        timerOn = v;
    }
}
