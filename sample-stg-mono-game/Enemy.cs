using Microsoft.Xna.Framework;
using System;

public class Enemy : CollisionObject {
    protected int score = 100;

    public Enemy() {

    }

    public override void Update() {
        //Spin(-1);
        //System.Diagnostics.Debug.WriteLine(MathHelper.ToRadians(rotation));
        Rotate(ToTargetRadian(pool.player.position));
        MoveFront(1);

        if(manager.elapsedFrame % 60 == 0) {
            Shot();
        }
    }

    public override void WakeUp() {
        ++manager.target;
        base.WakeUp();
    }

    public override void Sleep() {
        base.Sleep();
    }

    protected void Shot() {
        var(radian, length) = ToTargetRadLen(pool.player.position);
        EnemyBullet eb = pool.WakeUp(pool.enemyBullets);
        eb?.Init(position, radian, length / 60); //必ず60フレームで到達する
    }

    public override void HitAction(CollisionObject other) {
        --manager.target;
        manager.score += score;
        base.HitAction(other);
    }
}
