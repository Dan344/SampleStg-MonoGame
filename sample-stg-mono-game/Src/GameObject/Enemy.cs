using Microsoft.Xna.Framework;
using System;
using System.Collections;

public abstract class Enemy : CollisionObject {
    protected int score = 100;

    public Enemy() {
        normalAction = new Coroutine();
    }

    //public override void Initialize();

    public override void Update() {
        //Coroutine.Repeat(ref normalAction, NormalAction());

        normalAction.Repeat(NormalAction());

        //if(normalAction == null) {

        //    //Console.WriteLine("hoge");
        //    normalAction = NormalAction();
        //}

        //if(!normalAction?.MoveNext() ?? false) {
        //    normalAction = null;
        //}


        //normalAction?.MoveNext();


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

    Coroutine normalAction;

    //IEnumerator normalAction;
    protected abstract IEnumerator NormalAction();

    public override void WakeUp() {
        ++manager.target;
        base.WakeUp();
    }

    public override void Sleep() {
        base.Sleep();
    }

    protected virtual void Shot() {
        var(degree, length) = ToTargetDegLen(pool.player.position);
        EnemyBullet eb = pool.WakeUp(pool.enemyBullets);
        eb?.Init(position, degree, length / 60); //必ず60フレームで到達する
    }

    public override void HitAction(CollisionObject other) {
        --manager.target;
        manager.score += score;
        base.HitAction(other);
    }
}
