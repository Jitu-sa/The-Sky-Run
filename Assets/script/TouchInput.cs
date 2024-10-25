using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour
{
    private float swipeThreshold = 50f;
    private Vector2 startPos;
    private bool swipeInProgress = false;
    private AudioSource audioSource;
    private bool death;
    private bool fall;
    private bool hasplayed=false;

    public bool canroll = false;
    public bool canjump = false;
    public bool rightswipe = false;
    public bool leftswipe = false;
    public PlayerMovement playerMovement;

    public AudioClip jumpsound;
    public AudioClip rollsound;
    public AudioClip deathsound;
    public AudioClip fallsound;

    IEnumerator SetCanJump()
    {
        canjump = true;
        yield return new WaitForSeconds(0.1f);
        canjump = false;
    }

    IEnumerator SetCanRoll()
    {
        canroll = true;
        yield return new WaitForSeconds(0.7f);
        canroll = false;
    }

    IEnumerator ResetSwipeFlags(float delay)
    {
        yield return new WaitForSeconds(delay);
        rightswipe = false;
        leftswipe = false;
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        death = playerMovement.isdead;
        fall = playerMovement.isfall;

        if (death==true && hasplayed==false)
        {
            PlaySound(deathsound);
            hasplayed=true;
        }
        else if (fall==true && hasplayed == false)
        {
            PlaySound(fallsound);
            hasplayed = true;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
                swipeInProgress = true;
                rightswipe = false;
                leftswipe = false;
            }
            else if (touch.phase == TouchPhase.Moved && swipeInProgress)
            {
                Vector2 direction = touch.position - startPos;

                if (Mathf.Abs(direction.y) > swipeThreshold)
                {
                    if (direction.y > 0)
                    {
                        StartCoroutine(SetCanJump());
                        PlaySound(jumpsound);
                    }
                    else if (direction.y < 0)
                    {
                        StartCoroutine(SetCanRoll());
                        PlaySound(rollsound);
                    }
                }

                if (Mathf.Abs(direction.x) > 80)
                {
                    if (direction.x > 0 && !rightswipe)
                    {
                        rightswipe = true;
                        swipeInProgress = false;
                        StartCoroutine(ResetSwipeFlags(1f)); // Adjust the delay as needed
                    }
                    else if (direction.x < 0 && !leftswipe)
                    {
                        leftswipe = true;
                        swipeInProgress = false;
                        StartCoroutine(ResetSwipeFlags(1f)); // Adjust the delay as needed
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                swipeInProgress = false;
            }
        }
    }
}
