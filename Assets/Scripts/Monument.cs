using UnityEngine;
using UnityEngine.Rendering.Universal;
public class Monument : MonoBehaviour
{
    public Element requiredElement;
    public Transform placementPosition; 
    public Light2D monumentLight; 
    public GameObject elementHint; 
    public bool isCompleted = false;
    void Awake()
    {
        monumentLight.enabled = false; 
        elementHint.SetActive(true); 
    }
    public void PlaceElement(Inventory inventory)
    {
        Instantiate(requiredElement, placementPosition.position, Quaternion.identity); 
        inventory.RemoveElement(); 
        
        monumentLight.enabled = true;
        elementHint.SetActive(false);
        
        isCompleted = true;
        FindObjectOfType<PortalManager>().CheckMonuments(); 


    }
}