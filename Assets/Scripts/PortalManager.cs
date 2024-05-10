using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public Monument[] monuments;
    public GameObject portal; 

    public void CheckMonuments()
    {
        foreach (var monument in monuments)
        {
            if (!monument.isCompleted)
                return;
        }
        
        Instantiate(portal, new Vector3(0, -6, 0), Quaternion.identity);
    }
}
