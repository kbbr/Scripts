# Todo

あまりにもREADMEが微妙なので、しばらくはTODOリストに

- GameSystemでのオブジェクト初期化
  - オブジェクトはインスタンス化 -> スクリプトAddComponent
    - インスタンスを管理するManager的スクリプトを作成
  - UIオブジェクト
    - PlayerHPの更新メソッドへのアクセス方法が
    いちいちSerializedFieldやPublicやらFindやらするのが煩雑
  - 
- PlayerManagerの導入
  - Playerキャラの変更に備えて
  - 現状は、Playerのスクリプトにあらゆる処理や変数がある
  - 他クラスからのアクセスもしたいので修正？
  - これもインスタンス生成 -> アタッチ