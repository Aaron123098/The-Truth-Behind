using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public bool isPlayerOver = false;
    static public bool firstTriviaCompletedAdmin = false;

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
            dialogue.name = "Laura (Administradora)";
            switch (FindAnyObjectByType<DialogueManager>().dialogueState)
            {
                case DialogueManager.DialogueState.ThirdRundCompleted:
                    if (firstTriviaCompletedAdmin)
                    {
                        dialogue.sentences = new string[2]
                        {
                            "Gracias por tu ayuda.",
                            "Por favor, continua con el siguiente.",
                        };
                    }
                    else
                    {
                        dialogue.sentences = new string[7] {
                           "Estoy muy orgullosa de ti.",
                           "Has demostrado todas tus capacidades y no me queda más que agradecerte por todo lo que hiciste.",
                           "Pero, ahora, me queda pedirte 2 últimos favores...",
                           "Será proponer 2 planes de reestructuración para nosotros...",
                           "El primero, que ayude a gestionar nuestros recursos económicos",
                           "El segundo, para poder proteger nuestros derechos.",
                           "Gracias."
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
