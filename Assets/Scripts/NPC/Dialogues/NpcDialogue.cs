using NPC.Dialogues;
using UnityEngine;

namespace NPC.Main
{
    public class NpcDialogue : NpcMain, IDialogue
    {
        [SerializeField] private string npcName;
        [SerializeField] private DialogueInfo[] dialogueInfos;
        private int _pos = -1;

        public string GetDialogueText()
        {
            _pos++;
            if (_pos == dialogueInfos.Length) _pos = 0;

            return dialogueInfos[_pos].GetDialogueText();
        }

        public string[] GetDialogueResponseArray() => dialogueInfos[_pos].GetResponseArray();

        public string GetName() => npcName;
    }
}