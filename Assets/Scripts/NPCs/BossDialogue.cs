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
                        "Al final era cierto lo que decían de ti...",
                        "No debí subestimarte...",
                        "Pero, no me habrás derrotado hasta que no resuelvas lo que tengo preparado para ti..."
                };
                break;

            case DialogueManager.DialogueState.FrstRunCompleted:
                dialogue.sentences = new string[4] {
                        "¡¿Otra vez?!",
                        "No lo entiendo, jamás nadie había podido superar esta zona ni una sola vez...",
                        "Y tú ya me derrotaste en 2 ocasiones...",
                        "Pero no, tendrás que pasar por mis preguntas para que me puedas vencer..."
                };
                break;

            case DialogueManager.DialogueState.ScndRunCompleted:
                dialogue.sentences = new string[2] {
                        "¿Para qué molestarme?",
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
