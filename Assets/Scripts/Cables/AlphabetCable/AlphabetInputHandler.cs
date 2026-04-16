using UnityEngine;
using UnityEngine.InputSystem;

public class AlphabetInputHandler : MonoBehaviour
{
    private void OnEnable() 
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        keyboard.onTextInput += OnTextInput;
    }

    private void OnDisable()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        keyboard.onTextInput -= OnTextInput;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTextInput(char inputChar)
    {
        if (inputChar == ' ') return;
        char upperChar = char.ToUpper(inputChar);
        
    }
}
