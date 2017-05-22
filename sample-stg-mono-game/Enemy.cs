using System;

public class Enemy : CollisionObject {
    protected int score = 100;

    public Enemy() {

    }

    public override void Update() {
        //Spin(1);
        Rotate(GetLookAt(pool.player.position));
        MoveFront(1);

        if(manager.elapsedFrame % 60 == 0) {
            Shot();
        }
    }

    protected void Shot() {
        EnemyBullet eb = pool.WakeUp(pool.enemyBullets);
        eb?.Translate(position);
        eb?.Rotate(rotation);
    }

    public override void HitAction(CollisionObject other) {
        manager.score += score;
        base.HitAction(other);
    }
}
