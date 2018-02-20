using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj; //manettes
    // 1
    private GameObject collidingObject; //objet qui touche le collider de la manette


    private AudioSource son;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        son = GetComponent<AudioSource>();
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



    // Update is called once per frame
    void Update()
    {
        // 1
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                if (collidingObject.gameObject.CompareTag("Play"))
                {
                    son.Play();
                    SceneManager.LoadScene("Game");
                }
            }
        }
    }
}
