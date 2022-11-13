using Interface;
using Items;
using NPC;
using UnityEngine;

namespace Player
{
    //Todo: Divide this class into two subclasses or come up with a name suitable for the actions of this class
    public class PlayerInteractive : MonoBehaviour
    {
        [SerializeField] private float rayLength;
        [SerializeField] private LayerMask interactiveMask;
        [SerializeField] private PlayerDialogue playerDialogue;
        [SerializeField] private PlayerInventory playerInventory;

        private RaycastHit2D _hit2D;
        private ISelected _selected;

        private void Update()
        {
            FindSelected();
            if (Input.GetKeyDown(KeyCode.E))
                FindInteractive();
        }

        private T RaycastFinder<T>() where T : class
        {
            _hit2D = Physics2D.Raycast(transform.position, transform.right, rayLength, interactiveMask);
            if (_hit2D.transform == null) return null;

            return !_hit2D.transform.TryGetComponent(out T findResult) ? null : findResult;
        }

        private void FindSelected()
        {
            ISelected selected = RaycastFinder<ISelected>();

            if (selected == null) return;
            if (selected != _selected && _selected != null)
            {
                _selected.Undo();
                _selected = null;
            }
                


            selected.Select();
            _selected = selected;
        }


        private void FindInteractive()
        {
            IInteractive interactive = RaycastFinder<IInteractive>();
            if (interactive == null) return;
            StartInteractive(_hit2D.transform.gameObject, interactive.GetInteractiveType());
        }

        private void StartInteractive(GameObject interactiveObject, InteractiveType type)
        {
            switch (type)
            {
                case InteractiveType.Dialogue:
                    if (interactiveObject.TryGetComponent(out IDialogue dialogue))
                        playerDialogue.InitDialogue(dialogue);
                    break;
                case InteractiveType.Item:
                    if (interactiveObject.TryGetComponent(out IUsableItem usable))
                    {
                        playerInventory.InitInventory(usable);
                        _selected = null;
                    }

                    break;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.right * rayLength);
        }
    }
}