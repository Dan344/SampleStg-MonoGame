using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Enemy : CollisionObject {
    public Enemy() {

    }

    public override void HitAction(CollisionObject other) {
        base.HitAction(other);
    }
}
