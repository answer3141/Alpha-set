using UnityEngine;
using UnityEngine.InputSystem;

public class AlphabetInputHandler : MonoBehaviour
{
    [SerializeField]
    private AlphabetChanger alphabetChanger; // アルファベットを変更するためのクラス
    private bool isInputEnabled = false; // 入力を有効にするかどうかのフラグ
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

    public void EnableInput(GameObject targetObject)
    {
        isInputEnabled = true;
        alphabetChanger.StartTypingReady(targetObject); // 入力中用のプレハブに変更
    }
    private void DisableInput()
    {
        isInputEnabled = false;
    }

    private void OnTextInput(char inputChar)
    {
        if (!char.IsLetter(inputChar) || !isInputEnabled) return;
        char upperChar = char.ToUpper(inputChar);
        
        alphabetChanger.ChangeAlphabetCable(upperChar);
        DisableInput();
    }
}
