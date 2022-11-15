using NPC;
using UI.Main;
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