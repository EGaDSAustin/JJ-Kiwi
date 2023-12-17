using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanScript : MonoBehaviour
{
    public GameObject scanInstance;

    GameObject[] scanObjects = new GameObject[144];

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(scan());
    }

    IEnumerator scan()
    {
        foreach (GameObject obj in scanObjects)
            Destroy(obj);

        for (int y = 1; y < 10; y++)
        {
            for (int x = 1; x < 17; x++)
            {

                Vector3 crosshair = new Vector3(Screen.width * (x / 18f), Screen.height * (y / 11f));
                Ray ray = Camera.main.ScreenPointToRay(crosshair);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject newObj = Instantiate(scanInstance);
                    newObj.transform.position = hit.point;
                    scanObjects[(y - 1) * 16 + x - 1] = newObj;
                }
            }

            yield return new WaitForSeconds(.1f);
        }
    }
}
