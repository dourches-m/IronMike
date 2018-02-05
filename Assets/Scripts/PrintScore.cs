using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintScore : MonoBehaviour {

    public GameObject playerObject;
    int score = 0;
	// Use this for initialization
	void Start () {
	}
    List<GameObject> tmp = new List<GameObject>();
    // Update is called once per frame
    void Update () {
        playerObject = GameObject.Find("PlayerObject");
        int scoretmp = playerObject.GetComponent<Player>().GetScore();
        int i = 0;
        if (score != scoretmp)
        {
            for (int j = 0; !j.Equals(tmp.Count); ++j)
            {
                Destroy(tmp[j]);
            }
            score = scoretmp;
            if (scoretmp > 0)
            {
                foreach (char cc in scoretmp.ToString())
                {
                    switch (cc)
                    {
                        case '0':
                            tmp.Add(GameObject.Instantiate(Resources.Load("Chiffre0")) as GameObject);
                            break;
                        case '1':
                            tmp.Add(GameObject.Instantiate(Resources.Load("Chiffre1")) as GameObject);
                            break;
                        case '2':
                            tmp.Add(GameObject.Instantiate(Resources.Load("Chiffre2")) as GameObject);
                            break;
                        case '3':
                            tmp.Add(GameObject.Instantiate(Resources.Load("Chiffre3")) as GameObject);
                            break;
                        case '4':
                            tmp.Add(GameObject.Instantiate(Resources.Load("Chiffre4")) as GameObject);
                            break;
                        case '5':
                            tmp.Add(GameObject.Instantiate(Resources.Load("Chiffre5")) as GameObject);
                            break;
                        case '6':
                            tmp.Add(GameObject.Instantiate(Resources.Load("Chiffre6")) as GameObject);
                            break;
                        case '7':
                            tmp.Add(GameObject.Instantiate(Resources.Load("Chiffre7")) as GameObject);
                            break;
                        case '8':
                            tmp.Add(GameObject.Instantiate(Resources.Load("Chiffre8")) as GameObject);
                            break;
                        case '9':
                            tmp.Add(GameObject.Instantiate(Resources.Load("Chiffre9")) as GameObject);
                            break;
                        default:
                            tmp.Add(GameObject.Instantiate(Resources.Load("Chiffre0")) as GameObject);
                            break;
                    }
                    tmp[tmp.Count - 1].transform.Translate(this.transform.position);
                    tmp[tmp.Count - 1].transform.position = new Vector3(tmp[tmp.Count - 1].transform.position.x + i, tmp[tmp.Count - 1].transform.position.y, tmp[tmp.Count - 1].transform.position.z);
                    i++;
                }
            }
            else
            {
                tmp.Add(GameObject.Instantiate(Resources.Load("Chiffre0")) as GameObject);
                tmp[tmp.Count - 1].transform.Translate(this.transform.position);
                tmp[tmp.Count - 1].transform.position = new Vector3(tmp[tmp.Count - 1].transform.position.x + i, tmp[tmp.Count - 1].transform.position.y, tmp[tmp.Count - 1].transform.position.z);
            }
        }
    }
}
