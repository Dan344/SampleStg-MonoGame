using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// 当たり判定を持つオブジェクト
/// </summary>
public class CollisionObject : GameObject {
    Rectangle collisionArea;

    public CollisionObject() : base() {
        //todo: 当たり判定の位置をずらせるようにする。とりあえずハードコーディング
        collisionArea = new Rectangle(0, 0, 32, 32);
    }

    public bool CollisionCheck(CollisionObject other) {
        collisionArea.Offset(position);
        other.collisionArea.Offset(other.position);

        if(collisionArea.Intersects(other.collisionArea)) {
            return true;
        } else {
            return false;
        }
    }
}
