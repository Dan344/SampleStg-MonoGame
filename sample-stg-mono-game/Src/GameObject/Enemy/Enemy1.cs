using Microsoft.Xna.Framework;
using System.Collections;

public class Enemy1 : Enemy {
    protected override void Init() {
        base.Init();

        spriteColor = Color.Green;
    }

    protected override IEnumerator NormalAction() {
        for(int i = 0; i < 100; ++i) {
            LookAtPlayer(1);
            MoveFront(8);
            yield return null;
        }

        Shot();

        for(int i = 0; i < 80; ++i) {
            LookAtTarget(pool.player.position, 10);
            yield return null;
        }
    }
}
