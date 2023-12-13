using UnityEngine;
using UnityEngine.UI;

public class TouchControls : MonoBehaviour
{
    public UIButtons word;
    public LevelGenerator level;
    public string letter;
    public LayerMask touchLayers;

    public Color32 buttonColor;
    
    RaycastHit2D hitInfo;

    private void Awake()
    {
        Application.targetFrameRate = 45;
    }

    public void Update()
    {
#if UNITY_IOS || UNITY_ANDROID
        TouchControlMobile();
#endif
#if UNITY_STANDALONE
        TouchControlsPC();
#endif
    }

    private void TouchControlsPC()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast(position, Vector2.zero, 10f, touchLayers);

            if (hitInfo.collider != null && hitInfo.transform.gameObject.GetComponent<CheckTouch>().isTouched == false)
            {
                hitInfo.transform.gameObject.GetComponent<Image>().color = buttonColor;
                hitInfo.transform.gameObject.GetComponent<CheckTouch>().isTouched = true;
                letter = hitInfo.transform.gameObject.GetComponentInChildren<Text>().text;
                level.placeholder.text += letter;
                FindObjectOfType<AudioManager>().Play("button");
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            word.CheckWord();

            for (int i = 0; i < level.LetterObjects.Length; i++)
            {
                level.LetterObjects[i].GetComponent<CheckTouch>().isTouched = false;
                level.LetterObjects[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    private void TouchControlMobile()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 position = Camera.main.ScreenToWorldPoint(touch.position);
                hitInfo = Physics2D.Raycast(position, Vector2.zero, 10f, touchLayers);
                if (hitInfo.collider != null && hitInfo.transform.gameObject.GetComponent<CheckTouch>().isTouched == false)
                {
                    hitInfo.transform.gameObject.GetComponent<Image>().color = buttonColor;
                    hitInfo.transform.gameObject.GetComponent<CheckTouch>().isTouched = true;
                    letter = hitInfo.transform.gameObject.GetComponentInChildren<Text>().text;
                    level.placeholder.text += letter;
                    FindObjectOfType<AudioManager>().Play("button");
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                word.CheckWord();

                for (int i = 0; i < level.LetterObjects.Length; i++)
                {
                    level.LetterObjects[i].GetComponent<CheckTouch>().isTouched = false;
                    level.LetterObjects[i].GetComponent<Image>().color = Color.white;
                }
            }
        }
    }
}