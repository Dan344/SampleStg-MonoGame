using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// ゲーム中に登場するオブジェクトはこれを継承する。
/// </summary>
public class GameObject {
    /// <summary>Uniqueなid(未実装)</summary>
    public int id { get; set; }
    /// <summary>gameObjectの表示するスプライト(一部を切り出して使う予定)</summary>
    public Texture2D sprite { get; set; }
    /// <summary>スプライトの色。Whiteが原色</summary>
    public Color spriteColor { get; set; }
    /// <summary>スプライトの反転</summary>
    public SpriteEffects spriteFlip { get; set; }
    /// <summary>スプライトの重なり順</summary>
    public float spriteDepth { get; set; }
    /// <summary>gameObjectの現在座標</summary>
    public Vector2 position { get; set; }
    /// <summary>gameObjectの現在の向き(degree)</summary>
    public float rotation { get; set; }
    /// <summary>gameObjectの現在の大きさ</summary>
    public float scale { get; set; }
    /// <summary>親オブジェクトの参照</summary>
    public GameObject parent { get; set; }

    bool _isActive;
    /// <summary>gameObjectが現在無効になっているか</summary>
    public bool isActive { get { return _isActive; } }

    SpriteFont debugFont;

    public GameObject() {
        _isActive = false;
        sprite = null;
        spriteColor = Color.White;
        spriteFlip = SpriteEffects.None;
        spriteDepth = 0;
        position = Vector2.Zero;
        rotation = 0;
        scale = 1;
    }

    /// <summary>poolに寝かす時の設定</summary>
    public virtual void Sleep() {
        _isActive = false;
    }

    /// <summary>poolから起こす時の初期化</summary>
    public virtual void WakeUp() {
        _isActive = true;
        spriteColor = Color.White;
        position = Vector2.Zero;
        rotation = 0;
        scale = 1;
        parent = null;
    }

    /// <summary>
    /// 適切な場所で呼ぶと描画を行う
    /// </summary>
    /// <param name="spriteBatch"></param>
    public void Draw(SpriteBatch spriteBatch) {
        if(!isActive) return;

        spriteBatch.Draw(sprite
                         , position
                         , null
                         , spriteColor
                         , rotation
                         , new Vector2(sprite.Width / 2, sprite.Height / 2) //pivot
                         , scale
                         , spriteFlip
                         , spriteDepth
                        );

        //spriteBatch.DrawString(debugFont, position, position, Color.White);
    }

    /// <summary>
    /// 渡したベクトルに向けて移動する
    /// </summary>
    /// <param name="vector">移動させたいvector</param>
    public virtual void Move(Vector2 vector) {
        position += vector;
    }

    /// <summary>
    /// 回転させる
    /// </summary>
    /// <param name="vector">移動した後の座標</param>
    public virtual void Rotate(float degree) {
        rotation += degree;
    }
}
