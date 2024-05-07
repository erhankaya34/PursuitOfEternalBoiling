using UnityEngine;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        private bool isDraggingItem = false;
        private GameObject draggedItem;

        public void HandleInteraction()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isDraggingItem)
                {
                    PickUpItem();
                }
                else
                {
                    DropItem();
                }
            }

            if (isDraggingItem && Input.GetMouseButton(0))
            {
                DragItem();
            }
        }

        private void PickUpItem()
        {
            // Nesneyi toplama işlemi
        }

        private void DropItem()
        {
            // Nesneyi bırakma işlemi
        }

        private void DragItem()
        {
            // Nesneyi sürükleme işlemi
        }
    }
}