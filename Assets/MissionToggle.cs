using UnityEngine;

public class MissionToggle : MonoBehaviour
{
    public GameObject missionPanel; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            missionPanel.SetActive(!missionPanel.activeSelf);
        }
    }
}
