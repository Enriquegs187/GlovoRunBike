using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private LifeManager lifeManager; // Referencia al script LifeManager
    public AudioSource pitidoCoche;
    public AudioSource pedidoCompletado;
    public GameEnding gameEnding;

    private bool isInvincible = false; // Indica si el jugador está temporalmente invencible
    [SerializeField] private float invincibilityDuration = 2.0f; // Duración del tiempo de espera

    private void OnCollisionEnter(Collision collision)
    {
        // Detecta si colisiona con un coche
        if (collision.gameObject.CompareTag("Car") && !isInvincible)
        {
            // Llama al método para perder una vida
            lifeManager.LoseLife();

            pitidoCoche.Play();

            // Inicia el tiempo de invencibilidad
            StartCoroutine(InvincibilityCoroutine());

        }
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            pedidoCompletado.Play();
            collision.gameObject.SetActive(false);
            gameEnding.SumaReparto();
            
        }
    }


    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true; // Activa la invencibilidad
        yield return new WaitForSeconds(invincibilityDuration); // Espera la duración definida
        isInvincible = false; // Desactiva la invencibilidad
    }
}
