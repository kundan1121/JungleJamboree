using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    //public Animator anim;
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> Checkpoint;
    [SerializeField] List<GameObject> Trap;
    [SerializeField] Vector3 vectorPoint;
    bool isDead = false;
    [SerializeField] Vector3 trapPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isDead) { 
        player.transform.position = vectorPoint;
            isDead = false;
        }
    }

  /*  private void OnTriggerEnter(Collider other)
    {
        vectorPoint = player.transform.position;
        Destroy(other.gameObject);
    }
  */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Trap")
        {
            isDead = true;
        }

        if (collision.collider.tag == "CheckPoint")
        {
            vectorPoint = player.transform.position;
            //Destroy(collision.gameObject);
        }
    }

}
