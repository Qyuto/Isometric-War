using System;
using Interface;
using Interface.Items;
using NPC;
using UnityEngine;

namespace Player
{
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

            if (selected == null)
            {
                if (_selected == null) return;
                _selected.Undo();
                _selected = null;
                return;
            }

            if (_selected != null && _selected != selected)
            {
                _selected.Undo();
                _selected = null;
            }


            _selected = selected;
            selected.Select();
        }

        private void FindInteractive()
        {
            IInteractive interactive = RaycastFinder<IInteractive>();
            if (interactive == null) return;
            StartInteractive(_hit2D.transform.gameObject, interactive.GetInteractiveType());
        }

        private void StartInteractive(GameObject interactiveObject, InteractiveType type)
        {
            _selected.Select();
            switch (type)
            {
                case InteractiveType.Dialogue:
                    if (interactiveObject.TryGetComponent(out IDialogue dialogue))
                        playerDialogue.InitDialogue(dialogue);
                    break;
                case InteractiveType.Item:
                    if (interactiveObject.TryGetComponent(out IUsable usable))
                    {
                        _selected = null;
                        playerInventory.InitInventory(usable);
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