using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Microsoft.Xna.Framework.MathHelper;
using static System.Math;
using static Microsoft.Xna.Framework.Vector2;
using static Microsoft.Xna.Framework.Matrix;

/// <summary>
/// ゲーム中に登場するオブジェクトはこれを継承する。
/// </summary>
public abstract class GameObject {
    /// <summary>gameObjectの表示するスプライト(一部を切り出して使う予定)</summary>
    public Texture2D sprite { get; set; }
    /// <summary>スプライトの色。Whiteが原色</summary>
    public Color spriteColor { get; set; }
    /// <summary>スプライトの反転</summary>
    public SpriteEffects spriteFlip { get; set; }
    /// <summary>スプライトの重なり順</summary>
    public float spriteDepth { get; set; }
    /// <summary>スプライトの正面</summary>
    public SpriteFront spriteFront { get; set; }

    /// <summary>gameObjectの現在座標</summary>
    public Vector2 position { get; protected set; }
    /// <summary>gameObjectの現在の向き(degree, 0:右 90:上)</summary>
    public float rotation { get; protected set; }

    /// <summary>gameObjectの現在の大きさ</summary>
    public float scale { get; set; }

    /// <summary>gameObjectが現在無効になっているか</summary>
    public bool isActive { get; private set; }

    protected Input input;
    protected ObjectPool pool;
    protected GameManager manager;

    /// <summary>スプライトの正面を指定する</summary>
    public enum SpriteFront {
        right  = 0,
        top    = 90,
        left   = 180,
        bottom = 270
    }

    public GameObject() {
        input = Input.instance;
        pool = ObjectPool.instance;
        manager = GameManager.instance;

        isActive = false;
        sprite = null;
        spriteColor = Color.White;
        spriteFlip = SpriteEffects.None;
        spriteDepth = 0;
        position = Zero;
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
                         , ToRadians(rotation + (float)spriteFront)
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
    /// <param name="degree">回転速度</param>
    /// <returns>回転後の向き</returns>
    public virtual float Spin(float degree) => rotation += degree;

    /// <summary>rotationに基づいた現在の正面の向きを単位ベクトルで返す</summary>
    public Vector2 GetFront() => Transform(UnitX, CreateRotationZ(ToRadians(rotation)));

    /// <summary>rotationに基づいた現在の後ろの向きを単位ベクトルで返す</summary>
    public Vector2 GetBack() => Transform(-UnitX, CreateRotationZ(ToRadians(rotation)));

    /// <summary>rotationに基づいた現在の右の向きを単位ベクトルで返す</summary>
    public Vector2 GetRight() => Transform(UnitY, CreateRotationZ(ToRadians(rotation)));

    /// <summary>rotationに基づいた現在の左の向きを単位ベクトルで返す</summary>
    public Vector2 GetLeft() => Transform(-UnitY, CreateRotationZ(ToRadians(rotation)));

    /// <summary>現在の正面に向かって移動する</summary>
    /// <param name="speed">速度</param>
    /// <returns>移動後の座標</returns>
    public virtual Vector2 MoveFront(float speed) => position += GetFront() * speed;

    /// <summary>現在の後ろに向かって移動する</summary>
    /// <param name="speed">速度</param>
    /// <returns>移動後の座標</returns>
    public virtual Vector2 MoveBack(float speed) => position += GetBack() * speed;

    /// <summary>現在の右に向かって移動する</summary>
    /// <param name="speed">速度</param>
    /// <returns>移動後の座標</returns>
    public virtual Vector2 MoveRight(float speed) => position += GetRight() * speed;

    /// <summary>現在の左に向かって移動する</summary>
    /// <param name="speed">速度</param>
    /// <returns>移動後の座標</returns>
    public virtual Vector2 MoveLeft(float speed) => position += GetLeft() * speed;

    /// <summary>現在位置から指定座標を向くrotation(degree)が返される。移動はしない</summary>
    /// <param name="target">目標</param>
    /// <returns>目標方向</returns>
    public float GetLookAt(Vector2 target) {
        Vector2 dv = target - position;
        return ToDegrees((float)Atan2(dv.Y, dv.X));
    }
}
