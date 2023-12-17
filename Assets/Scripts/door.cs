using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour
{
    public string loadScene;

    Transform player;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Movement>().transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("DoorOpen", Vector3.Distance(transform.position, player.position) < 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ObjectManager.saveCarryingObj();
            SceneManager.LoadScene(loadScene);
        }
    }
}
