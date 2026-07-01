using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProを使うために必要

public class CharacterSelectManager : MonoBehaviour
{
    [Header("UIへの割り当て")]
    public Image characterImageDisplay; // キャラの画像を表示するImage
    public TextMeshProUGUI nameText;    // キャラ名を表示するテキスト
    public TextMeshProUGUI descriptionText; // 説明を表示するテキスト

    [System.Serializable]
    public struct CharacterData
    {
        public Sprite characterSprite;
        public string characterName;
        [TextArea(3, 5)]
        public string description;
    }

    [Header("キャラクターの設定データ")] // ← 変数の直前に移動！
    public CharacterData[] characters;

    private int currentIndex = 0; // 現在選択されているキャラの番号 (0からスタート)

    void Start()
    {
        // 最初に0番目のキャラを表示する
        UpdateUI();
    }

    // [>] ボタンを押した時に呼ぶ関数
    public void NextCharacter()
    {
        currentIndex++;
        // もし最後のキャラを超えたら、最初のキャラ(0番)に戻る
        if (currentIndex >= characters.Length)
        {
            currentIndex = 0;
        }
        UpdateUI();
    }

    // [<] ボタンを押した時に呼ぶ関数
    public void PrevCharacter()
    {
        currentIndex--;
        // もし最初のキャラより前に行こうとしたら、最後のキャラに戻る
        if (currentIndex < 0)
        {
            currentIndex = characters.Length - 1;
        }
        UpdateUI();
    }

    // 画面の表示を更新する関数
    private void UpdateUI()
    {
        // 現在の番号のデータを使って、画像とテキストを書き換える
        characterImageDisplay.sprite = characters[currentIndex].characterSprite;
        nameText.text = characters[currentIndex].characterName;
        descriptionText.text = characters[currentIndex].description;
    }

    // [決定] ボタンを押した時に呼ぶ関数
    public void ConfirmSelection()
    {
        // ここに決定時の処理を書く (例: 次のシーンへ移動など)
        Debug.Log(characters[currentIndex].characterName + " が選ばれました！");
        
        // 選択されたキャラの番号を保存して、ゲーム本編のシーンに渡す際によく使う方法
        // PlayerPrefs.SetInt("SelectedCharacterID", currentIndex);
        // SceneManager.LoadScene("MainGameScene");
    }
}