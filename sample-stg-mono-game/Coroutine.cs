using System.Collections;

/// <summary>
/// Coroutineを呼びやすくするだけの静的クラス
/// </summary>
public static class Coroutine {
    /// <summary>置く場所と処理を渡すと、繰り返しコルーチンを実行する。</summary>
    /// <param name="local">instanceの参照</param>
    /// <param name="method">実行したいコルーチン</param>
    public static void Repeat(ref IEnumerator local, IEnumerator method) {
        if(local == null) {
            local = method;
        }

        if(local != null) {
            if(!local.MoveNext()) {
                local = null;
            }
        }
    }

    /// <summary>置く場所と処理を渡すと、一度コルーチンを実行する。</summary>
    /// <param name="local">instanceの参照</param>
    /// <param name="method">実行したいコルーチン</param>
    public static void Start(ref IEnumerator local, IEnumerator method) {
        if(local == null) {
            local = method;
        }

        if(local != null) {
            local.MoveNext();
        }
    }
}
