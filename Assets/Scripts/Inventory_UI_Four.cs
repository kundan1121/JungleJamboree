using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory_UI_Four : MonoBehaviour
{
    public class PlayerInventoryLevel4 : MonoBehaviour
{
    public Text diamondText; // Reference to the UI Text element
    // Other variables and methods...
}
    private TextMeshProUGUI diamondText;

    // Start is called before the first frame update
    void Start()
    {
        // Find the TextMeshProUGUI component in the children of the GameObject this script is attached to
        diamondText = GetComponent<TextMeshProUGUI>(); 
    }

    // Method to update the diamond text
    public void UpdateDiamondText(Player_Inventory_Four player_Inventory_Four)
    {
        // Update the text to display the correct number of diamonds
        diamondText.text = player_Inventory_Four.NumberOfDiamonds.ToString() + "/30";
    }  
    
}
