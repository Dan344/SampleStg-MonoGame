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
        if(input.GetAction(Input.Action.shot)) {
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
    }
}
