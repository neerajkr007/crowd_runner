using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rockEnemy : MonoBehaviour
{
    public float launchVelocityY = 700f;
    public float launchVelocityZ = -800f;

    public init _init;
    bool once1 = true;
    public GameObject tbd;
    bool level1 = false;
    bool once2 = false;

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            level1 = true;
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            level1 = false;
        }
    }

    void FixedUpdate()
    {
        if(level1)
        {
            if(((_init.highestZ <= 42.5f && _init.highestZ >= 41.5f) || (Input.GetKeyDown(KeyCode.Space))) && once1)
            {
                once1 = false;
                once2 = true;
                Vector3 pos = gameObject.transform.position;
                pos.x = UnityEngine.Random.Range(-2.1f, 2.1f);
                gameObject.transform.position = pos;
                gameObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, launchVelocityY,launchVelocityZ));
            }
        }
        else
        {
            if(((_init.highestZ <= 18.5 && _init.highestZ >= 17.5) || (Input.GetKeyDown(KeyCode.Space))) && once1)
            {
                once1 = false;
                once2 = true;
                Vector3 pos = gameObject.transform.position;
                pos.x = UnityEngine.Random.Range(-1f, 1f);
                launchVelocityZ = -700f;//UnityEngine.Random.Range(-730f, -700f);
                gameObject.transform.position = pos;
                gameObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, launchVelocityY,launchVelocityZ));
            }
        }
        
        if(once2)
        {
            //print(Time.time);
        }
    }

int count = 0;
int playersRemaining = 0;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "clone(Clone)")
        {
            print("collided  " + count);
            collision.collider.gameObject.transform.parent = collision.collider.gameObject.transform.parent.transform.parent;
            collision.collider.gameObject.AddComponent<Rigidbody>();
            collision.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
            collision.collider.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0,0,-60f), collision.contacts[0].point);
            collision.collider.transform.parent = tbd.transform;
            if(level1)
            StartCoroutine(destroy(collision.collider.gameObject));
            StartCoroutine(destroySelf());
            collision.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
            count++;
            playersRemaining = Int16.Parse(_init.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text);
            playersRemaining--;
            _init.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = playersRemaining.ToString();
        }
        else if(collision.collider.name == "floor")
        {
            //print("yolo");
        }
    }

    IEnumerator destroy(GameObject go)
    {
        yield return new WaitForSeconds(3f);
        Destroy(go);
    }

    IEnumerator destroySelf()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
