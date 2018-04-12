using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class SpriteManager : Singleton<SpriteManager> {
    Dictionary<Sprite, Texture2D> texture = new Dictionary<Sprite, Texture2D>();
    Dictionary<Font, SpriteFont> spriteFont = new Dictionary<Font, SpriteFont>();

    private ContentManager content;

    public enum Sprite {
        ArrowBullet,
        BirdA,
        Player,
        VecBullet,
    }

    public enum Font {
        Debug,
    }

    public void SetContentManager(ContentManager content) {
        this.content = content;

        if(content != null) {
            content.RootDirectory = "Content";
        }
    }

    /// <summary>指定したスプライトを取得する。存在しない場合はロードする</summary>
    /// <param name="spriteName">取得したいスプライト</param>
    /// <returns>指定したスプライト</returns>
    public Texture2D Get(Sprite spriteName) {
        return texture.ContainsKey(spriteName)
               ? texture[spriteName] ?? LoadSprite(spriteName)
               : LoadSprite(spriteName);
    }

    /// <summary>指定したスプライトを取得する。存在しない場合はロードする</summary>
    /// <param name="spriteName">取得したいスプライト</param>
    /// <returns>指定したスプライト</returns>
    public SpriteFont Get(Font fontName) {
        return spriteFont.ContainsKey(fontName)
               ? spriteFont[fontName] ?? LoadFont(fontName)
               : LoadFont(fontName);
    }

    private Texture2D LoadSprite(Sprite spriteName) {
        // enumのToString()は遅い。気になる時は事前に変換テーブルを作成する。
        return texture[spriteName] = content.Load<Texture2D>("Sprite/" + spriteName.ToString());
    }

    private SpriteFont LoadFont(Font fontName) {
        // enumのToString()は遅い。気になる時は事前に変換テーブルを作成する。
        return spriteFont[fontName] = content.Load<SpriteFont>("Font/" + fontName.ToString());
    }
}
