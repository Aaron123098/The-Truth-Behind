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

                default:
                    break;

            }

            TriggerDialogue();
        }

    }

}
