using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

public class ObjectPool : Singleton<ObjectPool> {
    public Player player;
    public List<CollisionObject> playerBullets;
    public List<Enemy> enemys;
    public List<CollisionObject> enemyBullets;
    public List<CollisionObject> explosions;
    public List<CollisionObject> items;

    public Texture2D sampleTexture;

    public ObjectPool() {
        playerBullets = new List<CollisionObject>();
        enemys = new List<Enemy>();
        enemyBullets = new List<CollisionObject>();
        explosions = new List<CollisionObject>();
        items = new List<CollisionObject>();
    }

    /// <summary>
    /// ObjectPoolで管理するオブジェクトを用意する
    /// </summary>
    public void Initialize() {
        player = new Player {
            position = new Vector2(200, 200),
            sprite = sampleTexture,
            spriteColor = Color.Green
        };

        for(int i = 0; i < 10; ++i) {
            enemys.Add(
            new Enemy {
                position = new Vector2(100 * (i + 1), 300),
                sprite = sampleTexture,
                spriteColor = Color.Red
            }
            );
        }
    }

    /// <summary>
    /// objectPoolで使用するContentを読み込む
    /// </summary>
    /// <param name="Content"></param>
    public void LoadContent(ContentManager Content) {
        sampleTexture = Content.Load<Texture2D>("Sprite/ArrowBullet");
    }

    /// <summary>
    /// ２つのCollisionObjectのリストを渡すとそれぞれのリスト当たり判定処理が行われる
    /// </summary>
    /// <param name="list1">CollisionObjectのリスト</param>
    /// <param name="list2">CollisionObjectのリスト</param>
    public void HitObjects<T1, T2>(List<T1> list1, List<T2> list2) where T1 : CollisionObject where T2 : CollisionObject {
        foreach(T1 obj in list1) {
            HitObjects(obj, list2);
        }
    }

    /// <summary>
    /// 一つのCollisionObjectとCollisionObjectのリストを渡すと一つobjとリストの当たり判定の処理が行われう
    /// </summary>
    /// <param name="obj">CollisionObject</param>
    /// <param name="list">CollisionObjectのリスト</param>
    public void HitObjects<T1, T2>(T1 obj, List<T2> list) where T1 : CollisionObject where T2 : CollisionObject {
        if(!obj.isActive) { return; }

        foreach(T2 other in list) {
            if(other.isActive && obj.CollisionCheck(other)) {
                obj.HitAction(other);
                other.HitAction(obj);
            }
        }
    }

    /// <summary>
    /// 渡したオブジェクトが寝ていれば起こす
    /// </summary>
    /// <typeparam name="T">gameObjectの派生クラス</typeparam>
    /// <param name="gameObject">起こしたいオブジェクト</param>
    /// <returns>起こしたオブジェクト</returns>
    public T WakeUp<T>(T gameObject) where T : GameObject {
        if(!gameObject.isActive) {
            gameObject.WakeUp();
            return gameObject;
        } else {
            return null;
        }
    }

    /// <summary>
    /// 渡したリストから寝ているオブジェクトを起こす。誰もいなければnull
    /// </summary>
    /// <typeparam name="T">gameObjectの派生クラスのリスト</typeparam>
    /// <param name="list">起こしたいオブジェクトを含むリスト</param>
    /// <returns>起こしたオブジェクト</returns>
    public T WakeUp<T>(List<T> list) where T : GameObject {
        T result = null;

        foreach(T gameObject in list) {
            result = WakeUp(gameObject);

            if(result != null) break;
        }

        return result;
    }

    /// <summary>
    /// 該当オブジェクトを描画する時に呼ぶ
    /// </summary>
    /// <param name="gameObject">GameObjectの派生クラス</param>
    /// <param name="spriteBatch">SpriteBatchの参照</param>
    public void Draw(GameObject gameObject, SpriteBatch spriteBatch) {
        gameObject.Draw(spriteBatch);
    }

    /// <summary>
    /// 該当オブジェクトのリストを描画する時に呼ぶ
    /// </summary>
    /// <param name="gameObject">GameObjectの派生クラスのリスト</param>
    /// <param name="spriteBatch">SpriteBatchの参照</param>
    public void Draw<T>(List<T> list, SpriteBatch spriteBatch) where T : GameObject {
        foreach(GameObject gameObject in list) {
            gameObject.Draw(spriteBatch);
        }
    }
}
