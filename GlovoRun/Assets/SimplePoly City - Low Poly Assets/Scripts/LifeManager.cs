using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Image[] hearts; // Array para los corazones en el HUD
    [SerializeField] private GameObject player; // Referencia al jugador
    public AudioSource gameOver;

    private int lives; // Contador de vidas restantes

    private void Start()
    {
        lives = hearts.Length; // Inicializa las vidas según el número de corazones
    }

    // Método para perder una vida
    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            hearts[lives].gameObject.SetActive(false);

        }
    }
    public int GetVidas() { return lives; }
}
