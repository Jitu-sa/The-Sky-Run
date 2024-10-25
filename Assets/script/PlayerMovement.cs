using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private float runspeed=20;
    private float jumpspeed =90;
    private bool feetcollison;
    private bool headcollison;
    private int score = 0;
    private int highscore = 0;
    private bool roll;
    private bool jump;
    public bool Rplatform=false;
    private int targetangle;
    private float move;
    public bool hasswipe=false;
    private int sensitivity;
    private int assumeangle;
    private float angle;
    private AudioSource audioSource;


    public Head_collison Head_collison;
    public TouchInput TOUCHINPUT;
    public Rigidbody rb;
    public TextMeshProUGUI scoretext;
    public AudioClip coinsound;
    public bool isdead=false;
    public bool isfall=false;

    void Start()
    {
        Head_collison = FindObjectOfType<Head_collison>();
        TOUCHINPUT= FindObjectOfType<TouchInput>();
        audioSource = GetComponent<AudioSource>();
        sensitivity = PlayerPrefs.GetInt("sensitivity",30);
        audioSource.clip = coinsound;
    }

    // Update is called once per frame
    void Update()
    {
        scoretext.text = score.ToString();
        headcollison = Head_collison.headcollide;
        roll=TOUCHINPUT.canroll; 
        jump=TOUCHINPUT.canjump;
        angle = gameObject.transform.rotation.eulerAngles.y;

        transform.Translate(Vector3.forward * Time.deltaTime * runspeed);

        if (60 < angle && 120 > angle)
        {
            assumeangle = 90;
        }
        else if (150 < angle && 210 > angle)
        {
            assumeangle = 180;
        }
        else if (240 < angle && 300 > angle)
        {
            assumeangle = 270;
        }
        else if (-30 < angle && 30 > angle || 330 < angle && 390 > angle)
        {
            assumeangle = 0;
        }
        //moving game object left and right %%%%%%%%%%%%%%%%%%%
        Vector3 acceleration = Input.acceleration;
        move = acceleration.x * sensitivity;
        transform.Translate(new Vector3(move, 0, 0) * Time.deltaTime);

        //#######jump animation
        if (transform.position.y >2.5f)
        {
            GetComponent<Animator>().SetBool("isjumping", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isjumping", false);
        }

        //code for slide animation##################################################################
        if (roll==true)
        {
            GetComponent<Animator>().SetBool("isrolling", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isrolling", false);
        }

        //death animations $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        if(feetcollison==true && headcollison==false)
        {
            GetComponent<Animator>().Play("Death_feet");
            if (runspeed > 0.1)
            {
                runspeed = runspeed - 7.5f * Time.deltaTime;
            }
            jumpspeed = 0;sensitivity = 0;
            savefinalscore();
            StartCoroutine(DelayAndLoadScene());
            isdead = true;
        }

        if (headcollison == true && feetcollison==false)
        {
            GetComponent<Animator>().Play("Death");
            runspeed = 0;jumpspeed = 0;sensitivity=0;
            savefinalscore();
            StartCoroutine(DelayAndLoadScene());
            isdead = true;
        }

        if (feetcollison == true && headcollison==true)
        {
            GetComponent<Animator>().Play("Death_body");
            runspeed = 0; jumpspeed = 0;sensitivity = 0;
            savefinalscore();
            StartCoroutine(DelayAndLoadScene());
            isdead = true;
        }

        //code for fall from platform$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        if (transform.position.y<0)
        {
            GetComponent<Animator>().Play("FallingLoop");
            runspeed = 0; jumpspeed = 0;sensitivity=0;
            savefinalscore();
            StartCoroutine(DelayAndLoadScene());
            isfall = true;

        }

    }

    void FixedUpdate()
    {
        //code for jump#################################################################
        if (jump==true && transform.position.y > 1.5 && transform.position.y < 2.5 )
        {
            rb.AddForce(Vector3.up * jumpspeed);
        }
        else if (transform.position.y > 4)
        {
            rb.AddForce(Vector3.down * 10);
        }

        //swipe left and swipe right############################
        if (TOUCHINPUT.rightswipe == true && Rplatform == true && hasswipe == false)
        {
            targetangle = assumeangle + 90;
            transform.rotation = Quaternion.Euler(0, targetangle, 0);
            hasswipe = true;
        }
        else if (TOUCHINPUT.leftswipe == true && Rplatform == true && hasswipe == false)
        {
            targetangle = assumeangle - 90;
            transform.rotation = Quaternion.Euler(0, targetangle, 0);
            hasswipe = true;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            feetcollison = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            score += 1;
            audioSource.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "rotate_platform")
        {
            Rplatform = true;
        }
        else
        {
            Rplatform = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "rotate_platform")
        {
            hasswipe = false;
        }
    }

    private void savefinalscore()
    {
        PlayerPrefs.SetInt("finalscore", score);
        if (score > PlayerPrefs.GetInt("highscore", 0))
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
        }
    }

    IEnumerator DelayAndLoadScene()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(3);
    }

}
