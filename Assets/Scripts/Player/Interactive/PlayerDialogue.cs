using Interface.UI;
using NPC;
using UnityEngine;

namespace Player
{
    public class PlayerDialogue : MonoBehaviour
    {
        [SerializeField] private DialogueUI dialogueUI;

        public void InitDialogue(IDialogue dialogue)
        {
            dialogueUI.InitDialogueUI(dialogue, dialogue.GetName());
        }
    }
}