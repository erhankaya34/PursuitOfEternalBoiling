using UnityEngine;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        private Inventory inventory;

        void Awake()
        {
            inventory = GetComponent<Inventory>();
        }

        public void HandleInteraction()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                bool interactionHandled = false;
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);

                // İlk olarak, monument ile etkileşimi kontrol et
                foreach (var hit in hits)
                {
                    Monument monument = hit.GetComponent<Monument>();
                    if (monument != null && inventory.HasElement(monument.requiredElement.elementName))
                    {
                        monument.PlaceElement(inventory);
                        interactionHandled = true;
                        break;
                    }
                }

                // Eğer monument ile etkileşim olmadıysa ve envanter boşsa yerdeki elementleri kontrol et
                if (!interactionHandled)
                {
                    foreach (var hit in hits)
                    {
                        Element element = hit.GetComponent<Element>();
                        if (element != null && inventory.currentElement == null)
                        {
                            inventory.AddElement(element);
                            hit.gameObject.SetActive(false);
                            interactionHandled = true;
                            break;
                        }
                    }
                }

                // Eğer hiçbir etkileşim olmadıysa ve envanterde element varsa elementi yere bırak
                if (!interactionHandled && inventory.currentElement != null)
                {
                    inventory.DropElement();
                }
            }
        }
    }
}