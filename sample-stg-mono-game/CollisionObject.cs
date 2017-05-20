using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

/// <summary>
/// 当たり判定を持つオブジェクト
/// </summary>
public class CollisionObject : GameObject {
    //public int hp=1;
    //public int power=1;

    Rectangle collisionArea;

    public CollisionObject() : base() {
        //todo: 当たり判定の位置をずらせるようにする。とりあえずハードコーディング
        collisionArea = new Rectangle(0, 0, 32, 32);
    }

    public bool CollisionCheck(CollisionObject other) {
        collisionArea.Location = new Point((int)position.X, (int)position.Y);
        other.collisionArea.Location = new Point((int)other.position.X, (int)other.position.Y);

        Debug.WriteLine("piyoooooooooooooooo");

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
