using System.Collections;

public abstract class EnemyAction<T> : BaseAction<Enemy> {

    Coroutine normalAction = new Coroutine();

    public abstract IEnumerator NormalAction();

    public override void Init(Enemy gameObject) {
        normalAction?.Reset();
        base.Init(gameObject);
    }

    public override void Update() {
        normalAction.Repeat(NormalAction());
        System.Console.WriteLine("hoge");
    }


}
