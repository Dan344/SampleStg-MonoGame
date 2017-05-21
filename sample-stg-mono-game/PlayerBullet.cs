using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class PlayerBullet : CollisionObject {
    float speed = 10;

    /// <summary>毎フレーム呼ぶ</summary>
    public void Update() {
        if(!isActive) return;

        Move(-Vector2.UnitY * speed);
    }
}
