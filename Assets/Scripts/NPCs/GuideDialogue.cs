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
            dialogue.name = "Ra�l (Guia)";
            switch (FindAnyObjectByType<DialogueManager>().dialogueState)
            {
                case DialogueManager.DialogueState.BeforeFrstRun:
                    dialogue.sentences = new string[11] {
                        "Hola, jugador",
                        "�Est�s listo?",
                        "Recuerda, eres la �nica persona que puede ayudarnos a recuperar nuestros recursos," +
                        "nuestros recuerdos de hechos pasados y los conocimientos sobre aquellos lugares que sol�amos habitar.",
                        "Necesitamos hacer frente a nuestros enemigos y empezar a recordar y recuperar todo.",
                        "Hemos construido un transportador que te llevar� a la zona donde batallar�s por nosotros.",
                        "Esta zona, a la cual llamamos 'Los cuartos', es un lugar muy particular.",
                        "All�, te encontrar�s con varias personas que tratar�n de evitar que logres tu objetivo.",
                        "Tu deber ser� pasar a trav�s de ellos y llegar hasta el 'regidor'.",
                        "Creemos que �l es el responsable de que hayamos perdido todo lo que nos pertenece.",
                        "Ve a la zona norte del patio y entra al transportador para ir a 'los cuartos'.",
                        "Te esperar� aqu� cuando termines tu primer intento..."
                    };
                    break;

                case DialogueManager.DialogueState.AfterFrstRun:
                    dialogue.sentences = new string[4] {
                        "�Hola!",
                        "Veo que ya pudiste probar c�mo son 'los cuartos'...",
                        "Puedes ir a intentar superarlos las veces que quieras.",
                        "Ahora, tambi�n puedes hablar con Ricardo, el vendedor. �l te podr� ayudar a facilitar tus pr�ximos intentos " +
                        "brind�ndote algunas mejoras." +
                        "�Ve y averigua qu� es lo que ofrece!"
                    };
                    break;
                case DialogueManager.DialogueState.FrstRunCompleted:
                    dialogue.sentences = new string[6]
                    {
                        "Qu� tal.",
                        "Lograste derrotar al regidor.",
                        "Ahora se han abierto m�s posibilidades para poder encontrar lo que buscamos.",
                        "Me parece que Ricardo tiene novedades para ti. Anda a verlo.",
                        "Tambi�n puedes ir a ver a Susana y a Erick, creo que te andan buscando.",
                        "Ellos est�n en la zona este y suroeste del patio."
                    };
                    break;
                case DialogueManager.DialogueState.ScndRunCompleted:
                    dialogue.sentences = new string[3]
                    {
                        "�Has vuelto!",
                        "Est�s teniendo mucho �xito explorando los cuartos.",
                        "Me han llegado estos documentos y me gustar�a que me puedas ayudar con ellos, por favor"
                    };
                    break;
                case DialogueManager.DialogueState.ThirdRundCompleted:
                    dialogue.sentences = new string[6]
                    {
                        "�LO HICISTE!",
                        "Finalmente, el regidor termin� de dar todas las pistas faltantes.",
                        "Gracias a ti hemos podido recuperar gran parte del conocimiento que se nos fue quitado.",
                        "Ahora, como �ltimo paso, ve a hablar con Laura, la administradora de este lugar.",
                        "Ella tiene las 2 �ltimas tareas para ti.",
                        "Compl�talas, y te lo agradeceremos por siempre."
                    };
                    break;
                default:
                    break;

            }

            TriggerDialogue();
        }

    }

}
