using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallObject : MonoBehaviour {

    private AudioSource son;
    private SphereCollider trashCollider;
    private Vector3 origin;

	// Use this for initialization
	void Start () {
        son = GetComponent<AudioSource>();
        trashCollider = GameObject.Find("Trash").GetComponent<SphereCollider>();
        origin = transform.position;
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
        }
    }
}
