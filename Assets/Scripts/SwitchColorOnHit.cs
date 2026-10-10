using UnityEngine;

public class SwitchColorOnHit : MonoBehaviour
{
    public Color color;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Renderer>().material.color = color;
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Renderer>().material.color = Color.gray2;
    }
}
