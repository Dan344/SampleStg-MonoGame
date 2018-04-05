using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;

public class Enemy1Action : EnemyAction<Enemy> {

    public override IEnumerator NormalAction() {
        for(int i = 0; i < 60; ++i) {
            gameObject.LookAtPlayer(2);
            gameObject.MoveFront(8);
            yield return null;
        }

        gameObject.Shot();

        for(int i = 0; i < 30; ++i) {
            gameObject.LookAtPlayer(8);
            gameObject.MoveBack(8);
            yield return null;
        }
    }

}
