using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }
    public void PlayButtonClickSound()
    {
        SoundEffects.Instance.audioSource.PlayOneShot(SoundEffects.Instance.buttonClick);
    }
    public void NewGame()
    {
        SaveSystem.DeleteAllSavings();
    }
}