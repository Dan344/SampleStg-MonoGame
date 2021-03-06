﻿using Microsoft.Xna.Framework;

/// <summary>
/// 当たり判定を持つオブジェクト
/// </summary>
public abstract class CollisionObject : GameObject {
    Rectangle collisionArea;

    public CollisionObject() : base() {
        //todo: 当たり判定の位置をずらせるようにする。とりあえずハードコーディング
        collisionArea = new Rectangle(0, 0, 32, 32);
    }

    protected override void Init() {
        collisionArea = new Rectangle(0, 0, 32, 32);
        base.Init();
    }

    public bool CollisionCheck(CollisionObject other) {
        collisionArea.Location = new Point((int)position.X, (int)position.Y);
        other.collisionArea.Location = new Point((int)other.position.X, (int)other.position.Y);

        if(collisionArea.Intersects(other.collisionArea)) {
            return true;
        } else {
            return false;
        }
    }

    public virtual void HitAction(CollisionObject other) {
        Sleep();
    }
}
