using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class crowdIncrement : MonoBehaviour
{
    public GameManager gm;
    public GameObject playerCrowd;
    public GameObject siblingWall;
    //public TMPro.TMP_Text playerCrowdCount;
    // bool once = true;
    void Start()
    {
        //print("started");
    }
    void OnTriggerEnter(Collider collider)
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        siblingWall.GetComponent<BoxCollider>().enabled = false;
        //collider.enabled = false;
        if(collider.gameObject.name == "clone(Clone)" || collider.gameObject.name == "clone(Clone)(Clone)")
        {
            string name = gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.name;
            string previousValue = playerCrowd.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text;
            //print(playerCrowd.transform.childCount);
            for (int i = 3; i < playerCrowd.transform.childCount; i++)
            {
                Destroy(playerCrowd.transform.GetChild(i).transform.gameObject);
            }
            if (name.Contains("+"))
            {
                int value = Int16.Parse(name.TrimStart('+'));
                value += Int16.Parse(previousValue);
                // if(playerCrowd.transform.position.x <= -2.3)
                // {
                //     gm.calcSpots(value, playerCrowd.transform.position.x + 0.2f, playerCrowd.transform.position.z, false, "player");
                // }
                // else if(playerCrowd.transform.position.x >= 2.3)
                // {
                //     gm.calcSpots(value, playerCrowd.transform.position.x - 0.2f, playerCrowd.transform.position.z, false, "player");
                // }
                // else
                // {
                //     gm.calcSpots(value, playerCrowd.transform.position.x, playerCrowd.transform.position.z, false, "player");
                // }
                gm.calcSpots(value, playerCrowd.transform.position.x, playerCrowd.transform.position.z, false, "player");
                playerCrowd.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = value.ToString();
                //print(value);
            }
            else if(name.Contains("x"))
            {
                float value = float.Parse(name.TrimStart('x'));
                value = Mathf.RoundToInt(value*Int16.Parse(previousValue));
                gm.calcSpots(value, playerCrowd.transform.position.x, playerCrowd.transform.position.z, false, "player");
                playerCrowd.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = value.ToString();
            }
        }
        
    }
}
