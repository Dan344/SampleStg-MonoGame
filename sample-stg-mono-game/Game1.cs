using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace sample_stg_mono_game {
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Input input;
        ObjectPool pool;
        GameManager manager;
        SpriteFont debugFont;
        Texture2D sampleTexture;
        SequenceManager sequence;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            sequence = new SequenceManager();

            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = CONST.AREA.W;
            graphics.PreferredBackBufferHeight = CONST.AREA.H;

            input = Input.instance;
            pool = ObjectPool.instance;
            manager = GameManager.instance;

            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
        }

        /// <summary>contentのロード。Initialize()より早く呼ばれる</summary>
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            debugFont = Content.Load<SpriteFont>("Debug");
            sampleTexture = Content.Load<Texture2D>("Sprite/ArrowBullet");
            pool.LoadContent(Content);
        }

        /// <summary>初期化</summary>
        protected override void Initialize() {
            Window.Title = CONST.TITLE;
            base.Initialize();

            pool.Initialize();

        }

        /// <summary>解放</summary>
        protected override void UnloadContent() {
            Content.Unload();
        }

        string debug;

        /// <summary>ゲームループのメインロジック</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            //入力の状態を更新する
            input.Update();

            //escキーが押されたら終了する
            if(input.exit) Exit();

            switch(sequence.state) {
                case SequenceManager.State.splash:
                    if(input.GetActionDown(Input.Action.submit)) {
                        sequence.state = SequenceManager.State.title;
                    }

                    break;

                case SequenceManager.State.title:
                    if(input.GetActionDown(Input.Action.submit)) {
                        sequence.state = SequenceManager.State.game;
                        manager.Initialize();
                    }

                    break;

                case SequenceManager.State.game:
                    manager.Update();

                    if(manager.state == GameManager.GameState.gameover) {
                        sequence.state = SequenceManager.State.title;
                    }

                    break;
            }

            SetDebugString();

            base.Update(gameTime);
        }

        /// <summary>ゲームループの描画</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            switch(sequence.state) {
                case SequenceManager.State.splash:
                    SplashDraw();
                    break;

                case SequenceManager.State.title:
                    TitleDraw();
                    break;

                case SequenceManager.State.game:
                    GameDraw();
                    break;
            }

            base.Draw(gameTime);
        }

        void SetDebugString() {
            debug = "";
            debug += "sequence: " + sequence.state + " score: " + manager.score + "\n";
            debug += "target: " + manager.target + " state: " + manager.state + "\n";
            debug += "left: " + manager.playerLeft + "\n";
            debug += "x: " + input.x + "\n";
            debug += "y: " + input.y + "\n";
            debug += "shot: " + input.GetAction(Input.Action.shot) + "\n";
            debug += "shot: " + input.normalizedVector + "\n";
        }

        void SplashDraw() {
            GraphicsDevice.Clear(Color.Red);
            spriteBatch.Begin();
            spriteBatch.DrawString(debugFont, debug, Vector2.Zero, Color.White);
            spriteBatch.End();
        }

        void TitleDraw() {
            GraphicsDevice.Clear(Color.BlueViolet);
            spriteBatch.Begin();
            spriteBatch.DrawString(debugFont, debug, Vector2.Zero, Color.White);
            spriteBatch.End();
        }

        void GameDraw() {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(debugFont, debug, Vector2.Zero, Color.White);
            pool.Draw(pool.playerBullets, spriteBatch);
            pool.Draw(pool.player, spriteBatch);
            pool.Draw(pool.enemyBullets, spriteBatch);
            pool.Draw(pool.enemys, spriteBatch);
            spriteBatch.End();
        }
    }
}
