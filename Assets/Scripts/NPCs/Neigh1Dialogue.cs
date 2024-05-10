using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neigh1Dialogue : MonoBehaviour
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
            dialogue.name = "Erick (Vecino)";
            switch (FindAnyObjectByType<DialogueManager>().dialogueState)
            {
                case DialogueManager.DialogueState.FrstRunCompleted:
                    dialogue.sentences = new string[2] { 
                        "Hola, sé que ayudas a la gente.",
                        "Por favor, mira esto."
                    };
                    break;

                default:
                    break;

            }



            TriggerDialogue();
        }

    }

}
