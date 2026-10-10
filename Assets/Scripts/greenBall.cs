using TMPro;
using UnityEngine;

public class greenBall : MonoBehaviour
{

    public int collect = 0;
    public bool win = false;
    public TextMeshProUGUI score;
    public PlayerMovement controller;
    public CanvasGroup main;
    public CanvasGroup winScreen;
    public ParticleSystem sparkle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(collect == 6)
        {
            win = true;
            controller.enabled = false;
            main.alpha = 0f;
            winScreen.alpha = 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "collectable")
        {
            ParticleSystem spark = Instantiate(sparkle, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            spark.Play();
            collect += 1;
            score.text = collect.ToString();
            Destroy(spark.gameObject, spark.main.duration + spark.main.startLifetime.constantMax);
        }
    }
}
