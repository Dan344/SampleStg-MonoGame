using Microsoft.Xna.Framework;

public class Player : CollisionObject {
    public float speed = 5;

    public Player() {
        //Debug.WriteLine(input);
    }

    /// <summary>
    /// 毎フレーム呼ぶ
    /// </summary>
    public override void Update() {
        ControleMove();
        Shot();
    }

    public void Shot() {
        if(input.GetActionDown(Input.Action.shot)) {
            PlayerBullet bullet = pool.WakeUp(pool.playerBullets);
            bullet?.Translate(position);
        }
    }

    public override void HitAction(CollisionObject other) {
        base.HitAction(other);
    }

    /// <summary>
    /// ユーザの入力に応じて移動する
    /// </summary>
    public void ControleMove() {
        base.Move(input.normalizedVector * speed);
        Vector2 limit = position;

        if(position.Y < CONST.AREA.MOVEBLE_TOP) {
            limit.Y = CONST.AREA.MOVEBLE_TOP;
        } else if(position.Y > CONST.AREA.MOVEBLE_BOTTOM) {
            limit.Y = CONST.AREA.MOVEBLE_BOTTOM;
        }

        if(position.X < CONST.AREA.MOVEBLE_LEFT) {
            limit.X = CONST.AREA.MOVEBLE_LEFT;
        } else if(position.X > CONST.AREA.MOVEBLE_RIGHT) {
            limit.X = CONST.AREA.MOVEBLE_RIGHT;
        }

        Translate(limit);
    }
}
