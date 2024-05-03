using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTemplate : MonoBehaviour
{
    public Dialogue dialogue;
    public bool isPlayerOver = false;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerOver = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerOver = false;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && isPlayerOver)
        {
            dialogue.name = "Nombre";
            switch (FindAnyObjectByType<DialogueManager>().dialogueState)
            {
                case DialogueManager.DialogueState.BeforeFrstRun:
                    dialogue.sentences = new string[2] { 
                        "prueba", 
                        "prueba 2" 
                    };
                    break;

                default:
                    break;

            }



            TriggerDialogue();
        }

    }

}
