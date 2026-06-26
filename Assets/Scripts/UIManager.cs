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
                title1.text = "Long Password";
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
        switch (player.BattlePhase){
            case 1:
                switch (cardNo){
                    case 1:
                        player.IncreaseDefense(20f);
                        player.DecreaseAttackSpeed(20f);
                        break;

                    case 2:
                        //特定の敵に対して無敵
                        break;

                    case 3:
                        player.EnableRevive();
                        player.DecreaseMoveSpeed(30f);
                        break;
                }
                break;
            case 2:
                switch (cardNo){
                    case 1:
                        //ドローンの追加・頻度ランダム
                        break;

                    case 2://デメリットがまだ未実装
                        player.IncreaseAttackPower(1);
                        player.IncreaseAttackSpeed(50f);
                        //特定の敵にだけ攻撃が効かない・武器を追加するのか？
                        break;

                    case 3:
                        //遅延型の大技・狙いはどうやってつけるのか
                        break;
                }
                break;
            case 3:
                switch (cardNo){
                    case 1:
                        player.IncreaseAttackSpeed(20f);
                        player.IncreaseDefense(10f);
                        player.IncreaseMoveSpeed(10f);
                        //経験値獲得量増加・ここで増加しても意味があるのか
                        //バリアを四方に置いて一箇所破られる？
                        break;

                    case 2:
                        //プレイヤーと同じ攻撃を行う分身を配置できる
                        //パスキー進化していると全体的にステータス向上
                        break;

                    case 3:
                        //敵の密度によってステータス変動
                        //密度が少ないと移動速度上昇・多いと防御力、攻撃力上昇
                        break;
                }
                break;
        }
        player.BattlePhase++;
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