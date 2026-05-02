# Alpha-set

## はじめに

- **Unity Version:** 6000.4.0f1
- **Platform:** PC (Windows/Mac)
- **Setup:**
  1. リポジトリをクローン
  2. Unity Hubからプロジェクトを追加
  3. [フリーアセットのインポート](#フリーアセットのインポート)を行う

## フリーアセットのインポート

1. [このサイト](https://ja.fonts2u.com/lemonmilk.フォント)からLemon/Milkフォントをダウンロード
2. ダウンロードしたフォントを`Assets/FreeAssets/Fonts/`に`LemonMilk.ttf`という名前でインポート
3. `Window` > `TextMeshPro` > `Font Asset Creator` を選択
4. 画像の設定でFont Assetを生成  
    ![Font Asset Creator 設定画像](https://github.com/user-attachments/assets/baf5cab4-cd0d-40cb-841a-989593992265)
5. `Assets/FreeAssets/FontAssets/`に`LemonMilk SDF.asset`という名前で保存

---

## 開発ルール

※小規模なのでREADMEに集約

### Unity作業の注意点

- **アルファベットケーブル新規作成**
  1. テンプレート(`Assets/Prefabs/AlphabetCables/AlphabetCableTemplate.prefab`)をヒエラルキー上の`StageTemplate/Circuit/AlphabetCables/AlphabetCableParent`の子オブジェクトとして配置
  2. テンプレートのPositionを(0, 0, 0)に設定
  3. AlphabetConnectionParts(コネクタの画像)とConnectionCheckArea(接続判定用のオブジェクト)を必要な分だけコピペし、配置する
  4. ConnectionCheckAreaのconnectedCableObjectsを設定する
     - そのコネクタと繋がっているコネクタのみをドラッグアンドドロップする
  5. `Canvas/Text (TMP)`のテキストを変更
  6. オブジェクトの名前をAlphabetCableTemplateから[英字]Cableに変更(例：ACable)
  7. Projectの`Assets/Prefabs/AlphabetCables/CablePrefabs/`に名前を変更した~Cableをドラッグアンドドロップし、`Prefab Variant`を選択
  8. Projectの`Assets/Resources/ScriptableObjects/AlphabetCableDataAsset.asset`の+を押して英字とプレハブを登録
  9. ゲームを開始し、英字を入力して正常に動作するかテスト
