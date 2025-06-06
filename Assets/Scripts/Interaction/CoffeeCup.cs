using UnityEngine;

public class CoffeeCup : Interactable
{
    public Transform holdPoint; 
    private Rigidbody rb;
    private bool isHeld = false;
    [Header("Visual")]
    [SerializeField] private GameObject coffeeLiquid;
    [SerializeField] private GameObject Cap;
    [SerializeField] private GameObject CapTrigger;
    private bool isCoffeReady;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        coffeeLiquid.SetActive(false);
        Cap.SetActive(false);
        CapTrigger.SetActive(false);
        isCoffeReady = false;
    }

    //private Vector3 targetPosition;
    private float holdDistance;

    void FixedUpdate()
    {
        if (isHeld)
        {
            //Vector3 direction = (holdPoint.position - transform.position);
            //rb.MovePosition(transform.position + direction * 10f * Time.fixedDeltaTime); // можно подкрутить скорость

            //////////
            //Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;

            //Vector3 direction = targetPosition - transform.position;
            //rb.MovePosition(transform.position + direction * 10f * Time.fixedDeltaTime); 
            //rb.freezeRotation = true;

            float maxHoldDistance = 1.0f; 

            float currentHoldDistance = Mathf.Min(holdDistance, maxHoldDistance);

            Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * currentHoldDistance;

            Vector3 direction = targetPosition - transform.position;
            rb.MovePosition(transform.position + direction * 10f * Time.fixedDeltaTime);
            rb.freezeRotation = true;
        }
        else
        {
            rb.freezeRotation = false;
        }
    }

    public override void Interact()
    {
        if ((CoffeeStepManager.Instance.IsCurrentStep(CoffeeStepManager.Step.GrabCup) || (CoffeeStepManager.Instance.IsCurrentStep(CoffeeStepManager.Step.ServeToNPC)) && !isHeld))
        {
            isHeld = true;
            rb.useGravity = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            holdDistance = Vector3.Distance(Camera.main.transform.position, transform.position);

            if (CoffeeStepManager.Instance.IsCurrentStep(CoffeeStepManager.Step.GrabCup))
                CoffeeStepManager.Instance.AdvanceStep();
        }
        else if (isHeld)
        {
            isHeld = false;
            rb.useGravity = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            if (CoffeeStepManager.Instance.IsCurrentStep(CoffeeStepManager.Step.PlaceCup))
                CoffeeStepManager.Instance.ReverceStep();

        }
    }

    public void AutoPlace(Transform targetPoint)
    {
        isHeld = false;

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        transform.position = targetPoint.position;
        transform.rotation = targetPoint.rotation;

        CoffeeStepManager.Instance.AdvanceStep();
    }

    public void UpdateLiquidInCup(float progress)
    {
        if (!coffeeLiquid.activeSelf) coffeeLiquid.SetActive(true);

        Vector3 liquidObjectScale = new Vector3(0.75f + (0.25f * progress), 0.75f + (0.25f * progress), progress);
        coffeeLiquid.transform.localScale = liquidObjectScale;
    }
    public void EnableCap()
    {
        Cap.SetActive(true);
        CapTrigger.SetActive(false);

        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = false;
        isCoffeReady = true;
    }
    public void SetCapTrigger(bool isActive)
    {
        CapTrigger.SetActive(isActive);
    }
    public bool IsCoffeeReady() 
    {
        return isCoffeReady;
    }
}
