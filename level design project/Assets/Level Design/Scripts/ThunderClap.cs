using UnityEngine;
using System.Collections;

public class ThunderClap : MonoBehaviour
{
    bool canFlicker = true;
    public AudioClip clip;

    void Update()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()

    {
        if (canFlicker)
        {
            canFlicker = false;
            GetComponent<AudioSource>().PlayOneShot(clip);
            GetComponent<Light>().enabled = true;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            GetComponent<Light>().enabled = false;
            yield return new WaitForSeconds(Random.Range(0.1f, 5));
            canFlicker = true;
        }
    }

}