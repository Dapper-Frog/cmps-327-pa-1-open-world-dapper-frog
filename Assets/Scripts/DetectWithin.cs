using UnityEngine;

public class DetectWithin : MonoBehaviour
{
    public bool isWithin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isWithin = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player") { isWithin = false;  }
    }
}
