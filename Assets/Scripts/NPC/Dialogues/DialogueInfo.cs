using UnityEngine;

namespace NPC.Dialogues
{
    [CreateAssetMenu(fileName = "dialogue", menuName = "Dialogue", order = 0)]
    public class DialogueInfo : ScriptableObject
    {
        [SerializeField] private string dialogueText;
        [SerializeField] private string[] dialogueResponse;

        public string GetDialogueText() => dialogueText;
        public string[] GetResponseArray() => dialogueResponse;

    }
}