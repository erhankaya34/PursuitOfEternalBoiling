using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portal
{
    public class PortalManager : MonoBehaviour
    {
        public Monument[] monuments;
        public GameObject portalPrefab; 

       public void CheckMonuments()
       {
           foreach (var monument in monuments)
           {
               if (!monument.isCompleted)
               {
                   return;
               }
           }
           
           Instantiate(portalPrefab, new Vector3(0, -6, 0), Quaternion.identity);
       }

    }
}
