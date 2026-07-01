using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject titlePanel;
    public GameObject gameHUDPanel;
    public GameObject levelUpPanel;
    public GameObject resultPanel;

    [Header("UI Elements")]
    public TextMeshProUGUI resultText;

    void Start()
    {
        ShowTitle(); // 起動時はタイトル画面を表示
    }

    // タイトル画面を表示
    public void ShowTitle()
    {
        titlePanel.SetActive(true);
        gameHUDPanel.SetActive(false);
        levelUpPanel.SetActive(false);
        resultPanel.SetActive(false);
        Time.timeScale = 0; // タイトル画面ではゲーム内時間を止める
    }

    // ゲーム開始（スタートボタンから呼ばれる）
    public void StartGame()
    {
        titlePanel.SetActive(false);
        gameHUDPanel.SetActive(true);
        Time.timeScale = 1; // ゲーム時間を動かす
        
        // ※本来はここでプレイヤーのHPやタイマー、敵の全削除などの初期化処理を呼び出します
    }

    // レベルアップ画面を表示
    public void ShowLevelUp()
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0; // 選択中は制限時間を止める 
    }

    // 強化カードを選んだ時の処理（カードのボタンから呼ばれる）
    public void SelectSkillCard()
    {
        // ※今回はどのボタンを押しても一旦画面を閉じるだけの実装
        levelUpPanel.SetActive(false);
        Time.timeScale = 1;
    }

    // クリアまたは失敗画面を表示
    public void ShowResult(bool isClear)
    {
        resultPanel.SetActive(true);
        gameHUDPanel.SetActive(false);
        Time.timeScale = 0;

        if (isClear) {
        // クリア画面と分かるような文字 
            resultText.text = "Congratulation\nアカウントを保護しました"; 
        } else {
        // 失敗とわかるような文字 
            resultText.text = "GAME OVER\nあなたの情報が漏洩しました"; 
        }
    }
}