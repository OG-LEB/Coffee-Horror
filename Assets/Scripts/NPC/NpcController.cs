using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    public Transform player;
    public float triggerDistance = 5f;
    //public float waitTime = 2f;
    public float speed;

    private NavMeshAgent agent;
    [SerializeField] private Transform startPosition;
    private bool isReturning = false;
    private bool isFollowing = false;
    private bool isCoffeOrdered = false;
    //private bool isWatchingPlayer = false;

    private Animator animator;
    private NpcFootsteps footstepsScript;

    [Header("GameOver")]
    [SerializeField] private GameObject doorOpened, doorClosed, trigger0, trigger1;

    [SerializeField] private GameObject MoneyText;
    [SerializeField] private MainSceneManager mainSceneManager;
    [SerializeField] private AudioSource Screamer;
    [SerializeField] private GameObject NcpModel, NpcScaryModel, ModelsBehindWindow;
    [SerializeField] private GameObject ScaryEffects;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        animator = GetComponent<Animator>();
        footstepsScript = GetComponent<NpcFootsteps>();
    }

    private void Update()
    {


        if (isFollowing || isReturning) return;

        if (isCoffeOrdered)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f;

            if (direction.magnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
            }

            return;
        }


        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= triggerDistance)
        {
            StartCoroutine(FollowThenReturn());
        }
    }

    private IEnumerator FollowThenReturn()
    {
        isFollowing = true;
        animator.SetBool("isWalking", true);
        footstepsScript.StartFootsteps();

        while (Vector3.Distance(transform.position, player.position) > 2f)
        {
            agent.SetDestination(player.position);
            yield return null;
        }

        agent.ResetPath();
        animator.SetBool("isWalking", false);
        footstepsScript.StopFootsteps();


        NotificationSystem.Instance.ShowMessage("Привет. Они говорили, что ты придёшь. Ты ведь умеешь готовить кофе, верно?.. Сделай один для меня. Мне нужно немного тепла.", 5f);

        yield return new WaitForSeconds(6f);

        CoffeeStepManager.Instance.AdvanceStep();

        isCoffeOrdered = true;
        isFollowing = false;
        isReturning = true;
        animator.SetBool("isWalking", true);
        footstepsScript.StartFootsteps();


        agent.SetDestination(startPosition.position);

        while (Vector3.Distance(transform.position, startPosition.position) > 0.5f)
        {
            yield return null;
        }

        isReturning = false;
        animator.SetBool("isWalking", false);
        footstepsScript.StopFootsteps();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (CoffeeStepManager.Instance.IsCurrentStep(CoffeeStepManager.Step.ServeToNPC) && other.CompareTag("CoffeeCup"))
        {
            if (other.GetComponent<CoffeeCup>().IsCoffeeReady())
            {
                NotificationSystem.Instance.ShowMessage("Спасибо за кофе! Держи немного денег за работу.", 2f);
                Instantiate(MoneyText, other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                StartCoroutine(GameEnd());
            }
            else
            {
                NotificationSystem.Instance.ShowMessage("Мне нужен кофе, а не пустой стаканчик!", 2f);
            }
        }
    }

    private IEnumerator GameEnd() 
    {
        trigger0.SetActive(false);
        trigger1.SetActive(false);
        doorOpened.SetActive(false);
        doorClosed.SetActive(true);

        yield return new WaitForSeconds(3f);

        NotificationSystem.Instance.ShowMessage("Кофе… тёплый, как воспоминание.", 2f);
        yield return new WaitForSeconds(3f);

        NotificationSystem.Instance.ShowMessage("Не смотри на дверь, она уже заперта", 2f);
        yield return new WaitForSeconds(3f);

        mainSceneManager.SwitchToDarkCameraEffects();
        trigger0.SetActive(false);
        trigger1.SetActive(false);

        NotificationSystem.Instance.ShowMessage("Теперь ты останешься с нами.", 2f);
        yield return new WaitForSeconds(3f);

        //Вот тут пиздец
        Screamer.Play();
        NcpModel.SetActive(false);
        NpcScaryModel.SetActive(true);
        ModelsBehindWindow.SetActive(true);
        ScaryEffects.SetActive(true);
        FadeToBlack(3f);
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(0);
    }

    //Fade
    public Image fadeImage;

    public void FadeToBlack(float duration)
    {
        StartCoroutine(FadeCoroutine(duration));
    }

    private IEnumerator FadeCoroutine(float duration)
    {
        float time = 0f;
        Color startColor = fadeImage.color;
        Color endColor = new Color(0, 0, 0, 1); // Полностью черный

        while (time < duration)
        {
            fadeImage.color = Color.Lerp(startColor, endColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = endColor;
    }
}
