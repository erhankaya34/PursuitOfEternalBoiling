using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Element currentElement;
    public SpriteRenderer handSpriteRenderer; 

    public void AddElement(Element element)
    {
        if (currentElement == null)
        {
            currentElement = element;
            handSpriteRenderer.sprite = element.elementIcon; 
            element.gameObject.SetActive(false); 
        }
    }

    public bool HasElement(string elementName)
    {
        return currentElement != null && currentElement.elementName == elementName;
    }

    public void RemoveElement()
    {
        if (currentElement != null)
        {
            handSpriteRenderer.sprite = null; 
            Destroy(currentElement.gameObject); 
            currentElement = null;
        }
    }
    
    public void DropElement()
    {
        if (currentElement != null)
        {
            GameObject droppedElement = Instantiate(currentElement.gameObject, transform.position, Quaternion.identity);
            droppedElement.SetActive(true);
            RemoveElement();
        }
    }

}