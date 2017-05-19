public class Singleton<T> where T : Singleton<T> {
    protected static T _instance;
    public static T instance {
        get {
            if(_instance == null) {
                _instance = new Singleton<T>() as T;
            }

            return _instance;
        }
    }
}
