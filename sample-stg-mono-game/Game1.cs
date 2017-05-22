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

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
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

            Player p = pool.WakeUp(pool.player);
            p?.Translate(new Vector2(200, 200));

            for(int i = 0; i < 8; ++i) {
                Enemy e = pool.WakeUp(pool.enemys);
                e?.Translate(new Vector2(100 * i, 100));
            }
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

            SetDebugString();

            manager.Update();

            //todo: 当たり判定チェックとUpdateで二重に呼ぶのは無駄な気がする
            pool.HitObjects(pool.playerBullets, pool.enemys);
            pool.HitObjects(pool.player, pool.enemys);
            pool.HitObjects(pool.player, pool.enemyBullets);

            pool.Update(pool.player);
            pool.Update(pool.enemys);
            pool.Update(pool.playerBullets);
            pool.Update(pool.enemyBullets);

            base.Update(gameTime);
        }

        void SetDebugString() {
            debug = "";
            debug += "x: " + input.x + "\n";
            debug += "y: " + input.y + "\n";
            debug += "shot: " + input.GetAction(Input.Action.shot) + "\n";
            debug += "shot: " + input.normalizedVector + "\n";
        }

        /// <summary>ゲームループの描画</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            spriteBatch.DrawString(debugFont, debug, Vector2.Zero, Color.White);

            pool.Draw(pool.playerBullets, spriteBatch);
            pool.Draw(pool.player, spriteBatch);
            pool.Draw(pool.enemyBullets, spriteBatch);
            pool.Draw(pool.enemys, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
