using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    private int contadorRepartos = 0;

    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;

    public LifeManager lifeManager;
    public CanvasGroup winBackgroundImageCanvasGroup;
    public AudioSource winAudio;
    public CanvasGroup loseBackgroundImageCanvasGroup;
    public AudioSource loseAudio;
    public GameObject minimapa;
    public Timer timerManager;
    public CanvasGroup timeoutBackgroundImageCanvasGroup;
    public AudioSource timeoutAudio;
    public GameObject timer;
    public GameObject vidas;


    float m_Timer;
    bool m_HasAudioPlayed;


    void Update()
    {
        if (contadorRepartos == 5)
        {
            EndLevel(winBackgroundImageCanvasGroup, false, winAudio);
        }
        else if(lifeManager.GetVidas() == 0 )
        {
            EndLevel(loseBackgroundImageCanvasGroup, true, loseAudio);
        }
        else if (timerManager.GetTiempo() == 0)
        {
            EndLevel(timeoutBackgroundImageCanvasGroup, true, timeoutAudio);
        }
    }
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        minimapa.SetActive(false);
        timer.SetActive(false);
        vidas.SetActive(false);

        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;

        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                Application.Quit();
            }
        }
    }
    public int SumaReparto()
        { return ++contadorRepartos; }

}
