using UnityEngine;

public class Head_collison : MonoBehaviour
{
    public bool headcollide;
    private bool slide=false;
    public TouchInput script;

    void Start()
    {
        script = FindObjectOfType<TouchInput>();
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            headcollide=true;
        }
    }

    private void Update()
    {
        slide = script.canroll;
        if (slide==true)
        {
           
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            GetComponent<Collider>().enabled = true;
        }
    }
}
