using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCollision : MonoBehaviour
{
    public TMPro.TMP_Text enemyCrowdCount;
    public TMPro.TMP_Text playerCrowdCount;
    int collisionCount = 0;
    public init _init;
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "clone(Clone)" && collisionCount == 0)
        {
            collider.gameObject.GetComponent<BoxCollider>().enabled = false;
            collisionCount = 1;
            int newEnemyCount = Int16.Parse(enemyCrowdCount.text);
            newEnemyCount -= 1;
            int newPlayerCount = Int16.Parse(playerCrowdCount.text);
            newPlayerCount -= 1;
            playerCrowdCount.text = newPlayerCount.ToString(); 
            enemyCrowdCount.text = newEnemyCount.ToString();
            _init.forwardSpeed = 0.015f;
            //_init.isMovable = false;
            if(newEnemyCount == 0)
            {
                gameObject.transform.parent.GetChild(0).gameObject.SetActive(false);
                _init.forwardSpeed = 0.06f;
                // for (int i = 3; i < collider.transform.parent.childCount; i++)
                // {
                //     Destroy(collider.transform.parent.GetChild(i).transform.gameObject);
                // }
                // gameManager.calcSpots(float.Parse(crowdCount.text), gameObject.transform.position.x, gameObject.transform.position.z, false, "player");
                //_init.isMovable = true;
            }
            Animator animator = gameObject.GetComponent<Animator>();
            animator.SetTrigger("attack");
            Animator animator2 = collider.gameObject.GetComponent<Animator>();
            animator2.SetTrigger("attack");
            StartCoroutine(destroy(collider.gameObject));
        }
    }

    IEnumerator destroy(GameObject go)
    {
        yield return new WaitForSeconds(0.25f);
        // Animator animator = gameObject.GetComponent<Animator>();
        // animator.SetBool("dead", true);
        // Animator animator2 = go.GetComponent<Animator>();
        // animator.SetBool("dead", true);
        Destroy(go);
        Destroy(gameObject);
        //_init.forwardSpeed = 0.06f;
    }
}
