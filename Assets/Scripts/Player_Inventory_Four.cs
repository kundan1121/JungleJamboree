using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory_Four : MonoBehaviour
{
    public Inventory_UI_Four ui;
    
    public GameObject boat; 

    public int NumberOfDiamonds { get; private set; }

    private void Start()
    {
        // Initially hide the boat
        SetBoatVisibility(false);
    }

    public void DiamondCollected()
    {
        // Increment the number of diamonds
        NumberOfDiamonds++;
        // Invoke the event
        ui.UpdateDiamondText(this);


        if (NumberOfDiamonds >= 10)
        {
            SetBoatVisibility(true);
        }
    }

    private void SetBoatVisibility(bool visible)
    {
        // Set the boat object's visibility
        if (boat != null)
        {
            boat.SetActive(visible);
        }
    }
}
