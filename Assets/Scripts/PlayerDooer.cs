using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDooer : MonoBehaviour
{
    public GameObject crosshair;
    public float pickupDistance = 2.5f;
    public LayerMask doodad;
    public GameObject playerHand;

    private Doodad heldDoodad;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DoDoo();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            UndoDoo();
        }
    }

    private void DoDoo()
    {
        Vector3 crosshair = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(crosshair);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance, doodad))
        {
            Doodad doodad = hit.collider.transform.parent.GetComponent<Doodad>();
            if (doodad != null)
            {
                doodad.PickUp(playerHand.transform);
                heldDoodad = doodad;
            }
        }
    }

    private void UndoDoo()
    {
        if (heldDoodad != null)
        {
            heldDoodad.PutDown();
            heldDoodad = null;
        }
    }
}
