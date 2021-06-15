using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyWithDelay : MonoBehaviour
{
    void FixedUpdate()
    {
        if(gameObject.transform.childCount > 0)
        {
            for(int i = 0; i < gameObject.transform.childCount; i++)
            {
                StartCoroutine(destroy(gameObject.transform.GetChild(i).gameObject));
            }
        }
    }

    IEnumerator destroy(GameObject go)
    {
        yield return new WaitForSeconds(2f);
        Destroy(go);
    }
}
