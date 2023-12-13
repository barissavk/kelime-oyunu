using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public Image muteImage;

    public Sprite spriteMute;
    public Sprite spriteUnmute;

    public bool muted = false;

    private void Start()
    {
        if (muted == false)
        {
            muteImage.GetComponent<Image>().sprite = spriteMute;
        }
        else if (muted == true)
        {
            muteImage.GetComponent<Image>().sprite = spriteUnmute;
        }
    }

    public void MuteSound()
    {
        FindObjectOfType<AudioManager>().Play("button");

        if (muted == false)
        {
            FindObjectOfType<AudioManager>().StopPlaying("theme");
            muteImage.GetComponent<Image>().sprite = spriteUnmute;
            muted = true;
        }
        else if (muted == true)
        {
            FindObjectOfType<AudioManager>().Play("theme");
            muteImage.GetComponent<Image>().sprite = spriteMute;
            muted = false;
        }
    }
}
