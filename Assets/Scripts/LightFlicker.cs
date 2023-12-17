using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LightFlickering());
    }

    IEnumerator LightFlickering()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 15f));
            GetComponent<Light>().enabled = false;
            yield return new WaitForSeconds(Random.Range(.1f, 1));
            GetComponent<Light>().enabled = true;
        }
    }
}
