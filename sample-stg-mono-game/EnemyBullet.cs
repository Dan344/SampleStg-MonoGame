using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

public class EnemyBullet : CollisionObject {
    float speed = 8;

    /// <summary>
    /// WakeUp後の初期化
    /// </summary>
    /// <param name="position">座標</param>
    /// <param name="rotation">向き</param>
    /// <param name="speed">速度</param>
    public virtual void Init(Vector2 position, float rotation, float speed) {
        this.position = position;
        this.rotation = rotation;
        this.speed = MathHelper.Clamp(speed, 1, 30);
    }

    /// <summary>毎フレーム呼ぶ</summary>
    public override void Update() {
        MoveFront(speed);
        //Move(GetForward()*speed);

        if(position.Y <= CONST.AREA.TOP - CONST.AREA.E_BULLET_DELETE_MARGIN
                || position.Y >= CONST.AREA.BOTTOM + CONST.AREA.E_BULLET_DELETE_MARGIN
                || position.X <= CONST.AREA.LEFT - CONST.AREA.E_BULLET_DELETE_MARGIN
                || position.X >= CONST.AREA.RIGHT + CONST.AREA.E_BULLET_DELETE_MARGIN
          ) {
            Sleep();
        }
    }
}
