using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpawnerMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public GameObject playerObject;

	// Update is called once per frame
	void Update () {
        playerObject = GameObject.Find("PlayerObject");
        if (playerObject.GetComponent<Player>().getOrientation() == "Down")
        {
            //bas
            Rigidbody2D rig = GetComponentInParent<Rigidbody2D>();
            Vector3 pos = new Vector3(rig.position.x, rig.position.y - 1.5f, 0);
            transform.position = pos;
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (playerObject.GetComponent<Player>().getOrientation() == "Up")
        {
            //haut
            Rigidbody2D rig = GetComponentInParent<Rigidbody2D>();
            Vector3 pos = new Vector3(rig.position.x, rig.position.y + 1.5f, 0);
            transform.position = pos;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (playerObject.GetComponent<Player>().getOrientation() == "Right")
        {
            //droite
            Rigidbody2D rig = GetComponentInParent<Rigidbody2D>();
            Vector3 pos = new Vector3(rig.position.x + 1.5f, rig.position.y, 0);
            transform.position = pos;
            transform.rotation = Quaternion.Euler(0f, 0f, 270f);
        }
        else
        {
            //gauche
            Rigidbody2D rig = GetComponentInParent<Rigidbody2D>();
            Vector3 pos = new Vector3(rig.position.x - 1.5f, rig.position.y, 0);
            transform.position = pos;
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }
}
