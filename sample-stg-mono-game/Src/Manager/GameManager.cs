﻿using Microsoft.Xna.Framework;
using System.Collections;

public class GameManager : Singleton<GameManager> {
    /// <summary>ゲームロジックの経過時間。1から始まる</summary>
    public int elapsedFrame { get; private set; } = 0;

    /// <summary>現在の合計スコア</summary>
    public int score { get; set; } = 0;

    /// <summary>倒す敵の数</summary>
    public int target { get; set; } = 0;

    /// <summary>現在のゲームの状態</summary>
    public GameState state { get; private set; } = GameState.standby;

    /// <summary>残機</summary>
    public int playerLeft { get; set; } = 6;

    private ObjectPool pool;
    private SpriteManager sprite;

    public GameManager() {
        pool = ObjectPool.instance;
        sprite = SpriteManager.instance;
    }

    public enum GameState {
        standby,
        play,
        death,
        clear,
        gameover
    }

    /// <summary>
    /// ゲームのメイン処理開始時の初期化
    /// </summary>
    public void Initialize() {
        score = 0;
        target = 0;
        state = GameState.standby;
        playerLeft = 6;
    }


    public void Update() {
        ++elapsedFrame;
        StateUpdate();
        GameObjectsHitCheck();
        GameObjectsUpdate();
    }

    void StateUpdate() {
        switch(state) {
            case GameState.standby:
                standbyCoroutine.Repeat(StandbyAction());
                break;

            case GameState.play:
                if(!pool.player.isActive) {
                    state = GameState.death;
                }

                if(target == 0) {
                    state = GameState.clear;
                }

                break;

            case GameState.clear:
                clearCoroutine.Repeat(ClearAction());
                break;

            case GameState.death:
                deathCoroutine.Repeat(DeathAction());
                break;

            case GameState.gameover:
                pool.Sleep(pool.player);
                break;

        }
    }

    Coroutine standbyCoroutine = new Coroutine();
    IEnumerator StandbyAction() {
        Player p = pool.WakeUp(pool.player);
        p?.Translate(new Vector2(CONST.AREA.RIGHT / 2, CONST.AREA.BOTTOM));

        for(int i = 0; i < 60; ++i) {
            yield return null;
        }

        // 初期化
        target = 0;

        for(int i = 0; i < 8; ++i) {
            Enemy e = pool.WakeUp(pool.enemys);

            if(i % 2 == 0) {
                e?.SetAction<Enemy0Action>()?.Init(e);
                e?.SetGraphic(sprite.Get(SpriteManager.Sprite.BirdA), Color.Red);
            } else {
                e?.SetAction<Enemy1Action>()?.Init(e);
                e?.SetGraphic(sprite.Get(SpriteManager.Sprite.ArrowBullet), Color.Pink);
            }

            e?.Translate(new Vector2(100 * (i + 1), 300));
        }

        state = GameState.play;
    }

    Coroutine clearCoroutine = new Coroutine();
    IEnumerator ClearAction() {
        for(int i = 0; i < 60; ++i) {
            yield return null;
        }

        state = GameState.standby;
    }

    Coroutine deathCoroutine = new Coroutine();
    IEnumerator DeathAction() {
        --playerLeft;

        for(int i = 0; i < 60; ++i) {
            yield return null;
        }

        if(playerLeft < 0) {
            state = GameState.gameover;
        } else {
            state = GameState.standby;
        }

        pool.Sleep(pool.playerBullets);
        pool.Sleep(pool.enemys);
        pool.Sleep(pool.enemyBullets);
    }

    /// <summary>当たり判定のチェックを行う</summary>
    void GameObjectsHitCheck() {
        //todo: 当たり判定チェックとUpdateで二重に呼ぶのは無駄な気がする
        pool.HitObjects(pool.playerBullets, pool.enemys);
        pool.HitObjects(pool.player, pool.enemys);
        pool.HitObjects(pool.player, pool.enemyBullets);
    }

    /// <summary>各GameObjectのUpdateを行う</summary>
    void GameObjectsUpdate() {
        pool.Update(pool.player);
        pool.Update(pool.enemys);
        pool.Update(pool.playerBullets);
        pool.Update(pool.enemyBullets);
    }
}
