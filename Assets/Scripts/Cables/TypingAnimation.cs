using UnityEngine;
using DG.Tweening; // DOTweenを使用するための名前空間
using TMPro;

public class TypingAnimation : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text text;

    private void OnEnable()
    {
        text.alpha = 1;
        text.DOFade(0f, 1f)
            .SetEase(Ease.InOutCubic)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
