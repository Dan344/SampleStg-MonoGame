using System;

public class Enemy : CollisionObject {
    public Enemy() {

    }

    public override void Update() { }

    public override void HitAction(CollisionObject other) {
        base.HitAction(other);
    }
}
