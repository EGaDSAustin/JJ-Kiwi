using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public Transform cube;
    public Transform point1;
    public Transform point2;

    public float speed = .01f;

    bool gotoPoint1 = false;

    // Update is called once per frame
    void Update()
    {
        if (gotoPoint1)
        {
            cube.position = Vector3.MoveTowards(cube.position, point1.position, speed);
            if (Vector3.Distance(cube.position, point1.position) < .1)
                gotoPoint1 = false;
        }
        else
        {
            cube.position = Vector3.MoveTowards(cube.position, point2.position, speed);
            if (Vector3.Distance(cube.position, point2.position) < .1)
                gotoPoint1 = true;
        }
    }
}
