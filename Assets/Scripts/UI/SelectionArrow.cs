using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip sound;
    [SerializeField] private AudioClip interactSound;

    private Transform rect_;

    private int currentPos;
    
    private void Awake()
    {
        rect_ = GetComponent<RectTransform>();
    }
    
    private void OnEnable()
    {
        currentPos = 0;
        ChangePosition(0);
    }

    private void Update()
    {
        //* Change position of the arrow
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangePosition(-1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangePosition(1);
        }
        
        //* Interact with the current option
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Interact();
        }
    }

    private void ChangePosition(int _change)
    {
        currentPos += _change;

        if (_change != 0)
        {
            SoundManager.Instance.PlaySound(sound);
        }
        if (currentPos < 0)
        {
            currentPos = options.Length - 1;
        }
        else if (currentPos >= options.Length - 1)
        {
            currentPos = 0;
        }
        //* Assign the current position to the first option if the current position is greater than the number of options
        rect_.position = new Vector3(rect_.position.x, options[currentPos].position.y);
    }
    
    private void Interact()
    {
        SoundManager.Instance.PlaySound(interactSound);
        //* Interact with the current option
        options[currentPos].GetComponent<Button>().onClick.Invoke();
    }
}
