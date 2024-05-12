using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public Inventory_UI ui;

    public int NumberOfDiamonds { get; private set; }

    public void DiamondCollected()
    {
        // Increment the number of diamonds
        NumberOfDiamonds++;
        // Invoke the event
        ui.UpdateDiamondText(this);

        if (NumberOfDiamonds == 15){
            SceneManager.LoadScene(4);
        }
    }
}
