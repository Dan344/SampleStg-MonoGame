using System.Collections;

/// <summary>
/// 1つのCoroutineを管理するクラス。一度インスタンス化したら何度でも流用できる。
/// </summary>
public class Coroutine {
    private IEnumerator obj;
    private bool isFinish;

    /// <summary>Coroutineを渡して毎フレーム呼び出すと、Coroutineの処理を繰り返す</summary>
    /// <param name="method">実行したいコルーチン</param>
    /// <returns>最後の処理完了時にfalse(MoveNextの返りと同じ)</returns>
    public bool Repeat(IEnumerator method) {
        if(obj == null) obj = method;

        if(obj?.MoveNext() ?? false) {
            return true;
        } else {
            Reset();
            return false;
        }
    }

    /// <summary>
    /// Coroutineを渡して毎フレーム呼び出すと、Coroutineの処理を1度だけ実行する。
    /// Reset()する事で、再度実行できる。
    /// </summary>
    /// <param name="method">実行したいコルーチン</param>
    /// <returns>最後の処理完了時にfalse(MoveNextの返りと同じ)</returns>
    public bool Start(IEnumerator method) {
        if(isFinish) return false;

        if(obj == null) obj = method;

        if(obj?.MoveNext() ?? false) {
            return true;
        } else {
            isFinish = true;
            return false;
        }
    }

    /// <summary>Coroutineの状態を初期状態に戻す</summary>
    public void Reset() {
        isFinish = false;
        obj = null;
    }
}
