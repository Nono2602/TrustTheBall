using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallObject : MonoBehaviour {

    const float X_MIN = 0.18f;
    const float X_MAX = 1.4f;
    const float Z_MIN = -1.9f;
    const float Z_MAX = 0f;
    const float Y = 0.031f;

    [SerializeField] TextMesh scoreText;
    [SerializeField] bool moveMode;
    [SerializeField] bool timeMode;
    [SerializeField] bool randomTime;
    [SerializeField] float timeMin;
    [SerializeField] float timeMax;

    private AudioSource[] sons;
    private SphereCollider trashCollider;
    private Vector3 origin;
    private int score;

    private float deltaTime;
    private float timer;
    private bool started;

    // Use this for initialization
    void Start () {

        sons = GetComponents<AudioSource>();
        trashCollider = GameObject.Find("Trash").GetComponent<SphereCollider>();
        origin = transform.position;
        score = 0;
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<Rigidbody>().useGravity = false;

        deltaTime = (randomTime) ? randomFloat(timeMin, timeMax) : 20f;
        timer = 0;

        moveMode   = StartGame.moveMode;
        timeMode   = StartGame.timeMode;
        randomTime = StartGame.randomTime;
        timeMin    = StartGame.timeMin;
        timeMax    = StartGame.timeMax;
    }

    void Update()
    {
        if (timeMode)
        {
            if(origin != transform.position) {
                started = true;
            }
            if (started)
            {
                timer += Time.deltaTime;
                if (timer > deltaTime)
                {
                    setTrashPosition();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("GameController"))
        {
            sons[0].Play();
        }
        if (collision.collider == trashCollider)
        {
            sons[1].Play();
            transform.position = origin;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            score++;
            scoreText.text = "Score : " + score;

            if (moveMode)
            {
                setTrashPosition();
            }
        }
    }

    /**
     * Set the position of the trash
     */
    private void setTrashPosition()
    {
        float x = randomFloat(X_MIN, X_MAX);
        float z = randomFloat(Z_MIN, Z_MAX);
        GameObject.Find("Trash").transform.position = new Vector3(x, Y, z);
        if (randomTime)
        {
            deltaTime = randomFloat(timeMin, timeMax);
        }
        timer = 0;
        sons[2].Play();
    }

    private float randomFloat(float min, float max)
    {
        System.Random random = new System.Random(System.DateTime.Now.TimeOfDay.Milliseconds);
        var result = (random.NextDouble()* (max - (double)min))+ min;
        return (float)result;
    }


}
