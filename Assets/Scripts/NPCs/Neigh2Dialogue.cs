using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neigh2Dialogue : MonoBehaviour
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
            dialogue.name = "Susana (vecino)";
            switch (FindAnyObjectByType<DialogueManager>().dialogueState)
            {
                case DialogueManager.DialogueState.FrstRunCompleted:
                    JSONSaveLoadSystem jsonFile = FindAnyObjectByType<JSONSaveLoadSystem>();

                    if (jsonFile.l7act)
                    {
                        dialogue.sentences = new string[2] {
                        "Holaaaaaaaaaa",
                        "Mira lo que me ha llegado, seguro tú me puedes ayudar..."
                        };
                    }
                    else
                    {
                        dialogue.sentences = new string[1] {
                            "Lo estás haciendo bien. Sigue tu camino."
                        };
                    }

                    break;

                default:
                    break;

            }



            TriggerDialogue();
        }

    }

}
