using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyObject : MonoBehaviour {
    public float speed;
    public GameObject playerObject;
    public bool isTouched = false;
    public int PushLight;
    public int PushHeavy;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        playerObject = GameObject.Find("PlayerObject");
        transform.position = Vector3.MoveTowards(transform.position, playerObject.transform.position, speed * Time.deltaTime);
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HitTrigger")
        {
            gameObject.layer = LayerMask.NameToLayer("Ennemy");
        }
        else if (isTouched && collision.gameObject.tag == "DeathTrigger")
        {
            Destroy(gameObject);
            Destroy(this);
            playerObject.GetComponent<Player>().Setscore(100);
        }
        else if (collision.gameObject.tag == "AttackLight")
        {
            isTouched = true;
            StartCoroutine(Push(PushLight));
        }
        else if (collision.gameObject.tag == "AttackHeavy")
        {
            isTouched = true;
            StartCoroutine(Push(PushHeavy));
        }

    }

    IEnumerator Push(int push)
    {
        Rigidbody2D rig = GetComponent<Rigidbody2D>();
        playerObject = GameObject.Find("PlayerObject");
        Vector3 dest = new Vector3();
        if (playerObject.GetComponent<Player>().getOrientation() == "Up")
        {
            //haut
            dest = new Vector3(transform.position.x, transform.position.y + push, transform.position.z);
        }
        else if (playerObject.GetComponent<Player>().getOrientation() == "Right")
        {
            //droite
            dest = new Vector3(transform.position.x + push, transform.position.y, transform.position.z);
        }
        else if (playerObject.GetComponent<Player>().getOrientation() == "Left")
        {
            //gauche
            dest = new Vector3(transform.position.x - push, transform.position.y, transform.position.z);
        }
        else
        {
            //bas
            dest = new Vector3(transform.position.x, transform.position.y - push, transform.position.z);
        }

        for (var i = 0; i != 10; i++)
        {
            transform.position = Vector3.Lerp(rig.position, dest, 0.1f);
            yield return null;
        }
        isTouched = false;
    }
}
