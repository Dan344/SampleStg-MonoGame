using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
        if(!isActive) return;

        ControleMove();
    }

    public override void HitAction(CollisionObject other) {
        Debug.WriteLine("hogeeeeeeeeeee");
        base.HitAction(other);
    }

    /// <summary>
    /// ユーザの入力に応じて移動する
    /// </summary>
    public void ControleMove() {
        base.Move(input.normalizedVector * speed);
    }
}
