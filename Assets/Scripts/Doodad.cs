using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doodad : MonoBehaviour
{
    public int value;
    public Material[] materials;

    public bool onDisplay = false;
    public bool claimed = false;

    public bool isPickedup;
    private Collider meshCollider;

    bool setup = false;

    private void Start()
    {
        SetMaterial();
        isPickedup = false;
        meshCollider = transform.GetComponent<Collider>();
        setup = true;
    }

    private void SetMaterial()
    {
        MeshRenderer mr = transform.GetChild(0).GetComponent<MeshRenderer>();
        mr.material = materials[Random.Range(0, materials.Length - 1)];

        if (onDisplay)
        {
            gameObject.layer = 0;
            GetComponent<Rigidbody>().isKinematic = true;
            if (!claimed)
                mr.material = materials[materials.Length - 1];
        }
    }

    public IEnumerator delayPickUp(Transform playerHand)
    {
        yield return null;
        PickUp(playerHand);
    }

    public void PickUp(Transform playerHand)
    {
        if (!isPickedup)
        {
            transform.parent = playerHand;
            transform.localPosition = Vector3.zero;
            //Debug.Log("picking up" + gameObject.name);

            isPickedup = true;
            meshCollider.enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void PutDown()
    {
        if (isPickedup)
        {
            transform.parent = null;
            transform.localScale = Vector3.one;
            //Debug.Log("putting down" + gameObject.name);

            isPickedup = false;
            meshCollider.enabled = true;
            GetComponent<Rigidbody>().isKinematic = false;

            //Check for statues
            foreach (Doodad doodad in FindObjectsByType<Doodad>(FindObjectsSortMode.None))
                if (doodad.onDisplay && doodad.value == value)
                {
                    ObjectManager.completedDoodads[value] = true;
                    doodad.GetComponentInChildren<MeshRenderer>().material = GetComponentInChildren<MeshRenderer>().material;
                    Destroy(gameObject);
                }
        }
    }
}
