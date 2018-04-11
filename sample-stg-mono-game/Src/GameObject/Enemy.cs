using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

public class Enemy : CollisionObject {
    /// <summary>破壊時に獲得可能な素点</summary>
    protected int score = 100;

    /// <summary>動作</summary>
    private EnemyAction<Enemy> action;

    protected override void Init() {
        score = 100;
        base.Init();
    }

    /// <summary>
    /// 指定の行動(action)を新規にセットする。
    /// 指定のactionが既にセットされていたら再利用する。
    /// </summary>
    /// <typeparam name="T">actionの型</typeparam>
    /// <returns>セットされているactionを返す</returns>
    public EnemyAction<Enemy> SetAction<T>() where T : EnemyAction<Enemy>, new() {
        if(action is T) {
            return action;
        } else {
            return action = new T();
        }
    }

    public void SetGraphic(Color color) {
        spriteColor = color;
    }

    public override void Update() {
        action?.Update();

        //Spin(1);
        //LookAtTarget(pool.player.position, 1);
        //System.Diagnostics.Debug.WriteLine((rotation));
        //Rotate(ToTargetDegree(pool.player.position));
        //MoveFront(1);
        //Move2Target(pool.player.position, 3);

        //if(LookAtTarget(pool.player.position, 1)) {
        //    Shot();
        //}

        //if(manager.elapsedFrame % 60 == 0) {
        //    //Shot();
        //}
    }

    public override T WakeUp<T>() {
        T result = base.WakeUp<T>();

        if(result != null) ++manager.target;

        return result;
    }

    public override void Sleep() {
        base.Sleep();
    }

    public virtual void Shot() {
        var(degree, length) = ToTargetDegLen(pool.player.position);
        EnemyBullet eb = pool.WakeUp(pool.enemyBullets);
        eb?.Set(position, degree, length / 60); //必ず60フレームで到達する
    }

    public override void HitAction(CollisionObject other) {
        --manager.target;
        manager.score += score;
        base.HitAction(other);
    }

    public bool LookAtPlayer(float speed) => LookAtTarget(pool.player.position, speed);
}
