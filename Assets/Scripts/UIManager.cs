using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject titlePanel;
    public GameObject gameHUDPanel;
    public GameObject levelUpPanel;
    public TextMeshProUGUI title1;
    public TextMeshProUGUI title2;
    public TextMeshProUGUI title3;
    public Image icon1;
    public Image icon2;
    public Image icon3;
    public TextMeshProUGUI description1;
    public TextMeshProUGUI description2;
    public TextMeshProUGUI description3;
    public GameObject resultPanel;
    public Sprite testSprite;
    public PlayerController player;

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
    public void ShowLevelUp(int phase)
    {
        levelUpPanel.SetActive(true);
        SetupCards(phase);
        Time.timeScale = 0; // 選択中は制限時間を止める 
    }

    private void SetupCards(int phase)
    {
        switch (phase)
        {
            case 1:
                title1.text = "test1";
                description1.text = "test_1test_1test_1test_1test_1test_1";
                icon1.sprite = testSprite;

                title2.text = "test2";
                description2.text = "test_2test_2test_2test_2test_2test_2";
                //icon2.sprite = hpSprite;

                title3.text = "test3";
                description3.text = "test_3test_3test_3test_3test_3test_3";
                //icon3.sprite = speedSprite;
                break;
            case 2:
                title1.text = "test4";
                description1.text = "test_1test_1test_1test_1test_1test_1";
                icon1.sprite = testSprite;

                title2.text = "test5";
                description2.text = "test_2test_2test_2test_2test_2test_2";
                //icon2.sprite = hpSprite;

                title3.text = "test6";
                description3.text = "test_3test_3test_3test_3test_3test_3";
                //icon3.sprite = speedSprite;
                break;

            case 3:
                title1.text = "test7";
                description1.text = "test_1test_1test_1test_1test_1test_1";
                icon1.sprite = testSprite;

                title2.text = "test8";
                description2.text = "test_2test_2test_2test_2test_2test_2";
                //icon2.sprite = hpSprite;

                title3.text = "test9";
                description3.text = "test_3test_3test_3test_3test_3test_3";
                //icon3.sprite = speedSprite;
                break;
        }
    }

    // 強化カードを選んだ時の処理（カードのボタンから呼ばれる）
    public void SelectSkillCard(int cardNo)
    {
        switch (cardNo)
        {
            case 1:
                player.IncreaseDefense(20f);
                break;

            case 2:
                player.IncreaseAttackSpeed(50f);
                break;

            case 3:
                // Card3の効果
                break;
        }

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