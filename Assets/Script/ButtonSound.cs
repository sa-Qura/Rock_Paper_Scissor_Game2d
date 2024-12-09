using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown() {
        // play sound when image is clicked
        if (audioSource != null) {
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
