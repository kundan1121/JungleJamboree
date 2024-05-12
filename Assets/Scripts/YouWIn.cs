using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWIn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Winpanel;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("You Win");
            Time.timeScale = 0;
        }
    }
}
