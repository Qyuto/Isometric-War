using Interface;
using NPC.Dialogues;
using UnityEngine;

namespace NPC.Main
{
    public class NpcDialogue : MonoBehaviour, IDialogue, IInteractive
    {
        [SerializeField] private string npcName;
        [SerializeField] private DialogueInfo[] dialogueInfos;
        [SerializeField] private InteractiveType interactiveType;
        private int _pos = -1;

        public string GetDialogueText()
        {
            Debug.Log(dialogueInfos.Length);
            _pos++;
            if (_pos == dialogueInfos.Length) _pos = 0;

            return dialogueInfos[_pos].GetDialogueText();
        }

        public string[] GetDialogueResponseArray() => dialogueInfos[_pos].GetResponseArray();

        public string GetName() => npcName;

        public InteractiveType GetInteractiveType() => interactiveType;
    }
}