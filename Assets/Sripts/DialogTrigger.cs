using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[System.Serializable]
public class DialogueCharacter
{
    public Sprite icon;
    public bool isOposite;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3,10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
    public DialogueAction onDialogueComplete; // Действие после диалога

    // Метод для выполнения действия
    public void ExecuteCompleteAction()
    {
        if (onDialogueComplete != null)
        {
            onDialogueComplete.Execute();
        }
    }
}


public class DialogTrigger : MonoBehaviour
{

    [SerializeField] private Dialogue dialogue;

    public void TriggerDialog()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialog();
        }
    }
}


[System.Serializable]
public class DialogueAction
{
    public enum ActionType
    {
        None,
        PlayTimeline,
        ActivateObject,
        DeactivateObject,
        LoadScene,
        CustomEvent
    }

    public ActionType actionType;

    // Для таймлайна
    public PlayableDirector timeline;

    // Для объектов
    public GameObject targetObject;

    // Для сцены
    public string sceneName;

    // Кастомное событие
    public UnityEvent onDialogueEnd;

    public void Execute()
    {
        switch (actionType)
        {
            case ActionType.None:
                break;

            case ActionType.PlayTimeline:
                if (timeline != null)
                {
                    timeline.Play();
                }
                else
                {
                    Debug.LogWarning("Timeline не назначен!");
                }
                break;

            case ActionType.ActivateObject:
                if (targetObject != null)
                {
                    targetObject.SetActive(true);
                }
                break;

            case ActionType.DeactivateObject:
                if (targetObject != null)
                {
                    targetObject.SetActive(false);
                }
                break;

            case ActionType.LoadScene:
                if (!string.IsNullOrEmpty(sceneName))
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
                }
                break;

            case ActionType.CustomEvent:
                onDialogueEnd?.Invoke();
                break;
        }
    }
}