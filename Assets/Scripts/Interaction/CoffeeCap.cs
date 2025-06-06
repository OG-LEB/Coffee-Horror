using UnityEngine;

public class CoffeeCap : Interactable
{
    public Transform holdPoint;
    private Rigidbody rb;
    private bool isHeld = false; 
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private float holdDistance;
    void FixedUpdate()
    {
        if (isHeld)
        {
            //Vector3 direction = (holdPoint.position - transform.position);
            //rb.MovePosition(transform.position + direction * 10f * Time.fixedDeltaTime); 

            ////////////
            //Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;

            //Vector3 direction = targetPosition - transform.position;
            //rb.MovePosition(transform.position + direction * 10f * Time.fixedDeltaTime);


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
        if (!isHeld)
        {
            isHeld = true;
            rb.useGravity = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            holdDistance = Vector3.Distance(Camera.main.transform.position, transform.position);

        }
        else if (isHeld)
        {
            isHeld = false;
            rb.useGravity = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            //CoffeeStepManager.Instance.ReverceStep();
        }
    }


    //public bool canBePickedUp = false;
    //public bool isHeld = false;

    //private Rigidbody rb;
    //private Camera cam;

    //public float holdDistance = 2f;
    //public float moveSpeed = 10f;

    //private void Start()
    //{
    //    cam = Camera.main;
    //    rb = GetComponent<Rigidbody>();
    //}

    //private void Update()
    //{
    //    if (isHeld)
    //    {
    //        Vector3 targetPos = cam.transform.position + cam.transform.forward * holdDistance;
    //        Vector3 dir = targetPos - transform.position;
    //        rb.velocity = dir * moveSpeed;
    //        rb.angularVelocity = Vector3.zero;

    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            Drop();
    //        }
    //    }
    //}
    //private void OnMouseDown()
    //{
    //    if (!isHeld && canBePickedUp)
    //    {
    //        PickUp();
    //    }
    //}
    //private void PickUp()
    //{
    //    isHeld = true;
    //    rb.useGravity = false;
    //    rb.freezeRotation = true;
    //}
    //private void Drop()
    //{
    //    isHeld = false;
    //    rb.useGravity = true;
    //    rb.freezeRotation = false;
    //}
    //public void EnablePickup()
    //{
    //    canBePickedUp = true;
    //}
    //public void DisablePickup()
    //{
    //    canBePickedUp = false;
    //}
}
