using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    public CrazyDuck crazyDuckPrefab;

    public Vector3 positionStart;
    public Vector3 positionEnd;

    public Vector3 velocityStart;
    public Vector3 velocityEnd;

    public Vector3 angularVelocityStart;
    public Vector3 angularVelocityEnd;

    private IEnumerator Spawner()
    {
        while (true)
        {
            Vector3 crazyDuckPosition = new Vector3(Random.Range(positionStart.x, positionEnd.x),
                                                    Random.Range(positionStart.y, positionEnd.y),
                                                    Random.Range(positionStart.z, positionEnd.z));

            CrazyDuck crazyDuck = Instantiate(crazyDuckPrefab, crazyDuckPosition, Quaternion.identity) as CrazyDuck;
            crazyDuck.mStartingVelocity = new Vector3(Random.Range(velocityStart.x, velocityEnd.x),
                                                      Random.Range(velocityStart.y, velocityEnd.y),
                                                      Random.Range(velocityStart.z, velocityEnd.z));
            crazyDuck.mStartingAngularVelocity = new Vector3(Random.Range(angularVelocityStart.x, angularVelocityEnd.x),
                                                             Random.Range(angularVelocityStart.y, angularVelocityEnd.y),
                                                             Random.Range(angularVelocityStart.z, angularVelocityEnd.z));

            yield return new WaitForSeconds(2);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
