using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public bool bossDefeated = false;

    public void TriggerDialogue()
    {
        dialogue.name = "Regidor";
        switch (FindAnyObjectByType<DialogueManager>().dialogueState)
        {
            case DialogueManager.DialogueState.BeforeFrstRun:
            case DialogueManager.DialogueState.AfterFrstRun:
                dialogue.sentences = new string[4] {
                        "No puede ser...",
                        "Al final era cierto lo que dec�an de ti...",
                        "No deb� subestimarte...",
                        "Pero, no me habr�s derrotado hasta que no resuelvas lo que tengo preparado para ti..."
                };
                break;

            case DialogueManager.DialogueState.FrstRunCompleted:
                dialogue.sentences = new string[4] {
                        "��Otra vez?!",
                        "No lo entiendo, jam�s nadie hab�a podido superar esta zona ni una sola vez...",
                        "Y t� ya me derrotaste en 2 ocasiones...",
                        "Pero no, tendr�s que pasar por mis preguntas para que me puedas vencer..."
                };
                break;

            case DialogueManager.DialogueState.ScndRunCompleted:
                dialogue.sentences = new string[2] {
                        "�Para qu� molestarme?",
                        "Ya sabes lo que sigue..."
                };
                break;

            default:
                break;

        }
        bossDefeated = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }    

}
