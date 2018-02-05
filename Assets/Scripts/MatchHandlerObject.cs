using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Palier
{
    public int[] nbEnnemies;
    public int[] time;
}

[System.Serializable]
public class MatchHandlerObject : MonoBehaviour {

    public List<Palier> match = new List<Palier>();
    public int totalTime = 0;

    public GameObject enemy;
    public Transform[] spawnPoints;

    private float time = 0;
    private int round = 0;
    private int step = 0;
    private bool gameEnd = false;
    private int roundsWon = 0;
    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("PlayerObject");
    }
	
    private int setNbEnnemies()
    {
        /*if (endPoints == 4)
            return match[round].nbEnnemies[step];
        else if (endPoints == 3)
            return match[round].nbEnnemies[step] * 0.9;
        else if (endPoints == 2)
            return match[round].nbEnnemies[step] * 0.8;
        else if (endPoints == 1)
            return match[round].nbEnnemies[step] * 0.75;*/
        return match[round].nbEnnemies[step];
    }

    void Spawn()
    {
        if (GameObject.FindObjectsOfType<EnnemyObject>().Length == 0)
            roundsWon++;
        int nbEnnemies = setNbEnnemies();
        for (int i = 0; i < match[round].nbEnnemies[step]; ++i)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
        if (step < match[round].nbEnnemies.Length - 1)
            step++;
        else
        {
            step = 0;
            round++;
        }
    }

	void Update () {
        time += Time.deltaTime;

        if ((round >= match.Count || time >= totalTime) && GameObject.FindObjectsOfType<EnnemyObject>().Length == 0)
            gameEnd = true;

        if (gameEnd && roundsWon < (match.Count / 2) + 1)
            player.GetComponent<Player>().Setscore(-350);

        if (gameEnd)
            SceneManager.LoadScene("WinScene");

        if (round < match.Count && ((GameObject.FindObjectsOfType<EnnemyObject>().Length == 0 &&
            round > 0 && step > 0) || (time >= match[round].time[step])))
            Spawn();
    }
}
