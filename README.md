# Aim

- AimRotateOrigin
  - MainAim
  - scope
    - targetCursor

## AimRotate.cs
Aimの回転制御

## MainAimFollow.cs
Aimのposition制御


# Camera

- CameraRotateOrigin
  - CameraPosition

## CameraDelayFollow.cs
CameraRotateOriginをプレイヤーに追従させる制御

## CameraRotate.cs
カメラの回転制御

## MainCameraFollow.cs
カメラのカメラポジションへの追従制御



# Enemy

## Enemy.cs
- ターゲットカーソル表示
- バリアーの当たり判定



# Player

## unitychanMove.cs
- SDunitychanの移動・ジャンプ・上昇
- ジャンプアニメーション
- ジャンプディフェンス
 -- ジャンプディフェンスアニメーション

## jumpDefenceEvents.cs
 - ジャンプディフェンスのアニメーションイベント
   - ジャンプディフェンスのアニメーションのトリガーが何故かオンになり続けるため

## PlayerGuard.cs
- プレイヤーのガード制御

## PlayerRotate.cs
プレイヤーの回転制御

## PlayerShot.cs
プレイヤーの遠距離攻撃制御

## ParticleCollision.cs
ジャンプガードのコリジョン制御

## ShotPlayer.cs
プレイヤーからショットされた弾の制御

## EffectGenerate.cs


# GameManager

## TimeManager.cs
- ヒットストップ（スロウ）


## ParticleAutoDestroy.cs
- パーティクルの時間経過で自動削除

