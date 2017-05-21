using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// ゲーム中に登場するオブジェクトはこれを継承する。
/// </summary>
public abstract class GameObject {
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
    public Vector2 position { get; protected set; }
    /// <summary>gameObjectの現在の向き(degree)</summary>
    public float rotation { get; protected set; }

    /// <summary>gameObjectの現在の大きさ</summary>
    public float scale { get; set; }
    /// <summary>親オブジェクトの参照</summary>
    public GameObject parent { get; set; }

    /// <summary>gameObjectが現在無効になっているか</summary>
    public bool isActive { get; private set; }

    protected Input input;
    protected ObjectPool pool;
    protected GameManager manager;

    public GameObject() {
        input = Input.instance;
        pool = ObjectPool.instance;
        manager = GameManager.instance;

        isActive = false;
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
        isActive = false;
    }

    /// <summary>poolから起こす時の初期化</summary>
    public virtual void WakeUp() {
        isActive = true;
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
                         , MathHelper.ToRadians(rotation)
                         , new Vector2(sprite.Width / 2, sprite.Height / 2) //pivot
                         , scale
                         , spriteFlip
                         , spriteDepth
                        );
    }

    public abstract void Update();

    /// <summary>渡した座標に移動する</summary>
    /// <param name="position">移動したい場所</param>
    public virtual Vector2 Translate(Vector2 position) => this.position = position;

    /// <summary>指定した方向を向く</summary>
    /// <param name="degree">度数</param>
    public virtual float Rotate(float degree) => rotation = degree;

    /// <summary>渡したベクトルに向けて移動する</summary>
    /// <param name="vector">移動させたいvector</param>
    public virtual Vector2 Move(Vector2 vector) => position += vector;

    /// <summary>回転させる(現在の向き+degree)</summary>
    /// <param name="vector">移動した後の座標</param>
    public virtual float Spin(float degree) => rotation += degree;

    public virtual Vector2 MoveForward(float speed) {
        Matrix rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(this.rotation));
        Vector2 direction = Vector2.Transform(-Vector2.UnitY, rotation);

        position += direction * speed;
        return position;
    }
}
