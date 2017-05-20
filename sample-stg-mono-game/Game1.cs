using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace sample_stg_mono_game {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Input input;
        ObjectPool pool;
        SpriteFont debugFont;
        Texture2D sampleTexture;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;

            input = Input.instance;
            pool = ObjectPool.instance;

            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            debugFont = Content.Load<SpriteFont>("Debug");
            sampleTexture = Content.Load<Texture2D>("Sprite/ArrowBullet");
            pool.LoadContent(Content);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            base.Initialize();

            pool.Initialize();

            Player p = pool.WakeUp(pool.player);
            p.position = new Vector2(200, 200);
            Enemy e = pool.WakeUp(pool.enemys) as Enemy;
            e.position = new Vector2(100, 100);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            Content.Unload();
        }

        string debug;
        Vector2 test = Vector2.Zero;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            //入力の状態を更新する
            input.Update();

            //escキーが押されたら終了する
            if(input.exit) Exit();

            debug = "";
            debug += "x: " + input.x + "\n";
            debug += "y: " + input.y + "\n";
            debug += "shot: " + input.GetAction(Input.Action.shot) + "\n";
            debug += "shot: " + input.normalizedVector + "\n";

            pool.HitObjects(pool.player, pool.enemys);
            pool.player.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.DrawString(debugFont, debug, Vector2.Zero, Color.White);

            pool.Draw(pool.player, spriteBatch);
            pool.Draw(pool.enemys, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
