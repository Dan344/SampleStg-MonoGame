using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class EnemyBullet : CollisionObject {
    float speed = 1;

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
