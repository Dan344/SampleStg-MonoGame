using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class PlayerBullet : CollisionObject {
    float speed = 10;

    protected override void Init() {
        speed = 10;
        base.Init();
    }

    /// <summary>毎フレーム呼ぶ</summary>
    public override void Update() {
        Move(-Vector2.UnitY * speed);

        if(position.Y <= CONST.AREA.P_BULLET_DELETE_LINE) {
            Sleep();
        }
    }
}
