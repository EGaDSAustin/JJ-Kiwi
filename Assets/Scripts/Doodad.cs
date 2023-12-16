using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doodad : MonoBehaviour
{
    public int value;
    public Material[] materials;

    public bool isPickedup;
    private MeshCollider meshCollider;

    private void Start()
    {
        SetMaterial();
        isPickedup = false;
        meshCollider = transform.GetChild(0).GetComponent<MeshCollider>();
    }

    private void SetMaterial()
    {
        MeshRenderer mr = transform.GetChild(0).GetComponent<MeshRenderer>();
        mr.material = materials[Random.Range(0, materials.Length)];
    }

    public void PickUp(Transform playerHand)
    {
        if (!isPickedup)
        {
            transform.parent = playerHand;
            transform.localPosition = Vector3.zero;
            Debug.Log("picking up" + gameObject.name);

            isPickedup = true;
            meshCollider.enabled = false;
        }
    }

    public void PutDown()
    {
        if (isPickedup)
        {
            transform.parent = null;
            Debug.Log("putting down" + gameObject.name);

            isPickedup = false;
            meshCollider.enabled = true;
        }
    }
}
