using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] string[] gameOverMessages;

        [SerializeField] GameObject screen;
        [SerializeField] Text header;

        public IEnumerator GameOverCo()
        {
            screen.SetActive(true);
            header.gameObject.SetActive(false);

            screen.GetComponent<CanvasGroup>().alpha = 0f;

            float t = 0f;

            while (t < 1)
            {
                t += 0.4f * Time.deltaTime;
                screen.GetComponent<CanvasGroup>().alpha = t;
                yield return null;
            }

            screen.GetComponent<CanvasGroup>().alpha = 1;

            header.gameObject.SetActive(true);

            header.text = gameOverMessages[Random.Range(0, gameOverMessages.Length)];

            yield return null;
        }
    }
}