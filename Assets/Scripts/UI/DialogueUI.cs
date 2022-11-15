using System.Collections;
using System.Collections.Generic;
using NPC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Main
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private TextMeshProUGUI npcName;
        [SerializeField] private HorizontalLayoutGroup buttonLayout;
        [SerializeField] private Button buttonResponsePrefab; // Todo: Add button for dialogue UI

        [SerializeField] private CanvasGroup dialogueGroup;
        private IDialogue _dialogue;
        private List<Button> _buttons = new();

        public void InitDialogueUI(IDialogue dialogue, string npcName)
        {
            dialogueGroup.gameObject.SetActive(true);

            _dialogue = dialogue;
            this.npcName.text = npcName;
            NextDialogue();
        }

        public void NextDialogue()
        {
            StopAllCoroutines();
            StartCoroutine(DialogueAnimation(_dialogue.GetDialogueText()));
            // dialogueText.text = _dialogue.GetDialogueText();
            UpdateButtons();
        }

        private IEnumerator DialogueAnimation(string text)
        {
            dialogueText.text = "";
            foreach (var symbol in text)
            {
                dialogueText.text += symbol;
                yield return new WaitForSeconds(0.05f);
            }
        }

        private void UpdateButtons()
        {
            foreach (var button in _buttons)
                Destroy(button.gameObject);
            _buttons.Clear();

            string[] response = _dialogue.GetDialogueResponseArray();

            foreach (var t in response)
            {
                Button newButton = Instantiate(buttonResponsePrefab, Vector3.zero, Quaternion.identity,
                    buttonLayout.transform);
                newButton.onClick.AddListener(NextDialogue);

                newButton.GetComponentInChildren<TextMeshProUGUI>().text = t;
                _buttons.Add(newButton);
            }
        }
    }
}