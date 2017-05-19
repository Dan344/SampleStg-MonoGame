using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class Input {
    Keys upKey     = Keys.W;
    Keys leftKey   = Keys.A;
    Keys downKey   = Keys.S;
    Keys rightKey  = Keys.D;
    Keys shotKey   = Keys.J;
    Keys cancelKey = Keys.K;
    Keys pauseKey  = Keys.P;
    Keys submitKey = Keys.Enter;
    Keys ExitKey   = Keys.Escape;

    KeyboardState state;

    public Input() {
        state = Keyboard.GetState();
    }

    public void Update() {
        KeyboardState newState = Keyboard.GetState();

        //次のフレームでキーが離されたか等の処理を挟む予定

        state = newState;
    }

    public bool exit {
        get { return state.IsKeyDown(ExitKey); }
    }

    /// <summary>x方向の入力を取得(-1,0,1)</summary>
    public float x {
        get {
            float result = 0;

            if(state.IsKeyDown(leftKey)) {
                result -= 1;
            }

            if(state.IsKeyDown(rightKey)) {
                result += 1;
            }

            return result;
        }
    }

    /// <summary>y方向の入力を取得(-1,0,1)</summary>
    public float y {
        get {
            float result = 0;

            if(state.IsKeyDown(upKey)) {
                result -= 1;
            }

            if(state.IsKeyDown(downKey)) {
                result += 1;
            }

            return result;
        }
    }

    /// <summary>xy方向の入力を取得</summary>
    public Vector2 vector {
        get { return new Vector2(x, y); }
    }

    /// <summary>xy方向の入力を取得(単位ベクトル)</summary>
    public Vector2 normalizedVector {
        get { return Vector2.Normalize(vector); }
    }

    /// <summary>当該のアクションに必要なボタンが入力されているかを取得</summary>
    public bool GetAction(Action act) {
        switch(act) {
            case Action.shot:
                return state.IsKeyDown(shotKey);

            case Action.cancel:
                return state.IsKeyDown(cancelKey);

            case Action.pause:
                return state.IsKeyDown(pauseKey);

            case Action.submit:
                return state.IsKeyDown(shotKey) ? true :
                       state.IsKeyDown(submitKey);

            default:
                return false;
        }
    }

    public enum Action {
        submit,
        cancel,
        pause,
        shot,
    }

}
