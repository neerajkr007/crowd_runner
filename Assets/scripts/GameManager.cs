using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float radius = 1f;
    float top, bottom, left, right;
    public GameObject clone;
    public GameObject enemyClone1;
    public TMPro.TMP_Text _playersRemaining;
    init _init;
    public Texture2D icon;
    int playersRemaining;
    public GameObject escapeMenu;
    public GameObject escapeMenu2;
    float currentSpeed = 0.06f;
    bool level1;
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
        calcSpots(1f, 0f, 5f, false, "player");
        if(level1)
        {
            calcSpots(20f, 0f, 21f, true, "enemy1");
            Destroy(enemyClone1.transform.parent.GetChild((enemyClone1.transform.parent.childCount - 2)).gameObject);
        }
        else
        {
            calcSpots(34f, 0f, 54f, true, "enemy1");
            Destroy(enemyClone1.transform.parent.GetChild((enemyClone1.transform.parent.childCount - 2)).gameObject);
            Destroy(enemyClone1.transform.parent.GetChild((enemyClone1.transform.parent.childCount - 3)).gameObject);
            // Destroy(enemyClone1.transform.parent.GetChild((enemyClone1.transform.parent.childCount - 4)).gameObject);
            // Destroy(enemyClone1.transform.parent.GetChild((enemyClone1.transform.parent.childCount - 5)).gameObject);
            // Destroy(enemyClone1.transform.parent.GetChild((enemyClone1.transform.parent.childCount - 6)).gameObject);
        }
        _init = clone.transform.parent.gameObject.GetComponent<init>();
    }

    //  void OnGUI ()
    // {
    //     if(GUI.Button(new Rect(10,10,100,100), icon))
    //     {
    //         currentSpeed = _init.forwardSpeed;
    //         _init.forwardSpeed = 0f;
    //         escapeMenu2.SetActive(true);
    //     }
    // }

    void FixedUpdate()
    {
        playersRemaining = Int16.Parse(_playersRemaining.text);
        if(playersRemaining == 0 || clone.transform.parent.childCount == 3)
        {
            currentSpeed = _init.forwardSpeed;
            _init.forwardSpeed = 0f;
            escapeMenu.SetActive(true);
        }
    }

    public void continueGame()
    {
        _init.forwardSpeed = currentSpeed;
    }

    public void restartGame()
    {
        if(level1)
        SceneManager.LoadScene(1);
        else
        SceneManager.LoadScene(2);
    }

    public void exitToMainMenu()
    {
        SceneManager.LoadScene(0);
        //SceneManager.UnloadSceneAsync(1);
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void calcSpots(float crowd, float X, float Y, bool start, string player)
    {
        radius = Mathf.Sqrt(crowd/3.14f);
        //print(radius);
        top    = Mathf.Floor(Y - radius);
        bottom =  Mathf.Ceil(Y + radius);
        left   = Mathf.Floor(X - radius);
        right  =  Mathf.Ceil(X + radius);
         for (float y = top; y <= bottom; y++)
        {
            for (float x = left; x <= right; x++)
            {
                if (inside_circle(X, Y, x, y, radius))
                {
                    // print(x + "  " + y);
                    placeClone(x , y , X, Y, start, player);
                    // print("small will clone");
                }
            }
        }
        StartCoroutine(test(start));
        
    }
    
    IEnumerator test(bool start)
    {
        yield return new WaitForSeconds(0.1f);
        float lx = 0f;
        float hx = 0f;
        Vector3 pos2 = clone.transform.parent.transform.position;
        if(!start)
        print(clone.transform.parent.transform.position.x - _init.highestX + "   " + pos2.x + "  " + _init.lowestX);
        if(!start && (_init.highestX >= 2.5f || _init.lowestX <= -2.5f))
        {
            if(pos2.x < 0)
            {
                for(int i = 3; i < clone.transform.parent.childCount; i++)
                {
                    Vector3 pos3 = clone.transform.parent.GetChild(i).transform.position;
                    pos3.x = pos3.x - (_init.lowestX + 2.5f);
                    clone.transform.parent.GetChild(i).transform.position = pos3;
                    if(pos3.x < lx)
                    lx = pos3.x;
                    if(pos3.x > hx)
                    hx = pos3.x;
                }
                pos2.x = (-hx + lx)/2f + hx;
            }
            else
            {
                for(int i = 3; i < clone.transform.parent.childCount; i++)
                {
                    Vector3 pos3 = clone.transform.parent.GetChild(i).transform.position;
                    pos3.x -= _init.highestX - 2.5f;
                    clone.transform.parent.GetChild(i).transform.position = pos3;
                    if(pos3.x < lx)
                    lx = pos3.x;
                    if(pos3.x > hx)
                    hx = pos3.x;
                }
                pos2.x = (hx - lx)/2f + lx;
            }
            clone.transform.parent.transform.position = pos2;
            
                // if(pos2.x < 0f)
                // print("works ?");
                // if(pos2.x<0f)
                // {
                //     //X -= _init.lowestX;
                //     pos2.x = (pos2.x - (pos2.x - _init.lowestX))/2f;
                //     print(pos2.x);
                // }
                // else
                // {
                //    //X += _init.highestX;
                //    pos2.x = (pos2.x + (pos2.x - _init.highestX)/2f);
                //    print(pos2.x);
                // }
                // clone.transform.parent.transform.position = pos2;
                // //_init.forwardSpeed = 0f;
        }
    }

    void placeClone(float x, float y, float X, float Y, bool start, string player)
    {
        float randomVariable = UnityEngine.Random.Range(-0.05f, 0.05f);
        GameObject go = null;
        if(player == "player")
        {
            go = Instantiate(clone);
            go.transform.parent = clone.transform.parent;
            clone.SetActive(false);
        }
        else if(player == "enemy1")
        {
            go = Instantiate(enemyClone1);
            go.transform.parent = enemyClone1.transform.parent;
            enemyClone1.SetActive(false);
        }
        go.SetActive(true);
        Vector3 pos = go.transform.position;
        // print(x + "  " + y + "  " + X + "  " + Y);
        if(player == "enemy1")
        {
            pos.x = x*0.2f + X + randomVariable;
            pos.y = 0.22f;
            pos.z = y*0.2f + Y + randomVariable;
        }
        else if(player == "player")
        {
            pos.x = x*0.2f + X + randomVariable;
            pos.y = 0f;
            if(!start)
            pos.z = y*0.2f + Y*0.8f + randomVariable;
            else
            pos.z = Y;
        }
        go.transform.position = pos;
        
    }

    bool inside_circle(float X, float Y, float x, float y, float radius)
    {
        float dx = X - x,
              dy = Y - y;
        float distance = Mathf.Sqrt(dx * dx + dy * dy);
        return distance <= radius;
    }



    int firstPedalNumber(int finalCrowdNumber)
    {
        int n = 0;
        n = (int)Mathf.RoundToInt((-1 + Mathf.Sqrt(1 + (8*finalCrowdNumber)))/2);
        if(n < 0)
        n = (int)Mathf.RoundToInt((-1 - Mathf.Sqrt(1 + (8*finalCrowdNumber)))/2);
        return n;
    }

    public void finalLineCrossed()
    {
        print("finished");
        playersRemaining = Int16.Parse(_playersRemaining.text);
        int n = firstPedalNumber(playersRemaining);
        if(level1)
        _init.cameraStopZ = 65f + n -2f;
        else
        _init.cameraStopZ = 78f + n -2f;
        startPlacingFinalClones(n);
    }

    void startPlacingFinalClones(int n)
    {
        for (int i = 3; i < clone.transform.parent.transform.childCount; i++)
            {
                Destroy(clone.transform.parent.transform.GetChild(i).transform.gameObject);
            }
        int currentPedalNumber = n;
        int placedOnPedalsNumber = 0;
        print(n);
        for(int i = 0; i < n; i++)
        {
            float gap = 5f/(float)currentPedalNumber;
            for(int j = 0 ; j < currentPedalNumber; j++)
            {
                float X;
                if(currentPedalNumber % 2 == 0)
                {
                    if(j % 2 == 0)
                    {
                        X = ((j/2) + 1)*gap - gap/2;  
                    }
                    else
                    {
                        X = -(j+1)/2*gap + gap/2;
                    }
                }
                else
                {
                    if(j % 2 == 0)
                    {
                        X = -(j/2)*gap;  
                    }
                    else
                    {
                        X = (j+1)/2*gap;
                    }
                }
                GameObject go = Instantiate(clone);
                go.transform.parent = clone.transform.parent;
                Vector3 pos = go.transform.position;
                pos.x = X;
                pos.y = i*0.5f + 0.5f;
                if(level1)
                pos.z = 64f + i + 1f;
                else
                pos.z = 77f + i + 1f;
                go.transform.position = pos;
                go.SetActive(true);
                placedOnPedalsNumber++;
            }
            currentPedalNumber--;
        }
        if(playersRemaining < placedOnPedalsNumber)
        {
            if(placedOnPedalsNumber - playersRemaining == 1)
            {
                Destroy(clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - 3).transform.gameObject);
                Vector3 pos = clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - 2).transform.position;
                pos.x = 0f;
                clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - 2).transform.position = pos;
            }
            else if(placedOnPedalsNumber - playersRemaining == 2)
            {
                Destroy(clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - 3).transform.gameObject);
                Destroy(clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - 2).transform.gameObject);
                Vector3 pos = clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - 1).transform.position;
                pos.y -= 0.5f;
                pos.z -= 1f;
                clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - 1).transform.position = pos;
            }
        }
        else
        {
            if(placedOnPedalsNumber - playersRemaining == -1)
            {
                GameObject go = Instantiate(clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - 1).gameObject);
                go.transform.parent = clone.transform.parent;
                clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - 1).transform.position = clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - 2).transform.position;
                Vector3 pos = clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - 1).transform.position;
                pos.y += 0.5f;
                pos.z += 1f;
                clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - 1).transform.position = pos;
            }
            else if(placedOnPedalsNumber - playersRemaining == -2)
            {
                for(int i = 2; i < 4; i++)
                {
                    GameObject go1 = Instantiate(clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - i).gameObject);
                    go1.transform.parent = clone.transform.parent;
                    go1.transform.position = clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - i).transform.position;
                }
                Destroy(clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - 3).transform.gameObject);
                float X;
                for(int j = 0; j < 2; j++)
                {
                    if(j % 2 == 0)
                    {
                        X = ((j/2) + 1)*(float)2.5 - (float)2.5/2;  
                    }
                    else
                    {
                        X = -(j+1)/2*(float)2.5 + (float)2.5/2;
                    }
                    Vector3 pos = clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - j - 1).transform.position;
                    pos.x = X;
                    clone.transform.parent.transform.GetChild(clone.transform.parent.transform.childCount - j - 1).transform.position = pos;
                }
            }
        }
    }
}
