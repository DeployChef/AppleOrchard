using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    [SerializeField] private Image characterIcon;
    [SerializeField] private TextMeshProUGUI dialogueArea;

    [Header("Settings")]
    [SerializeField] private float typingSpeed = 0.02f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

        //private readonly Queue<DialogueLine> lines = new Queue<DialogueLine>();

    public bool IsDialogueActive { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartDialogue(/*Dialogue dialogue*/)
    {
        IsDialogueActive = true;

        animator.Play("show");

        //lines.Clear();

        //foreach (DialogueLine line in dialogue.dialogueLines)
        //{
        //    lines.Enqueue(line);
        //}

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        //if (lines.Count == 0)
        //{
        //    EndDialogue();
        //    return;
        //}

        //DialogueLine currentLine = lines.Dequeue();

        //characterIcon.sprite = currentLine.character.icon;
        //characterName.text = currentLine.character.name;

        //StopAllCoroutines();
        //StartCoroutine(TypeSentence(currentLine.line));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueArea.text = string.Empty;

        foreach (char letter in sentence)
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void EndDialogue()
    {
        IsDialogueActive = false;
        animator.Play("hide");
    }
}