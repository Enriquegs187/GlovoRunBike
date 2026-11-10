using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Método para iniciar el juego
    public void PlayGame()
    {
        // Carga la escena del juego (asegúrate de que el nombre coincide)
        SceneManager.LoadScene("SimplePoly City - Low Poly Assets/Demo/SimplePoly City - Low Poly Assets_Demo Scene");
    }

    // Método para salir del juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego..."); // Solo visible en el editor
        Application.Quit(); // Funciona en una build del juego
    }
}
