using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideDialogue : MonoBehaviour
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
            dialogue.name = "Raúl (Guia)";
            switch (FindAnyObjectByType<DialogueManager>().dialogueState)
            {
                case DialogueManager.DialogueState.BeforeFrstRun:
                    dialogue.sentences = new string[11] {
                        "Hola, jugador",
                        "¿Estás listo?",
                        "Recuerda, eres la única persona que puede ayudarnos a recuperar nuestros recursos," +
                        "nuestros recuerdos de hechos pasados y los conocimientos sobre aquellos lugares que solíamos habitar.",
                        "Necesitamos hacer frente a nuestros enemigos y empezar a recordar y recuperar todo.",
                        "Hemos construido un transportador que te llevará a la zona donde batallarás por nosotros.",
                        "Esta zona, a la cual llamamos 'Los cuartos', es un lugar muy particular.",
                        "Allí, te encontrarás con varias personas que tratarán de evitar que logres tu objetivo.",
                        "Tu deber será pasar a través de ellos y llegar hasta el 'regidor'.",
                        "Creemos que él es el responsable de que hayamos perdido todo lo que nos pertenece.",
                        "Ve a la zona norte del patio y entra al transportador para ir a 'los cuartos'.",
                        "Te esperaré aquí cuando termines tu primer intento..."
                    };
                    break;

                case DialogueManager.DialogueState.AfterFrstRun:
                    dialogue.sentences = new string[4] {
                        "¡Hola!",
                        "Veo que ya pudiste probar cómo son 'los cuartos'...",
                        "Puedes ir a intentar superarlos las veces que quieras.",
                        "Ahora, también puedes hablar con Ricardo, el vendedor. Él te podrá ayudar a facilitar tus próximos intentos " +
                        "brindándote algunas mejoras." +
                        "¡Ve y averigua qué es lo que ofrece!"
                    };
                    break;
                case DialogueManager.DialogueState.FrstRunCompleted:
                    dialogue.sentences = new string[6]
                    {
                        "Qué tal.",
                        "Lograste derrotar al regidor.",
                        "Ahora se han abierto más posibilidades para poder encontrar lo que buscamos.",
                        "Me parece que Ricardo tiene novedades para ti. Anda a verlo.",
                        "También puedes ir a ver a Susana y a Erick, creo que te andan buscando.",
                        "Ellos están en la zona este y suroeste del patio."
                    };
                    break;
                case DialogueManager.DialogueState.ScndRunCompleted:
                    dialogue.sentences = new string[3]
                    {
                        "¡Has vuelto!",
                        "Estás teniendo mucho éxito explorando los cuartos.",
                        "Me han llegado estos documentos y me gustaría que me puedas ayudar con ellos, por favor"
                    };
                    break;
                case DialogueManager.DialogueState.ThirdRundCompleted:
                    dialogue.sentences = new string[6]
                    {
                        "¡LO HICISTE!",
                        "Finalmente, el regidor terminó de dar todas las pistas faltantes.",
                        "Gracias a ti hemos podido recuperar gran parte del conocimiento que se nos fue quitado.",
                        "Ahora, como último paso, ve a hablar con Laura, la administradora de este lugar.",
                        "Ella tiene las 2 últimas tareas para ti.",
                        "Complétalas, y te lo agradeceremos por siempre."
                    };
                    break;
                default:
                    break;

            }

            TriggerDialogue();
        }

    }

}
