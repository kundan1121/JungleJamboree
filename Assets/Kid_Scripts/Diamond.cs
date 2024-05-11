using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
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
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (playerInventory != null)
        {
            boxCollider.enabled = false;
            meshRenderer.enabled = false;
            isCollected = true;
            playerInventory.DiamondCollected();
            audioSource.Play();
            
        }
    }
}
