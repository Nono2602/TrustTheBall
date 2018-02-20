using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerGrabObject : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj; //manettes
    // 1
    private GameObject collidingObject; //objet qui touche le collider de la manette
    // 2
    private GameObject objectInHand; //objet qu'on "porte"

    private AudioSource[] sons;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        sons = GetComponents<AudioSource>();
    }

    /**************Savoir si il y avait collision************************/
    private void SetCollidingObject(Collider col)
    {
        // 1
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        // 2
        collidingObject = col.gameObject;
    }

    // 1
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
        SteamVR_Controller.Input((int)trackedObj.index).TriggerHapticPulse(3999);
    }

    // 2
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // 3
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }
    /**********************************************************************/

    private void GrabObject()
    {
        // 1
        objectInHand = collidingObject;
        collidingObject = null;
        // 2
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

        //sound
        if (objectInHand.gameObject.CompareTag("Ball"))
        {
            sons[1].Play();
        }
    }

    // 3
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        // 1
        if (GetComponent<FixedJoint>())
        {
            // 2
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        // 4
        objectInHand = null;

        //sound
        if (collidingObject.gameObject.CompareTag("Ball"))
        {
            sons[0].Play();
        }
    }

    // Update is called once per frame
    void Update () {
        // 1
        if (Controller.GetHairTriggerDown())
        {
            if(collidingObject)
            {
                if(collidingObject.gameObject.CompareTag("Play"))
                {
                    GameObject.Find("Level").GetComponent<TimerLevel>().setTimerOn(true);
                    Destroy(collidingObject);
                    collidingObject = null;
                    GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().isKinematic = false;
                    GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().useGravity = true;
                }
                else if(collidingObject.gameObject.CompareTag("lobby"))
                {
                    sons[2].Play();
                    SceneManager.LoadScene("Lobby");
                } else if(collidingObject.gameObject.CompareTag("playAgain"))
                {
                    sons[2].Play();
                    SceneManager.LoadScene("Game");
                }
                else
                {
                    GrabObject();
                }
            } 
        }

        // 2
        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }
}
