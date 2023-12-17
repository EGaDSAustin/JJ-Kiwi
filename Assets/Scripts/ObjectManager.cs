using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static bool[] completedDoodads = new bool[5] { false, false, false, false, false };
    static int heldObject = -1;

    public GameObject[] theDoodads;

    public Doodad[] onDisplayDoodads = new Doodad[0];

    public static void saveCarryingObj()
    {
        GameObject objInHand = FindObjectOfType<PlayerDooer>().playerHand;
        if (objInHand.transform.childCount == 0)
            heldObject = -1;
        else
        {
            objInHand = objInHand.transform.GetChild(0).gameObject;
            heldObject = objInHand.GetComponent<Doodad>().value;
        }
    }

    private void Start()
    {
        if (heldObject != -1)
            for (int i = 0; i < theDoodads.Length; i++)
                if (theDoodads[i].GetComponent<Doodad>().value == heldObject)
                {
                    Doodad newObj = Instantiate(theDoodads[i]).GetComponent<Doodad>();
                    StartCoroutine(newObj.delayPickUp(FindObjectOfType<PlayerDooer>().playerHand.transform));
                    FindObjectOfType<PlayerDooer>().heldDoodad = newObj;
                }

        if (onDisplayDoodads.Length > 0)
        {
            for (int i = 0; i < completedDoodads.Length; i++)
                if (completedDoodads[i])
                    foreach (Doodad statue in onDisplayDoodads)
                        if (statue.value == i)
                            statue.claimed = true;

        }
    }

}
