using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

public class ObjectPool : Singleton<ObjectPool> {
    public Player player;
    public List<CollisionObject> playerBullets;
    public List<CollisionObject> enemys;
    public List<CollisionObject> enemyBullets;
    public List<CollisionObject> explosions;
    public List<CollisionObject> items;

    public Texture2D sampleTexture;

    public ObjectPool() {
        playerBullets = new List<CollisionObject>();
        enemys = new List<CollisionObject>();
        enemyBullets = new List<CollisionObject>();
        explosions = new List<CollisionObject>();
        items = new List<CollisionObject>();
    }

    public void Initialize() {
        player = new Player { position = new Vector2(200, 200)
        , sprite = sampleTexture
                              , spriteColor = Color.Green
                            };

        for(int i = 0; i < 10; ++i) {
            enemys.Add(new Enemy { position = new Vector2(100 * (i + 1), 300)
            , sprite = sampleTexture
                                   , spriteColor = Color.Red
                                 });
        }
    }

    public void LoadContent(ContentManager Content) {
        sampleTexture = Content.Load<Texture2D>("Sprite/ArrowBullet");
    }

    /// <summary>
    /// ２つのCollisionObjectのリストを渡すとそれぞれのリスト当たり判定処理が行われる
    /// </summary>
    /// <param name="list1">CollisionObjectのリスト</param>
    /// <param name="list2">CollisionObjectのリスト</param>
    public void HitObjects(List<CollisionObject> list1, List<CollisionObject> list2) {
        foreach(CollisionObject obj in list1) {
            HitObjects(obj, list2);
        }
    }

    /// <summary>
    /// 一つのCollisionObjectとCollisionObjectのリストを渡すと一つobjとリストの当たり判定の処理が行われう
    /// </summary>
    /// <param name="obj">CollisionObject</param>
    /// <param name="list">CollisionObjectのリスト</param>
    public void HitObjects(CollisionObject obj, List<CollisionObject> list) {
        if(!obj.isActive) { return; }

        foreach(CollisionObject other in list) {
            if(other.isActive && obj.CollisionCheck(other)) {
                obj.HitAction(other);
                other.HitAction(obj);
            }
        }
    }

    public T WakeUp<T>(T gameObject) where T : GameObject {
        if(!gameObject.isActive) {
            gameObject.WakeUp();
            return gameObject;
        } else {
            return null;
        }
    }

    public T WakeUp<T>(List<T> list) where T : GameObject {
        T result = null;

        foreach(T gameObject in list) {
            result = WakeUp(gameObject);

            if(result != null) break;
        }

        return result;
    }

    public void Draw(GameObject gameObject, SpriteBatch spriteBatch) {
        gameObject.Draw(spriteBatch);
    }

    public void Draw<T>(List<T> list, SpriteBatch spriteBatch) where T : GameObject {
        foreach(GameObject gameObject in list) {
            gameObject.Draw(spriteBatch);
        }
    }
}
