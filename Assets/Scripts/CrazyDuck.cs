using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyDuck : MonoBehaviour
{
    private Rigidbody mRigidBody;

    public Vector3 mStartingVelocity;
    public Vector3 mStartingAngularVelocity;

    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();

        mRigidBody.velocity = mStartingVelocity;
        mRigidBody.angularVelocity = mStartingAngularVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -1)
            transform.position = Vector3.up * 1;
    }

    void OnCollisionEnter(Collision collision)
    {
        Wall wall = collision.gameObject.GetComponent<Wall>();

        if (wall != null)
        {
            Vector3 oldVelocity = mRigidBody.velocity;

            mRigidBody.velocity = Vector3.Reflect(mRigidBody.velocity.normalized, wall.normal);

            mRigidBody.velocity =  new Vector3(mStartingVelocity.x * mRigidBody.velocity.x, mStartingVelocity.y * mRigidBody.velocity.y, mStartingVelocity.z * mRigidBody.velocity.z);
        }
    }
}
