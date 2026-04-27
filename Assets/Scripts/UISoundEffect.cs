using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public GameObject audioObject;

    public void onPlayButtonClicked()
    {
        audioObject.GetComponent<AudioSource>().Play();
    }
}