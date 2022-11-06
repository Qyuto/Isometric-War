using Interface.UI;
using NPC;
using UnityEngine;

namespace Player
{
    public class PlayerDialogue : MonoBehaviour
    {
        [SerializeField] private float findRadius;
        [SerializeField] private LayerMask dialogueNpc;
        [SerializeField] private DialogueUI dialogueUI;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindDialogueNpc();
            }
        }

        private void FindDialogueNpc()
        {
            Collider2D res = Physics2D.OverlapCircle(transform.position, findRadius, dialogueNpc);
            if (res.TryGetComponent(out IDialogue dialogue))
                dialogueUI.InitDialogueUI(dialogue, dialogue.GetName());
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, findRadius);
        }
    }
}