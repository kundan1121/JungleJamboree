using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond_Four : MonoBehaviour
{
    private AudioSource audioSource;
    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;
    private bool isCollected;
    

    void Start() {
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update() {
        
        if(isCollected && !audioSource.isPlaying) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player_Inventory_Four player_Inventory_Four = other.GetComponent<Player_Inventory_Four>();

        if (player_Inventory_Four != null)
        {
            boxCollider.enabled = false;
            meshRenderer.enabled = false;
            isCollected = true;
            player_Inventory_Four.DiamondCollected();
            audioSource.Play();
            
        }
    }
}
