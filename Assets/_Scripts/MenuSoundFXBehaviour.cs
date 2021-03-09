using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundFXBehaviour : MonoBehaviour
{
    // Declarations for soundfx
    [Header ("Menu SoundFX")]
    public AudioSource onHoverSound;
    public AudioSource onClickSound;

    // Hovering mouse event
    void OnMouseOver() {
        onHoverSound.Play();
    }

    // Primary click event
    void OnMouseDown() {
        onClickSound.Play();
    }
}
