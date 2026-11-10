using UnityEngine;
using UnityEngine.AI;

using UnityEngine;
using UnityEngine.AI;

public class VehicleMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    private Vector3 direction; // Dirección del movimiento
    private NavMeshHit hitInfo; // Para verificar si está en el NavMesh

    private void Start()
    {
        // Inicializa la dirección hacia adelante basada en la orientación inicial del vehículo
        direction = transform.forward;
    }

    void Update()
    {
        MoveVehicle();
        CheckNavMesh();
    }

    void MoveVehicle()
    {
        // Mueve el vehículo en la dirección especificada
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    void CheckNavMesh()
    {
        // Verifica si el vehículo está dentro del NavMesh
        if (!NavMesh.SamplePosition(transform.position, out hitInfo, 1f, NavMesh.AllAreas))
        {
            // Si el vehículo está fuera del NavMesh, gira 180 grados
            Debug.Log($"{gameObject.name} está fuera del NavMesh. Girando 180 grados.");
            TurnAround();
        }
    }

    void TurnAround()
    {
        // Gira el vehículo 180 grados visualmente
        transform.Rotate(0f, 180f, 0f);

        // Actualiza la dirección lógica de movimiento para que coincida con la nueva orientación
        direction = transform.forward;
    }
}
