using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchBlackScript : MonoBehaviour
{
    public Material pitchBlackMat;
    public Material normalMaterial;

    public Doodad triggerLights;

    bool bLightsOn = false;

    // Start is called before the first frame update
    void Start()
    {
        lightsOff();
    }

    private void Update()
    {
        if (triggerLights.isPickedup && !bLightsOn)
            lightsOn();
    }

    public void lightsOff()
    {
        foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
        {
            mesh.material = pitchBlackMat;
        }
    }

    public void lightsOn()
    {
        bLightsOn = true;
        foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
        {
            mesh.material = normalMaterial;
        }
    }
}
