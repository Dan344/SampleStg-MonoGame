using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

public class Player : CollisionObject {
    public float speed = 5;

    protected Input input;
    public Player() {
        input = Input.instance;
        //Debug.WriteLine(input);
    }

    /// <summary>
    /// 毎フレーム呼ぶ
    /// </summary>
    public void Update() {
        ControleMove();
    }

    public void Hit<T>(T other) where T : GameObject {

    }

    /// <summary>
    /// ユーザの入力に応じて移動する
    /// </summary>
    public void ControleMove() {
        base.Move(input.normalizedVector * speed);
    }
}
