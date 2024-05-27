using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public bool isPlayerOver = false;
    static public int triviaNmbr = 0;

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
                    JSONSaveLoadSystem jsonFile = FindAnyObjectByType<JSONSaveLoadSystem>();

                    if (triviaNmbr == 1)
                    {
                        if (jsonFile.l10act)
                        {
                            dialogue.sentences = new string[2]
                            {
                                "Gracias por tu ayuda.",
                                "Por favor, continua con el siguiente.",
                            };
                        }
                        else
                        {
                            dialogue.sentences = new string[1] {
                               "Vuelve a hablar conmigo en 1 segundo."
                            };
                        }
                    }
                    else if (triviaNmbr == 0)
                    {
                        if (jsonFile.l9act)
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
                        else
                        {
                            dialogue.sentences = new string[1] {
                               "Vuelve a hablar conmigo en 1 segundo."
                            };
                        }
                        
                    }
                    else if(triviaNmbr == 2)
                    {
                        dialogue.sentences = new string[5] {
                           "¡LO LOGRASTE!",
                           "Le has devuelto al pueblo la memoria, sus recursos y todo lo que se le había arrebatado...",
                           "Estamos infinitamente agradecidos contigo.",
                           "Buena suerte en todo lo que se te venga.",
                           "¡Hasta luego! :D."
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
