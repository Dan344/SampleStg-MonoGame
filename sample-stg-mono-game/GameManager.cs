using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

    public GameManager() {
        pool = ObjectPool.instance;
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

    public void Standby() {
        Player p = pool.WakeUp(pool.player);
        p?.Translate(new Vector2(CONST.AREA.RIGHT / 2, CONST.AREA.BOTTOM));

        pool.Sleep(pool.playerBullets);
        pool.Sleep(pool.enemys);
        pool.Sleep(pool.enemyBullets);

        target = 0;

        for(int i = 0; i < 8; ++i) {
            Enemy e = pool.WakeUp(pool.enemys);
            e?.Translate(new Vector2(100 * i, 100));
        }

        state = GameState.play;
    }

    public void Update() {
        if(state == GameState.standby) {
            Standby();
        }

        if(playerLeft < 0) {
            state = GameState.gameover;
        }

        if(state == GameState.gameover) {
            pool.Sleep(pool.player);
        } else if(target == 0) {
            state = GameState.clear;
        } else if(!pool.player.isActive) {
            state = GameState.death;
            --playerLeft;
            Standby();
        }

        ++elapsedFrame;
    }
}
