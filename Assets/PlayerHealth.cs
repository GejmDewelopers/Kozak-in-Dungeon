using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] [Range(0, 10)] int healthPoints=10;
    [SerializeField] HealthDisplay healthDisplay;
    [SerializeField] ParticleSystem deathFX;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color normalColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField] Color damageColor = new Color(1f, 1f, 1f, 0.1f);
    public PlayerState state;
    bool isVulnerable = true;
    private void Start()
    {
        state = PlayerState.Alive;
        healthDisplay.SetHealth(healthPoints);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 4 || !isVulnerable) return; // border // water
        StartCoroutine(IFrames());
        healthPoints--;
        healthDisplay.SetHealth(healthPoints);
        if (healthPoints <= 0)
        {
            state = PlayerState.Dead;
            ManageDeathParticles();
            GetComponent<SpriteRenderer>().enabled = false;
            Time.timeScale = 0.1f;
            Invoke("LoadFirstLevel", 0.2f);
        }
    }

    IEnumerator IFrames()
    {
        isVulnerable = false;
        for (int i = 0; i < 4; i++)
        {
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = normalColor;
            yield return new WaitForSeconds(0.1f);
        }
        isVulnerable = true;
    }

    private void ManageDeathParticles()
    {
        var deathParticles = Instantiate(deathFX, transform.position, Quaternion.identity);
        deathParticles.Play();
        Destroy(deathParticles.gameObject, 1f);
    }


    void LoadFirstLevel() //TODO: Change later for "Load death sscreen or smth"
    {
        SceneManager.LoadScene(0);
    }

    private void OnLevelWasLoaded(int level)
    {
        Time.timeScale = 1f;
    }
}
