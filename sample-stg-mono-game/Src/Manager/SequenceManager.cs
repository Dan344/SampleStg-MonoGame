using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class SequenceManager {
    public State state = State.splash;

    public enum State {
        splash,
        title,
        game,
        option,
    }

    public SequenceManager() {

    }
}
