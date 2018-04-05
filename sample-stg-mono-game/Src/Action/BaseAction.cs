public abstract class BaseAction<T> where T : GameObject {
    /// <summary>操作するGameObject</summary>
    protected T gameObject;

    protected Input input;
    protected ObjectPool pool;
    protected GameManager manager;

    public BaseAction() {
        input = Input.instance;
        pool = ObjectPool.instance;
        manager = GameManager.instance;
    }

    public virtual void Init(T gameObject) {
        this.gameObject = gameObject;
    }

    public abstract void Update();
}
