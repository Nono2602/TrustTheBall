using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerLevel : MonoBehaviour {

    [SerializeField] float timer = 10f;
    private bool timerOn;
    private Vector3 originLobbyButton;
    private Vector3 originPlayAgainButton;
    private AudioSource soundEndLevel;

    // Use this for initialization
    private void Awake()
    {
        originLobbyButton = GameObject.FindGameObjectWithTag("lobby").transform.position;
        originPlayAgainButton = GameObject.FindGameObjectWithTag("playAgain").transform.position;
        GameObject.FindGameObjectWithTag("lobby").transform.position = new Vector3(-100,-100,-100);
        GameObject.FindGameObjectWithTag("playAgain").transform.position = new Vector3(-100, -100, -100);
    }

    void Start () {
        int min = (int)(timer / 60);
        int sec = (int)(timer % 60);
        GameObject.Find("UI/TextTimer").GetComponent<Text>().text = min + ":" + sec;
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
                    GameObject.Find("UI/TextTimer").GetComponent<Text>().text = min + ":0" + sec;
                } else
                {
                    GameObject.Find("UI/TextTimer").GetComponent<Text>().text = min + ":" + sec;
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
        soundEndLevel.Play();
        print("Time out !");
        Destroy(GameObject.FindGameObjectWithTag("Ball"));
        GameObject.FindGameObjectWithTag("lobby").transform.position = originLobbyButton;
        GameObject.FindGameObjectWithTag("playAgain").transform.position = originPlayAgainButton;
    }

    public void setTimerOn(bool v)
    {
        timerOn = v;
    }
}
