using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameManager : Singleton<GameManager> {
    /// <summary>ゲームロジックの経過時間。1から始まる</summary>
    public int elapsedFrame { get; private set; } = 0;

    public int score { get; set; } = 0;

    public void Update() {
        ++elapsedFrame;
    }
}
