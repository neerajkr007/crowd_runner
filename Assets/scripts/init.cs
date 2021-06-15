using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class init : MonoBehaviour
{
    private Vector3 pos;
    public bool run = true; 
    public float highestZ = 0f;
    public float lowestZ = 0f;
    public float highestX = 0f;
    public float lowestX = 0f;
    public float forwardSpeed = 0.06f;
    float horizontalSpeed = 0.05f;
    //int j = 0;
    bool once = true;
    float offsetX = 0f;
    float realX0 = 0f;

    public bool isMovable = true;
    bool once1 = true;
    bool once2 = true;
    bool once3 = true;
    bool once4 = true;
    bool once5 = true;
    
    bool once6 = true;
    public TMPro.TMP_Text crowdCount;
    public TMPro.TMP_Text enemyCount;
    private GameObject cameraObj;
    // private Camera camera;
    bool cameraMovable = false;

    bool level1 = true;
    public GameManager gameManager;
    public GameObject escapeMenu3;
    public GameObject tbd;
    Animator animator;
    public float cameraStopZ = 100f;
    
public GameObject confetti;

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
        for(int i = 3; i < gameObject.transform.childCount; i++)
        {
            Vector3 pos = gameObject.transform.gameObject.transform.GetChild(i).position;
            pos.y = 0f;
            gameObject.transform.gameObject.transform.GetChild(i).position = pos;
        }
        cameraObj = gameObject.transform.GetChild(0).gameObject;
        //camera = cameraObj.GetComponent<Camera>();
        animator = cameraObj.GetComponent<Animator>();
        //print(camera.transform.position);
        // y -2x - 0.5 = 0
    }

    void FixedUpdate()
    {
        if(run)
        {
            pos = gameObject.transform.position;
            pos.z += forwardSpeed;
            //test += forwardSpeed;
            // print(Time.time + "   " + test);
            gameObject.transform.position = pos;
        }

        for (int i = 3; i < gameObject.transform.childCount; i++)
        {
            
            Vector3 pos = gameObject.transform.GetChild(i).transform.position;
            //print(pos.z);
            if (i != 3)
            {
                if (highestZ < pos.z)
                highestZ = pos.z;
                if(lowestZ > pos.z)
                lowestZ = pos.z;
                if(pos.x > highestX)
                highestX = pos.x;
                if(pos.x < lowestX)
                lowestX = pos.x;
            }
            else
            {
                highestZ = pos.z;
                lowestZ = pos.z;
                lowestX = pos.x;
                highestX = pos.x;
            }
            
            
            // _crowdCount++;

        }

        if (Input.GetMouseButton(0) && isMovable)
        {
            Input.simulateMouseWithTouches = true;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 touchedPos = Camera.main.ScreenToWorldPoint(mousePos);

            //print(touchedPos);
            Vector3 pos = gameObject.transform.position;
            pos.z = gameObject.transform.position.z;
            pos.y = gameObject.transform.position.y;
            //print(pos.x);
            float offset = (highestX - lowestX)/2f;
            if(touchedPos.x + offset <= 2.3f && touchedPos.x - offset >= -2.3f)
            {
                pos.x = touchedPos.x;
            }
            
            gameObject.transform.position = pos;
        }
        

        if(level1)
        {
            // final positon satright
            if(highestZ >= 60f && highestZ <= 61f)
            {
                if(once1)
                {
                    once1 = false;
                    forwardSpeed = 0f;
                    gameManager.finalLineCrossed();
                    for (int i = 3; i < gameObject.transform.childCount; i++)
                    {
                        Animator animator2;
                        animator2 = gameObject.transform.GetChild(i).GetComponent<Animator>();
                        animator2.SetBool("finished", true);
                    }
                    
                    crowdCount.transform.parent.transform.parent.gameObject.SetActive(false);
                    animator.SetBool("doCamAnim", true);
                    StartCoroutine(moveCamera());
                }
                
                // cam = 3.5 -5.28
            }
            else
            {
                moveClusterToCenter(59f, 62f);
            }
            if(lowestZ <=53f && lowestZ >= 52f && once3)
            {
                once3 = false;
                for (int i = 3; i < gameObject.transform.childCount; i++)
                {
                    Destroy(gameObject.transform.GetChild(i).transform.gameObject);
                }
                gameManager.calcSpots(float.Parse(crowdCount.text), gameObject.transform.position.x, gameObject.transform.position.z, false, "player");
            }


            if(cameraObj.transform.position.z <= cameraStopZ && cameraMovable) // +5 where ever u wanna go //  40 + pedalnumber - 2 // cam posi = 2.79 3.3 17(17 = ladder posi - 7) rota 40 -60 0
            {
                Vector3 pos2 = Camera.main.transform.position;
                pos2.y += 0.03f;
                pos2.z += 0.06f;
                Camera.main.transform.position = pos2;
            }
            else if(cameraObj.transform.position.z >= cameraStopZ && once4)
            {
                confetti.transform.position = cameraObj.transform.position;
                confetti.SetActive(true);
                escapeMenu3.SetActive(true);
            }

            moveClusterToCenter(21f, 24f);
        }
        else
        {
            if(highestZ >= 49f && highestZ <= 53f && once6)
            {
                //once6 = false;
                for(int i = 3; i < gameObject.transform.childCount; i++)
                {

                    //print(gameObject.transform.GetChild(i).transform.position.z + "   " + gameObject.transform.GetChild(i).transform.position.y);
                    if((gameObject.transform.GetChild(i).transform.position.x < -1.025 && gameObject.transform.GetChild(i).transform.position.x > -2.25) || (gameObject.transform.GetChild(i).transform.position.x > 1.025 && gameObject.transform.GetChild(i).transform.position.x < 2.25))
                    {   
                        if(!gameObject.transform.GetChild(i).gameObject.GetComponent<Rigidbody>())
                        {
                            gameObject.transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
                            crowdCount.text = (Int16.Parse(crowdCount.text) - 1).ToString();
                        }
                    }
                    if(gameObject.transform.GetChild(i).transform.position.y > 0.2f && gameObject.transform.GetChild(i).gameObject.GetComponent<Rigidbody>() != null)
                    {
                        //print("above");
                        //print(gameObject.transform.GetChild(i).gameObject.GetComponent<Rigidbody>());
                        gameObject.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0f, 20f, -10f), gameObject.transform.GetChild(i).transform.position);
                        
                        StartCoroutine(destroy(gameObject.transform.GetChild(i).gameObject));
                    }
                }
            }
        
            if(highestZ >= 76f && highestZ <= 77f)
            {
                if(once1)
                {
                    once1 = false;
                    forwardSpeed = 0f;
                    gameManager.finalLineCrossed();
                    for (int i = 3; i < gameObject.transform.childCount; i++)
                    {
                        Animator animator2;
                        animator2 = gameObject.transform.GetChild(i).GetComponent<Animator>();
                        animator2.SetBool("finished", true);
                    }
                    
                    crowdCount.transform.parent.transform.parent.gameObject.SetActive(false);
                    animator.SetBool("doCamAnim", true);
                    StartCoroutine(moveCamera());
                }
                
                // cam = 3.5 -5.28
            }
            

            if(cameraObj.transform.position.z <= cameraStopZ && cameraMovable) // +5 where ever u wanna go //  40 + pedalnumber - 2 // cam posi = 2.79 3.3 17(17 = ladder posi - 7) rota 40 -60 0
            {
                Vector3 pos2 = Camera.main.transform.position;
                pos2.y += 0.03f;
                pos2.z += 0.06f;
                Camera.main.transform.position = pos2;
            }
            else if(cameraObj.transform.position.z >= cameraStopZ && once4)
            {
                confetti.transform.position = cameraObj.transform.position;
                confetti.SetActive(true);
                escapeMenu3.SetActive(true);
            }

            if(((highestZ - lowestZ)/2f + lowestZ) >=52.5 && ((highestZ - lowestZ)/2f + lowestZ) <=53.5 && once5)
            {
                once5 = false;
                for(int i = 3; i < gameObject.transform.childCount; i++)
                {
                    Destroy(gameObject.transform.GetChild(i).gameObject);
                }
                gameManager.calcSpots(float.Parse(crowdCount.text), gameObject.transform.position.x, gameObject.transform.position.z, false, "player");
            }

            moveClusterToCenter(60f, 63f);
        
        }
        //moveClusterToCenter(21f, 24f);
        
        
    }

    IEnumerator destroy(GameObject gameObject)
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void moveClusterToCenter (float initial, float final)  // 15 to 18
    {
        if (highestZ >= initial && highestZ <= final && gameObject.transform.position.x != 0f)
        {
            isMovable = false;
            //print(Time.frameCount + "  " + Time.time);
            if(once)
            {
                once = false;
                offsetX = gameObject.transform.position.x;
                realX0 = (highestX - lowestX)/2f;
                horizontalSpeed = -((offsetX)/30f);
            }
            Vector3 pos = gameObject.transform.position;
            if(offsetX < 0)
            {
                if(pos.x <= 0)
                {
                    pos.x += horizontalSpeed;
                }
                else
                {
                    pos.x = realX0;
                    return;
                }
            }
            else if(offsetX > 0)
            {
                if(pos.x >= 0)
                {
                    pos.x += horizontalSpeed;
                }
                else
                {
                    pos.x = -realX0;
                    return;
                }
            }
            
            
            //pos.x = realX0;
            gameObject.transform.position = pos;
        }
        else if(enemyCount.text == "0" && once2)
        {
            once2 = false;
            isMovable = true;
            for (int i = 3; i < gameObject.transform.childCount; i++)
            {
                Destroy(gameObject.transform.GetChild(i).transform.gameObject);
            }
            gameManager.calcSpots(float.Parse(crowdCount.text), gameObject.transform.position.x, gameObject.transform.position.z, false, "player");
        }
    }

    IEnumerator moveCamera()
    {
        yield return new WaitForSeconds(2.5f);
        animator.SetBool("doCamAnim", false);
        cameraMovable = true;
    }
}
