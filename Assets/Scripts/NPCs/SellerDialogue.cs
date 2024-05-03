using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerDialogue : MonoBehaviour
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
                    dialogue.sentences = new string[9] {
                        "¡Hola!",
                        "Veo que ya que intentaste superar los cuartos...",
                        "Son algo difíciles, ¿verdad?",
                        "No te preocupes, yo te puedo ayudar con algunos recursos que tengo...",
                        "Pero a cambio, me tendrás que dar algunas de las monedas que vayas encontrando.",
                        "Luchar con enemigos no es la única manera de conseguirlas. Si les haces algunos favores a los vecinos, podrás" +
                        "recibirlas también.",
                        "Y hablando de ello, quisiera que me ayudases con algo...",
                        "Verás, cuando fuiste a batallar, uno de los soldados del regidor se sorpendió tanto con tus habilidades que" +
                        "para intentar que no volvieses a los cuartos, nos envió algunos documentos que registran hechos pasados de gran" +
                        "importancia...",
                        "Sin embargo, no puedo verificar si son confiables o pertinentes. ¿Crees que me puedes ayudar con eso?"
                    };
                    break;

                default:
                    break;

            }



            TriggerDialogue();
        }

    }

}
