using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallObject : MonoBehaviour {

    private AudioSource son;
    private SphereCollider trashCollider;
    private Vector3 origin;
    private int score;

	// Use this for initialization
	void Start () {
        son = GetComponent<AudioSource>();
        trashCollider = GameObject.Find("Trash").GetComponent<SphereCollider>();
        origin = transform.position;
        score = 0;
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<Rigidbody>().useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("GameController"))
        {
            son.Play();
        }
        if (collision.collider == trashCollider)
        {
            transform.position = origin;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            score++;
            GameObject.Find("UI/TextScore").GetComponent<Text>().text = "Score : " + score;
        }
    }
}
