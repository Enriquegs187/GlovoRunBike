using UnityEngine;

public class WindmillBladeRotation : MonoBehaviour
{
    [Header("Configuración de rotación")]
    [SerializeField] private float rotationSpeed = 100f; // Velocidad de rotación de las aspas

    private void Update()
    {
        // Rotar las aspas alrededor del eje Z a la velocidad especificada
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
