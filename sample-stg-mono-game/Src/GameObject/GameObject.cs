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

    float _rotation;
    /// <summary>gameObjectの現在の向き(degree, 0:右 90:下)</summary>
    public float rotation {
        get => _rotation;
        protected set => _rotation = NormalizeDegree(value);
    }

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

        sprite = null;
        spriteColor = Color.White;
        spriteFlip = SpriteEffects.None;
        spriteDepth = 0;
        spriteFront = SpriteFront.right;

        Init();
    }

    protected virtual void Init() {
        position = Zero;
        rotation = 0;
        scale = 1;

        isActive = false;
    }

    /// <summary>poolに寝かす時の設定</summary>
    public virtual void Sleep() {
        isActive = false;
    }

    /// <summary>poolから起こす時の初期化</summary>
    public virtual void WakeUp() {
        Init();
        isActive = true;
    }

    /// <summary>
    /// 適切な場所で呼ぶと描画を行う
    /// </summary>
    /// <param name="spriteBatch"></param>
    public void Draw(SpriteBatch spriteBatch) {
        if(!isActive) return;

        spriteBatch?.Draw(
            sprite,
            position,
            null,
            spriteColor,
            ToRadians(rotation + (float)spriteFront),
            new Vector2((sprite?.Width ?? 1) / 2, (sprite?.Height ?? 1) / 2), //pivot
            scale,
            spriteFlip,
            spriteDepth
        );
    }

    public abstract void Update();

    /// <summary>渡した座標に移動する</summary>
    /// <param name="position">移動したい場所</param>
    public Vector2 Translate(Vector2 position) => this.position = position;

    /// <summary>指定した方向を向く</summary>
    /// <param name="degree">度数</param>
    public float Rotate(float degree) => rotation = degree;

    /// <summary>渡したベクトルに向けて移動する</summary>
    /// <param name="vector">移動させたいvector</param>
    public Vector2 Move(Vector2 vector) => position += vector;

    /// <summary>現在の正面に向かって移動する</summary>
    /// <param name="speed">速度</param>
    /// <returns>移動後の座標</returns>
    public Vector2 MoveFront(float speed) => position += GetFront() * speed;

    /// <summary>現在の後ろに向かって移動する</summary>
    /// <param name="speed">速度</param>
    /// <returns>移動後の座標</returns>
    public Vector2 MoveBack(float speed) => position += GetBack() * speed;

    /// <summary>現在の右に向かって移動する</summary>
    /// <param name="speed">速度</param>
    /// <returns>移動後の座標</returns>
    public Vector2 MoveRight(float speed) => position += GetRight() * speed;

    /// <summary>現在の左に向かって移動する</summary>
    /// <param name="speed">速度</param>
    /// <returns>移動後の座標</returns>
    public Vector2 MoveLeft(float speed) => position += GetLeft() * speed;

    /// <summary>現在位置から目標位置に向かって指定した速度で移動する。目標座標に到達したらtrue</summary>
    /// <param name="target">目標座標</param>
    /// <param name="speed">速度</param>
    /// <returns>目標座標に到達したらtrue</returns>
    public bool Move2Target(Vector2 target, float speed) {
        position = VectorFit(position, target, speed);
        return position == target;
    }

    /// <summary>回転させる(現在の向き+degree)</summary>
    /// <param name="speed">回転速度(左回り)</param>
    /// <returns>回転後の向き</returns>
    public float Spin(float speed) => rotation += speed;

    /// <summary>
    /// 指定したターゲットの方向に指定した速度で向く。ターゲットの方向を向いたらtrue
    /// </summary>
    /// <param name="target">目標の座標</param>
    /// <param name="speed">回転速度</param>
    /// <returns>ターゲットの方向を向いているか</returns>
    public bool LookAtTarget(Vector2 target, float speed) {
        float targetDegree = NormalizeDegree(ToTargetDegree(target));
        rotation = DegreeFit(rotation, targetDegree, speed);
        return rotation == targetDegree;
    }

    /// <summary>rotationに基づいた現在の正面の向きを単位ベクトルで返す</summary>
    public Vector2 GetFront() => Transform(UnitX, CreateRotationZ(ToRadians(rotation)));

    /// <summary>rotationに基づいた現在の後ろの向きを単位ベクトルで返す</summary>
    public Vector2 GetBack() => Transform(-UnitX, CreateRotationZ(ToRadians(rotation)));

    /// <summary>rotationに基づいた現在の右の向きを単位ベクトルで返す</summary>
    public Vector2 GetRight() => Transform(UnitY, CreateRotationZ(ToRadians(rotation)));

    /// <summary>rotationに基づいた現在の左の向きを単位ベクトルで返す</summary>
    public Vector2 GetLeft() => Transform(-UnitY, CreateRotationZ(ToRadians(rotation)));

    /// <summary>現在位置から目標への向き(degree)が返される。移動はしない</summary>
    public float ToTargetDegree(Vector2 target) => Vector2Degree(ToTargetVector(target));

    /// <summary>現在位置から目標へのベクトルが返される。</summary>
    public Vector2 ToTargetVector(Vector2 target) => target - position;

    /// <summary>現在位置から目標への単位ベクトルが返される。</summary>
    public Vector2 ToTargetNormalizedVector(Vector2 target) => Normalize(target - position);

    /// <summary>現在位置から目標への距離を返す</summary>
    public float ToTargetLength(Vector2 target) => ToTargetVector(target).Length();

    /// <summary>現在位置から目標への角度と距離をTupleで返す</summary>
    public(float degree, float length) ToTargetDegLen(Vector2 target) {
        Vector2 v = ToTargetVector(target);
        return (Vector2Degree(v), v.Length());
    }

    /// <summary>
    /// ある座標を目標の座標にpowerの速度で移動する。targetにぴったりフィット。
    /// </summary>
    /// <param name="current">現在座標</param>
    /// <param name="target">目標座標</param>
    /// <param name="power">力</param>
    /// <returns>移動後の座標</returns>
    public Vector2 VectorFit(Vector2 current, Vector2 target, float power) {
        Vector2 toTarget = target - current;

        if(toTarget.Length() > power) {
            current += Normalize(toTarget) * power;
        } else {
            current = target;
        }

        return current;
    }

    /// <summary>角度を目標の角度に速度を指定して回転させる。角度を返すだけ</summary>
    /// <param name="current">変換したい角度</param>
    /// <param name="target">目標の角度</param>
    /// <param name="speed">回転の速度</param>
    /// <returns>回転後の角度</returns>
    public float DegreeFit(float current, float target, float speed) {
        float delta = DeltaAngle(current, target);

        if(delta < -speed) {
            current -= speed;
        } else if(delta > speed) {
            current += speed;
        } else {
            current = target;
        }

        return current;
    }

    /// <summary>２つの角度(degree)の差分を求める</summary>
    /// <param name="current">基礎とする角度(degree)。正規化されていること</param>
    /// <param name="target">相手とする角度(degree)。正規化されていること</param>
    /// <returns>-180~n~180</returns>
    public float DeltaAngle(float current, float target) {
        float result = target - current;

        if(result >= 180) result -= 360;
        else if(result <= -180) result += 360;

        return result;
    }

    /// <summary>ベクトルを向き(degree)に変換する</summary>
    public float Vector2Degree(Vector2 vector) => ToDegrees((float)Atan2(vector.Y, vector.X));

    /// <summary>角度(degree)をベクトルに変換する</summary>
    public Vector2 Degree2Vector(float degree) => Transform(UnitX, CreateRotationZ(ToRadians(degree)));

    /// <summary>角度(degree)を正規化する(0°以上360°未満。360は0になる)</summary>
    /// <param name="degree">正規化したい角度</param>
    /// <returns>正規化後の角度</returns>
    public float NormalizeDegree(float degree) {
        if(degree >= 360) {
            degree -= 360;
        } else if(degree < 0) {
            degree += 360;
        } else {
            return degree;
        }

        return NormalizeDegree(degree);
    }
}