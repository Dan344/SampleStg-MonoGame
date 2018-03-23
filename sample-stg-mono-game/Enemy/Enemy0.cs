using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;

public class Enemy0 : Enemy {

    protected override IEnumerator NormalAction() {
        for(int i = 0; i < 60; ++i) {
            MoveFront(6);
            yield return null;
        }

        for(int i = 0; i < 10; ++i) {
            LookAtTarget(pool.player.position, 10);
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
}
