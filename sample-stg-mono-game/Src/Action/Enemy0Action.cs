using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;

public class Enemy0Action : EnemyAction<Enemy> {
    public override void Init(Enemy gameObject) {
        hoge?.Reset();
        base.Init(gameObject);
    }

    public override IEnumerator NormalAction() {
        for(int i = 0; i < 30; ++i) {
            gameObject?.MoveFront(6);
            yield return null;
        }

        for(int i = 0; i < 10; ++i) {
            gameObject?.LookAtTarget(pool.player.position, 10);
            yield return null;
        }

        while(hoge.Repeat(Hoge())) {
            yield return null;
        }

        for(int i = 0; i < 10; ++i) {
            if(i % 5 == 0) {
                gameObject?.Shot();
            }

            yield return null;
        }

        for(int i = 0; i < 10; ++i) {
            gameObject?.LookAtTarget(pool.player.position, 10);
            yield return null;
        }
    }


    Coroutine hoge = new Coroutine();
    protected IEnumerator Hoge() {
        for(int i = 0; i < 100; ++i) {
            if(i % 2 == 0) {
                gameObject?.Shot();
            }

            yield return null;
        }
    }

}
