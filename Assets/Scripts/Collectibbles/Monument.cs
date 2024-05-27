using Player;
using Portal;
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
    public void PlaceElement(PlayerInventory playerInventory)
    {
        Instantiate(requiredElement, placementPosition.position, Quaternion.identity); 
        playerInventory.RemoveElement(); 
        
        monumentLight.enabled = true;
        elementHint.SetActive(false);
        
        isCompleted = true;
        FindObjectOfType<PortalManager>().CheckMonuments(); 
    }
}