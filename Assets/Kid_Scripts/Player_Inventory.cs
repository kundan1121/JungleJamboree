using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    // Static reference to the single instance of PlayerInventory
    private static PlayerInventory instance;

    // Public property to access the single instance of PlayerInventory
    public static PlayerInventory Instance
    {
        get
        {
            // If the instance is null, find it in the scene or create a new one
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerInventory>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "PlayerInventory";
                    instance = obj.AddComponent<PlayerInventory>();
                }
            }
            return instance;
        }
    }

    public int NumberOfDiamonds { get; private set; }

    // Event to notify listeners when diamonds are collected
    public UnityEvent<PlayerInventory> OnDiamondsCollected;

    public void DiamondCollected()
    {
        // Increment the number of diamonds
        NumberOfDiamonds++;
        // Invoke the event
        OnDiamondsCollected.Invoke(this);
    }
}
