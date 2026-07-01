# 🔐 Security Survivor

> パスキー認証の仕組みとメリットを、遊びながら学べる2Dサバイバーゲーム

---

## 📖 プロジェクト概要

**Security Survivor** は、VampireSurvivors を参考にした2Dサバイバーゲームのプロトタイプです。

### 🎯 ターゲット

- パスキー認証をすでに使っているが、その仕組みやメリットをよく知らない人

### 🎮 コンセプト

押し寄せるサイバー攻撃の波を生き残りながら、**パスキー・パスワード・多要素認証**など各種認証方式の特徴やメリットを自然に学べるゲームを目指しています。

### 🛠 技術スタック

| 項目 | 内容 |
|------|------|
| エンジン | Unity（URP） |
| 言語 | C# |
| フォント | Noto Sans JP |
| 入力システム | Unity Input System |

---

## 📁 ディレクトリ構造

```
prototype/
├── Assets/
│   ├── Prefabs/          # ゲームオブジェクトのPrefab
│   │   ├── Bat
│   │   ├── Bullet
│   │   ├── Exp
│   │   ├── Drone
│   │   ├── DelayBomb
│   │   ├── Explosion
│   │   └── Zombie
│   ├── Scenes/           # シーンファイル
│   ├── Scripts/          # C#スクリプト
│   │   ├── Bullet.cs         # 弾の挙動（移動・衝突処理）
│   │   ├── Enemy.cs          # 敵のAI・ダメージ処理
│   │   ├── Experience.cs     # 経験値アイテムの取得・管理
│   │   ├── GameManager.cs    # ゲーム全体の進行・状態管理
│   │   ├── PlayerController.cs # プレイヤーの入力・移動制御
│   │   ├── DroneWeapon.cs    # ドローンの管理
│   │   ├── DelayBomb.cs      # 爆弾の落下エフェクト・着弾位置・落下表示
│   │   ├── Explosion.cs      # 爆発のダメージ・エフェクト・範囲判定
│   │   └── UIManager.cs 
│   ├── Settings/         # URPなどのRender設定
│   └── TextMesh Pro/     # フォント・UIテキスト関連
├── Packages/             # Unityパッケージ設定
├── ProjectSettings/      # Unityプロジェクト設定
└── prototype.slnx        # Visual Studioソリューションファイル
```

---

## 🚀 セットアップ

1. **リポジトリをクローン**

   ```bash
   git clone <repository-url>
   cd prototype
   ```

2. **Unity でプロジェクトを開く**

   Unity Hub から `prototype/` フォルダを開いてください。

3. **シーンを開く**

   `Assets/Scenes/` 内のシーンファイルを開き、再生ボタンで動作確認できます。

---
