using Microsoft.Xna.Framework;
using static CONST;

public class EnemyBullet : CollisionObject {
    float speed = 8;

    protected override void Init() {
        speed = 8;
        base.Init();
    }

    /// <summary>
    /// WakeUp後の初期化
    /// </summary>
    /// <param name="position">座標</param>
    /// <param name="rotation">向き</param>
    /// <param name="speed">速度</param>
    public virtual void Set(Vector2 position, float rotation, float speed) {
        this.position = position;
        this.rotation = rotation;
        this.speed = MathHelper.Clamp(speed, 1, 30);
    }

    /// <summary>毎フレーム呼ぶ</summary>
    public override void Update() {
        MoveFront(speed);
        //Move(GetForward()*speed);

        if(position.Y <= AREA.TOP - AREA.E_BULLET_DELETE_MARGIN
                || position.Y >= AREA.BOTTOM + AREA.E_BULLET_DELETE_MARGIN
                || position.X <= AREA.LEFT - AREA.E_BULLET_DELETE_MARGIN
                || position.X >= AREA.RIGHT + AREA.E_BULLET_DELETE_MARGIN
          ) {
            Sleep();
        }
    }
}
