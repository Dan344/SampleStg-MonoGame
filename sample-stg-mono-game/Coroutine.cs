using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Collections.Generic;

public class Coroutine : Singleton<Coroutine> {
    List<IEnumerator> threads = new List<IEnumerator>();

    /// <summary>開始したいコルーチンを渡すと開始する</summary>
    /// <param name="coroutine"></param>
    public void Start(IEnumerator coroutine) {
        if(!threads.Contains(coroutine)) {
            threads.Add(coroutine);
        }
    }

    public void Update() {
        //完了したコルーチンを削除しつつ全走査
        threads.RemoveAll(c => !c.MoveNext());
    }
}
