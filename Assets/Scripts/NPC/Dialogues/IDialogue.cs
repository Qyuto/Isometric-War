namespace NPC
{
    public interface IDialogue
    {
        public string GetDialogueText();
        public string[] GetDialogueResponseArray();

        public string GetName();
    }
}