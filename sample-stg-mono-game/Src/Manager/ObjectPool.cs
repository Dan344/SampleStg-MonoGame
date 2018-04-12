using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class ObjectPool : Singleton<ObjectPool> {
    public Player player { get; private set; }
    public List<PlayerBullet> playerBullets { get; private set; }
    public List<Enemy> enemys { get; private set; }
    public List<EnemyBullet> enemyBullets { get; private set; }
    public List<CollisionObject> explosions { get; private set; }
    public List<CollisionObject> items { get; private set; }

    SpriteManager sprite;

    public ObjectPool() {
        playerBullets = new List<PlayerBullet>();
        enemys = new List<Enemy>();
        enemyBullets = new List<EnemyBullet>();
        explosions = new List<CollisionObject>();
        items = new List<CollisionObject>();

        sprite = SpriteManager.instance;
    }

    /// <summary>ObjectPoolで管理するオブジェクトを用意する</summary>
    public void Initialize() {
        player = new Player {
            sprite = sprite.Get(SpriteManager.Sprite.Player),
            spriteColor = Color.Green
        };

        for(int i = 0; i < 4; ++i) {
            playerBullets.Add(new PlayerBullet {
                sprite = sprite.Get(SpriteManager.Sprite.ArrowBullet),
                spriteColor = Color.Aqua
            });
        }

        for(int i = 0; i < 55; ++i) {
            enemys.Add(new Enemy {
                sprite = sprite.Get(SpriteManager.Sprite.ArrowBullet),
                spriteColor = Color.Red,
                spriteFront = GameObject.SpriteFront.top
            });
        }

        for(int i = 0; i < 2000; ++i) {
            enemyBullets.Add(new EnemyBullet {
                sprite = sprite.Get(SpriteManager.Sprite.VecBullet),
                spriteColor = Color.Yellow,
                spriteFront = GameObject.SpriteFront.top
            });
        }
    }

    /// <summary>渡したオブジェクトのUpdate()を呼ぶ</summary>
    public void Update<T>(T obj) where T : GameObject {
        if(obj.isActive) obj.Update();
    }

    /// <summary>渡したリストのUpdate()を呼ぶ</summary>
    public void Update<T>(List<T> list) where T : GameObject {
        foreach(T obj in list) Update(obj);
    }

    /// <summary>２つのCollisionObjectのリストを渡すとそれぞれのリスト当たり判定処理が行われる</summary>
    /// <param name="list1">CollisionObjectのリスト</param>
    /// <param name="list2">CollisionObjectのリスト</param>
    public void HitObjects<T1, T2>(List<T1> list1, List<T2> list2) where T1 : CollisionObject where T2 : CollisionObject {
        foreach(T1 obj in list1) {
            HitObjects(obj, list2);
        }
    }

    /// <summary>一つのCollisionObjectとCollisionObjectのリストを渡すと一つobjとリストの当たり判定の処理が行われう</summary>
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

    /// <summary>渡したオブジェクトが寝ていれば起こす</summary>
    /// <typeparam name="T">gameObjectの派生クラス</typeparam>
    /// <param name="gameObject">起こしたいオブジェクト</param>
    /// <returns>起こしたオブジェクト</returns>
    public T WakeUp<T>(T gameObject) where T : GameObject {
        return gameObject.WakeUp<T>();
    }

    /// <summary>渡したリストから寝ているオブジェクトを起こす。誰もいなければnull</summary>
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

    /// <summary>該当オブジェクトを全てスリープする</summary>
    /// <typeparam name="T">GameObjectの派生クラス</typeparam>
    /// <param name="obj">寝かせたいオブジェクト</param>
    public void Sleep<T>(T obj) where T : GameObject {
        obj.Sleep();
    }

    /// <summary>該当オブジェクトを全てスリープする</summary>
    /// <typeparam name="T">GameObjectの派生クラス</typeparam>
    /// <param name="obj">寝かせたいオブジェクトのリスト</param>
    public void Sleep<T>(List<T> list) where T : GameObject {
        foreach(T obj in list) { obj.Sleep(); }
    }

    /// <summary>該当オブジェクトを描画する時に呼ぶ</summary>
    /// <param name="gameObject">GameObjectの派生クラス</param>
    /// <param name="spriteBatch">SpriteBatchの参照</param>
    public void Draw(GameObject gameObject, SpriteBatch spriteBatch) {
        gameObject.Draw(spriteBatch);
    }

    /// <summary>該当オブジェクトのリストを描画する時に呼ぶ</summary>
    /// <param name="gameObject">GameObjectの派生クラスのリスト</param>
    /// <param name="spriteBatch">SpriteBatchの参照</param>
    public void Draw<T>(List<T> list, SpriteBatch spriteBatch) where T : GameObject {
        foreach(GameObject gameObject in list) {
            gameObject.Draw(spriteBatch);
        }
    }


}
