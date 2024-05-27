using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when something get into the alta, make the runes glow
namespace Cainos.PixelArtTopDown_Basic
{

    public class PropsAltar : MonoBehaviour
    {
        public List<SpriteRenderer> runes;
        public float lerpSpeed;

        private Color curColor;
        private Color targetColor;

        private SceneController sceneController;

        public bool isPlayerOver = false;

        public bool altarAv;

        private void Start()
        {
            sceneController = GameObject.FindAnyObjectByType<SceneController>();
            altarAv = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            targetColor = new Color(1, 1, 1, 1);
            isPlayerOver = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            targetColor = new Color(1, 1, 1, 0);
            isPlayerOver = false;
        }

        private void Update()
        {
            curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

            foreach (var r in runes)
            {
                r.color = curColor;
            }

            if (Input.GetKey(KeyCode.E) && isPlayerOver && altarAv)
            {
                sceneController.LoadScene("Game");
            }
        }

    }
}
