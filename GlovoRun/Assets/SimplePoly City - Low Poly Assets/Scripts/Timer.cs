using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoCrono;
    [SerializeField] private float tiempo;
    [SerializeField] private GameObject TimerImage;
    [SerializeField] private GameObject TimerText;


    private int tiempoMinutos, tiempoSegundos;
    void Cronometro()
    {
        if (tiempo > 0)
        {
            tiempo -= Time.deltaTime;

            tiempoMinutos = Mathf.FloorToInt(tiempo / 60);
            tiempoSegundos = Mathf.FloorToInt(tiempo % 60);


            if (tiempo <= 0)
            {
                textoCrono.text = "00:00";
            }
            else
            {
                textoCrono.text = string.Format("{0:00}:{1:00}", tiempoMinutos, tiempoSegundos);
            }
                
        }

        else
        {
            tiempo = 0;
            TimerImage.SetActive(false);
            TimerText.SetActive(false);
        }


    }

    public float GetTiempo()
    {
        return tiempo;
    }

    // Update is called once per frame
    void Update()
    {
        Cronometro();
    }
}
