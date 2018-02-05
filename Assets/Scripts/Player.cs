using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Player : MonoBehaviour {
    public float speed;

    public GameObject shotlight;
    public GameObject shotfast;
    public Transform shotSpawn;
    public float fireRate;
    public float dashRate;
    public int maxUppercuts = 4;
    public GameObject[] uppercutLights;
    public float dashSize;
    public float frameinvi = 3f;

    private float tmp = 0;
    private int score = 300;
    private Animator anim;
    private float nextFire;
    private float uppercutRefresh = 0;
    private int remainingUppercuts;
    private float prevUppercutPressed;
    private float prevLightPressed;

    private string orientation = "";
    private bool invincible = false;

    public void Setscore(int i)
    {
        score += i;
    }

    public int GetScore()
    {
        return score;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        resetUppercutLights();
        remainingUppercuts = maxUppercuts;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invincible)
        {
            float tmp;
            if (collision.gameObject.tag == "Ennemy")
        {
                invincible = true;
                score -= 100;
            }
        }
    }

    public void resetUppercutLights()
    {
        foreach (var uppercutLight in uppercutLights)
            uppercutLight.SetActive(true);
    }

    private void updateUppercutsLights()
    {
        uppercutRefresh += Time.deltaTime;

        if (uppercutRefresh >= 4)
        {
            uppercutRefresh = 0;
            int i;
            for (i = maxUppercuts - 1; i >= 0 && uppercutLights[i].active; --i) ;
            if (i >= 0)
            {
                uppercutLights[i].SetActive(true);
                remainingUppercuts++;
            }
        }
    }

    private void shutUppercutLight()
    {
        int i;

        for (i = 0; i < maxUppercuts && !uppercutLights[i].active; ++i) ;
        if (i < maxUppercuts)
        {
            uppercutLights[i].SetActive(false);
            remainingUppercuts--;
        }
    }

    void Update()
    {
        if (score <= 0)
            SceneManager.LoadScene("LoseScene");
        anim.SetBool("LightHit", false);
        anim.SetBool("Uppercut", false);
        updateUppercutsLights();
        if (prevLightPressed < 0.85 && Input.GetAxis("Firelight") == 1 && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shotlight, shotSpawn.position, shotSpawn.rotation);
            anim.SetBool("LightHit", true);
        }
        if (prevUppercutPressed < 0.85 && Input.GetAxis("Firefast") == 1 && Time.time > nextFire && remainingUppercuts > 0)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shotfast, shotSpawn.position, shotSpawn.rotation);
            shutUppercutLight();
            uppercutRefresh = 0;
            anim.SetBool("Uppercut", true);
        }
        if (Input.GetButton("Fire3") && Time.time > nextFire)
        {
            nextFire = Time.time + dashRate;
            StartCoroutine("Dash");
        }
        if (tmp >= frameinvi)
        {
            invincible = false;
            tmp = 0;
        }
        else
            tmp += Time.deltaTime;
        prevUppercutPressed = Input.GetAxis("Firefast");
        prevLightPressed = Input.GetAxis("Firelight");
    }

    IEnumerator Dash()
    {
        Vector3 move;
        Vector3 posFinal;
        Rigidbody2D rig = GetComponent<Rigidbody2D>();
        Vector3 pos = rig.position;

        if (Input.GetAxis("Horizontal") < 0)
            move = new Vector3(-dashSize, 0, 0);
        else if (Input.GetAxis("Horizontal") > 0)
            move = new Vector3(dashSize, 0, 0);
        else if (Input.GetAxis("Vertical") > 0)
            move = new Vector3(0, dashSize, 0);
        else
            move = new Vector3(0, -dashSize, 0);
        posFinal = move + pos;
        for (var i= 0; i != 10; i++)
        {
            transform.position = Vector3.Lerp(rig.position, posFinal, 0.1f);
            yield return null;
        }
    }

    private void resetOrientation()
    {
        anim.SetBool("Left", false);
        anim.SetBool("Right", false);
        anim.SetBool("Up", false);
        anim.SetBool("Down", false);
    }

    public string getOrientation()
    {
        string _orientation = "";

        if (Input.GetAxis("VerticalRight") < 0 && (Input.GetAxis("HorizontalRight") < 0.2 && Input.GetAxis("HorizontalRight") > -0.2))
            orientation = "Down";
        if (Input.GetAxis("VerticalRight") > 0 && (Input.GetAxis("HorizontalRight") < 0.2 && Input.GetAxis("HorizontalRight") > -0.2))
            orientation = "Up";
        if (Input.GetAxis("HorizontalRight") < 0 && (Input.GetAxis("VerticalRight") < 0.2 && Input.GetAxis("VerticalRight") > -0.2))
            orientation = "Left";
        if (Input.GetAxis("HorizontalRight") > 0 && (Input.GetAxis("VerticalRight") < 0.2  && Input.GetAxis("VerticalRight") > -0.2))
            orientation = "Right";
        if (_orientation == "")
        {
            if (orientation != "")
                return orientation;
            return "Down";
        }
        return _orientation;
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            anim.SetBool("Stop", false);
        else
            anim.SetBool("Stop", true);
        resetOrientation();
        orientation = getOrientation();
        anim.SetBool(orientation, true);
        Vector2 move = new Vector2(moveHorizontal, moveVertical);
        Rigidbody2D rig = GetComponent<Rigidbody2D>();
        rig.velocity = move * speed;
    }
}
