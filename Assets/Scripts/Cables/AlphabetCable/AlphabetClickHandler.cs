using UnityEngine;
using Cysharp.Threading.Tasks; // UniTaskの名前空間
using System.Threading;
using UnityEngine.EventSystems;

public class AlphabetClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private AlphabetInputHandler alphabetInputHandler;
    private LayerMask alphabetCableLayer; 
    // オブジェクトが破棄されたときに非同期処理を止めるためのトークン
    private CancellationToken cancellationToken;
    private bool isPointerDown = false;
    // ドラッグとクリックの閾値時間
    private float clickThresholdTime = 0.2f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cancellationToken = this.GetCancellationTokenOnDestroy();
        alphabetCableLayer = LayerMask.GetMask("Alphabet"); 
    }

    // Event Systemが提供する、オブジェクトがクリックされたときに呼び出されるメソッド
    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        // .Forget()で非同期処理を読んでも動作を止めずに実行する
        CheckClickOrDragAsync(cancellationToken).Forget();
    }
    // Event Systemが提供する、オブジェクトからクリックが離れたときに呼び出されるメソッド
    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
    }
    public async UniTaskVoid CheckClickOrDragAsync(CancellationToken token)
    {
        float timer = 0f;

        // クリックが終了するか、設定した時間を超えたらループを抜ける
        while (isPointerDown && timer < clickThresholdTime)
        {
            await UniTask.Yield(token);
            timer += Time.deltaTime;
        }
        if (!isPointerDown)
        {
            alphabetInputHandler.EnableInput(this.gameObject);
        }
    }
}
