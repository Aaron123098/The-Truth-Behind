using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public bool isPlayerOver = false;
    static public bool firstTriviaCompletedSeller = false;
    public JSONSaveLoadSystem jsonFile;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void Start()
    {
        jsonFile = FindAnyObjectByType<JSONSaveLoadSystem>();
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
            dialogue.name = "Ricardo (Vendedor)";
            switch (FindAnyObjectByType<DialogueManager>().dialogueState)
            {
                case DialogueManager.DialogueState.BeforeFrstRun:
                    dialogue.sentences = new string[3] { 
                        "¡Hola!",
                        "¿Has intentado hablar con Raúl?",
                        "Te guiará en lo primero que debes hacer..."
                    };
                    break;

                case DialogueManager.DialogueState.AfterFrstRun:

                    if (jsonFile.l1act)
                    {
                        dialogue.sentences = new string[8] {
                            "¡Hola!",
                            "Veo que ya que intentaste superar los cuartos...",
                            "Son algo difíciles, ¿verdad?",
                            "Pero cuando vences a tus adversarios, te dejan algunas recompensas",
                            "Luchar con enemigos no es la única manera de conseguirlas. Si les haces algunos favores a los vecinos, podrás" +
                            "recibirlas también.",
                            "Y hablando de ello, quisiera que me ayudases con algo...",
                            "Verás, cuando fuiste a batallar, uno de los soldados del regidor se sorpendió tanto con tus habilidades que" +
                            "para intentar que no volvieses a los cuartos, nos envió algunos documentos que registran hechos pasados de gran" +
                            "importancia...",
                            "Sin embargo, no puedo verificar si son confiables o pertinentes. ¿Crees que me puedes ayudar con eso?"
                        };
                    }
                    else
                    {
                        dialogue.sentences = new string[1] {
                            "Lo estás haciendo bien. Sigue tu camino."
                        };
                    }
                    
                    break;

                case DialogueManager.DialogueState.FrstRunCompleted:

                    if(firstTriviaCompletedSeller)
                    {
                        if (jsonFile.l2act)
                        {
                            dialogue.sentences = new string[5]
                            {
                                "¡Hola de nuevo!",
                                "Te agradezco mucho por la ayuda que me diste.",
                                "Realmente te estás desempeñando muy bien",
                                "Por ello, me gustaría que puedas ayudarme nuevamente con estos documentos que me han llegado",
                                "¿Puedes hacerlo?"
                            };
                        }
                        else
                        {
                            dialogue.sentences = new string[1] {
                            "Lo estás haciendo bien. Sigue tu camino."
                            };
                        }
                    }
                    else
                    {
                        if (jsonFile.l1act)
                        {
                            dialogue.sentences = new string[8] {
                            "¡Hola!",
                            "Veo que ya que intentaste superar los cuartos...",
                            "Son algo difíciles, ¿verdad?",
                            "Pero cuando vences a tus adversarios, te dejan algunas recompensas",
                            "Luchar con enemigos no es la única manera de conseguirlas. Si les haces algunos favores a los vecinos, podrás" +
                            "recibirlas también.",
                            "Y hablando de ello, quisiera que me ayudases con algo...",
                            "Verás, cuando fuiste a batallar, uno de los soldados del regidor se sorpendió tanto con tus habilidades que" +
                            "para intentar que no volvieses a los cuartos, nos envió algunos documentos que registran hechos pasados de gran" +
                            "importancia...",
                            "Sin embargo, no puedo verificar si son confiables o pertinentes. ¿Crees que me puedes ayudar con eso?"
                        };
                        }
                        else
                        {
                            dialogue.sentences = new string[1] {
                            "Lo estás haciendo bien. Sigue tu camino."
                        };
                        }
                    }
                    break;
                default:
                    break;

            }



            TriggerDialogue();
        }

    }

}
