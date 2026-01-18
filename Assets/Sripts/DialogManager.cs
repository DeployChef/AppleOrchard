using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    [SerializeField] private Image characterIcon;
    [SerializeField] private Image characterIconOposite;
    [SerializeField] private TextMeshProUGUI dialogueArea;

    [Header("Settings")]
    [SerializeField] private float typingSpeed = 0.02f;

    [Header("Input")]
    [SerializeField] private InputAction continueAction;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private Animator animatorAvatar;
    [SerializeField] private Animator animatorOposite;

    private readonly Queue<DialogueLine> lines = new Queue<DialogueLine>();
    private Dialogue currentDialogue; // Храним текущий диалог
    private bool isFirstLine = true;
    private bool isLastOposite = false;
    private bool isTyping = false;
    public bool IsDialogueActive { get; private set; }
    private string currentLineText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        continueAction = new InputAction("Continue", InputActionType.Button);
        continueAction.AddBinding("<Keyboard>/enter");
        continueAction.AddBinding("<Keyboard>/space");
    }

    private void OnEnable()
    {
        continueAction.Enable();
        continueAction.performed += OnContinuePerformed;
    }

    private void OnDisable()
    {
        continueAction.performed -= OnContinuePerformed;
        continueAction.Disable();
    }

    private void OnContinuePerformed(InputAction.CallbackContext context)
    {
        if (!IsDialogueActive) return;

        if (isTyping)
        {
            StopAllCoroutines();
            dialogueArea.text = currentLineText;
            isTyping = false;
        }
        else
        {
            DisplayNextDialogueLine();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        IsDialogueActive = true;
        isTyping = false;
        isFirstLine = true;
        currentDialogue = dialogue; // Сохраняем ссылку на диалог

        animator.Play("show");

        lines.Clear();

        foreach (DialogueLine line in dialogue.dialogueLines)
        {
            lines.Enqueue(line);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        if (!isFirstLine)
        {
            if (isLastOposite)
            {
                animatorOposite.Play("HideOposite");
            }
            else
            {
                animatorAvatar.Play("HideAvatar");
            }
        }

        if (currentLine.character.isOposite)
        {
            characterIconOposite.sprite = currentLine.character.icon;
            animatorOposite.Play("ShowOposite");
        }
        else
        {
            characterIcon.sprite = currentLine.character.icon;
            animatorAvatar.Play("ShowAvatar");
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine.line));

        isLastOposite = currentLine.character.isOposite;
        isFirstLine = false;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        currentLineText = sentence;
        dialogueArea.text = string.Empty;

        foreach (char letter in sentence)
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void EndDialogue()
    {
        if (isLastOposite)
        {
            animatorOposite.Play("HideOposite");
        }
        else
        {
            animatorAvatar.Play("HideAvatar");
        }

        IsDialogueActive = false;
        isTyping = false;
        animator.Play("hide");

        // Выполняем действие после диалога
        if (currentDialogue != null)
        {
            currentDialogue.ExecuteCompleteAction();
        }

        currentDialogue = null; // Очищаем ссылку
    }
}