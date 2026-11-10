using UnityEngine;

public class BicycleController : MonoBehaviour
{
    [Header("Referencias de la bicicleta")]
    [SerializeField] private Transform frontWheel; // Rueda delantera
    [SerializeField] private Transform handlebar;  // Manillar

    [Header("Configuración de movimiento")]
    [SerializeField] private float maxForwardSpeed = 5f;     // Velocidad máxima hacia adelante
    [SerializeField] private float backwardSpeed = 2f;      // Velocidad hacia atrás (más lenta)
    [SerializeField] private float turnSpeed = 60f;         // Velocidad de giro de la bicicleta
    [SerializeField] private float maxWheelAngle = 45f;     // Ángulo máximo de giro de la rueda
    [SerializeField] private float wheelTurnSpeed = 45f;    // Reducida para giros más suaves
    [SerializeField] private float leanAngle = 15f;         // Ángulo de inclinación al girar
    [SerializeField] private float steeringSmoothing = 3f;  // Factor de suavizado para el giro
    [SerializeField] private float accelerationRate = 2f;   // Tasa de aceleración gradual
    [SerializeField] private float decelerationRate = 1f;   // Tasa de desaceleración gradual

    private float currentWheelAngle = 0f;
    private float targetWheelAngle = 0f;
    private float currentLeanAngle = 0f;
    private float currentSpeed = 0f;  // Velocidad actual de la bicicleta
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Congelar movimientos en el eje Y y rotaciones en X y Z
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.interpolation = RigidbodyInterpolation.None;
    }

    private void Update()
    {
        HandleSteering();
        UpdateWheelRotation();
        MoveBicycle();
        HandleLeaning();
    }

    private void HandleSteering()
    {
        float steeringInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            steeringInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            steeringInput = 1f;
        }

        float targetAngle = steeringInput * maxWheelAngle;
        targetWheelAngle = Mathf.Lerp(targetWheelAngle, targetAngle, Time.deltaTime * steeringSmoothing);
    }

    private void UpdateWheelRotation()
    {
        currentWheelAngle = Mathf.Lerp(currentWheelAngle, targetWheelAngle, Time.deltaTime * wheelTurnSpeed);

        if (frontWheel != null)
        {
            frontWheel.localRotation = Quaternion.Euler(0f, currentWheelAngle, 90f);
        }

        if (handlebar != null)
        {
            handlebar.localRotation = Quaternion.Euler(0f, currentWheelAngle, 0f);
        }
    }

    private void MoveBicycle()
    {
        Vector3 moveDirection;

        if (Input.GetKey(KeyCode.W))
        {
            // Acelera hacia adelante
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxForwardSpeed, accelerationRate * Time.deltaTime);
            moveDirection = -transform.right;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Acelera hacia atrás
            currentSpeed = Mathf.MoveTowards(currentSpeed, -backwardSpeed, accelerationRate * Time.deltaTime);
            moveDirection = transform.right;
        }
        else
        {
            // Desacelera gradualmente
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, decelerationRate * Time.deltaTime);
            moveDirection = currentSpeed > 0 ? -transform.right : transform.right;
        }

        // Limitar el movimiento al plano horizontal (ejes X y Z)
        moveDirection.y = 0;

        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            if (Mathf.Abs(currentWheelAngle) > 0.1f)
            {
                float turnAmount = currentWheelAngle * turnSpeed * Time.deltaTime;
                float turnDirection = currentSpeed < 0 ? -1f : 1f;
                transform.Rotate(Vector3.up, turnAmount * Mathf.Sign(currentSpeed) * turnDirection);
            }

            rb.MovePosition(rb.position + moveDirection * Mathf.Abs(currentSpeed) * Time.deltaTime);
        }
    }

    private void HandleLeaning()
    {
        float targetLean = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            float leanDirection = Input.GetKey(KeyCode.S) ? -1f : 1f;
            targetLean = currentWheelAngle * (leanAngle / maxWheelAngle) * leanDirection;
        }

        currentLeanAngle = Mathf.Lerp(currentLeanAngle, targetLean, Time.deltaTime * 5f);

        // Mantener la rotación en el plano horizontal, solo aplicando inclinación en X
        transform.localRotation = Quaternion.Euler(currentLeanAngle, transform.eulerAngles.y, 0f);
    }

    public void SetupReferences(Transform wheelTransform, Transform handlebarTransform)
    {
        frontWheel = wheelTransform;
        handlebar = handlebarTransform;
    }
}
