using UnityEngine;
using UnityEngine.SceneManagement;

public class RisingLava : MonoBehaviour
{
    //lava rises faster in the end/parkour section, which is beyond z = -80
    public float triggerZPos = -80.0f;
    public float normalSpeed = 0.2f;
    public float fastSpeed = 0.4f;
    public float waitAfterDeath = 2.0f;

    private GameObject deathText;
    private GameObject player;

    void Start()
    {
        deathText = GameObject.Find("Death Text");
        player = GameObject.Find("Player");
        deathText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z > triggerZPos)
            transform.Translate(0, normalSpeed*Time.deltaTime, 0);
        else
            transform.Translate(0, fastSpeed*Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.transform.tag == "Player")
            deathText.SetActive(true);
            player.SetActive(false);
            Invoke("Respawn", waitAfterDeath);
    }

    private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}