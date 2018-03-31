using System.Collections;

public class Enemy0 : Enemy {

    protected override IEnumerator NormalAction() {
        for(int i = 0; i < 30; ++i) {
            MoveFront(6);
            yield return null;
        }

        for(int i = 0; i < 10; ++i) {
            LookAtTarget(pool.player.position, 10);
            yield return null;
        }

        while(!Coroutine.Repeat(ref hoge, Hoge())) {
            yield return null;
        }

        for(int i = 0; i < 10; ++i) {
            if(i % 5 == 0) {
                Shot();
            }

            yield return null;
        }

        for(int i = 0; i < 10; ++i) {
            LookAtTarget(pool.player.position, 10);
            yield return null;
        }
    }

    IEnumerator hoge;
    protected IEnumerator Hoge() {
        for(int i = 0; i < 100; ++i) {
            if(i % 2 == 0) {
                Shot();
            }

            yield return null;
        }
    }
}
