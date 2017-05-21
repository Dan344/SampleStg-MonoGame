using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class Input : Singleton<Input> {
    Keys upKey     = Keys.W;
    Keys leftKey   = Keys.A;
    Keys downKey   = Keys.S;
    Keys rightKey  = Keys.D;
    Keys shotKey   = Keys.J;
    Keys cancelKey = Keys.K;
    Keys pauseKey  = Keys.P;
    Keys submitKey = Keys.Enter;
    Keys ExitKey   = Keys.Escape;

    KeyboardState prevState;
    KeyboardState state;


    public Input() {
        state = Keyboard.GetState();
    }

    public void Update() {
        prevState = state;
        state = Keyboard.GetState();
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

    /// <summary>xy方向の入力を取得(単位ベクトル)。vectorが0の時は0</summary>
    public Vector2 normalizedVector {
        get {
            Vector2 v = vector;
            return v == Vector2.Zero ? v : Vector2.Normalize(vector);
        }
    }

    /// <summary>当該のアクションに必要なボタンが入力されているかを取得</summary>
    public bool GetAction(Action act) {
        switch(act) {
            case Action.shot:
                return IsKeyHold(shotKey);

            case Action.cancel:
                return IsKeyHold(cancelKey);

            case Action.pause:
                return IsKeyHold(pauseKey);

            case Action.submit:
                return IsKeyHold(shotKey) ? true :
                       IsKeyHold(submitKey);

            default:
                return false;
        }
    }

    /// <summary>当該のアクションに必要なボタンが押されたフレームのみtrue</summary>
    public bool GetActionDown(Action act) {
        switch(act) {
            case Action.shot:
                return IsKeyDown(shotKey);

            case Action.cancel:
                return IsKeyDown(cancelKey);

            case Action.pause:
                return IsKeyDown(pauseKey);

            case Action.submit:
                return IsKeyDown(shotKey) ? true :
                       IsKeyDown(submitKey);

            default:
                return false;
        }
    }

    /// <summary>当該のアクションに必要なボタンが離されたフレームのみtrue</summary>
    public bool GetActionUp(Action act) {
        switch(act) {
            case Action.shot:
                return IsKeyUp(shotKey);

            case Action.cancel:
                return IsKeyUp(cancelKey);

            case Action.pause:
                return IsKeyDown(pauseKey);

            case Action.submit:
                return IsKeyUp(shotKey) ? true :
                       IsKeyUp(submitKey);

            default:
                return false;
        }
    }

    bool IsKeyHold(Keys key) => state.IsKeyDown(key);
    bool IsKeyDown(Keys key) => state.IsKeyDown(key) && !prevState.IsKeyDown(key);
    bool IsKeyUp(Keys key) => state.IsKeyUp(key) && prevState.IsKeyDown(key);

    public enum Action {
        submit,
        cancel,
        pause,
        shot,
    }
}
