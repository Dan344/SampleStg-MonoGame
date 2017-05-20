using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class ObjectPool : Singleton<ObjectPool> {
    public Player player;
    public List<GameObject> playerBullets;
    public List<GameObject> enemys;
    public List<GameObject> enemyBullets;
    public List<GameObject> explosions;
    public List<GameObject> items;

    public ObjectPool() {
        player = new Player();
        playerBullets = new List<GameObject>();
        enemys = new List<GameObject>();
        enemyBullets = new List<GameObject>();
        explosions = new List<GameObject>();
        items = new List<GameObject>();


        //for(int i=0; i<1000; ++i) {
        //    playerBulletPool

        //}
    }
}
