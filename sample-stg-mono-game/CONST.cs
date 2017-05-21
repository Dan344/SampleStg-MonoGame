public class CONST {
    public const string TITLE = "SampleSTG";

    public class AREA {
        public const int W = 1024;
        public const int H = 768;

        public const int LEFT = 0;
        public const int RIGHT = W - 1;
        public const int TOP = 0;
        public const int BOTTOM = H - 1;

        /// <summary>自機の移動可能範囲</summary>
        public const int MOVEBLE_TOP = TOP + 130;
        /// <summary>自機の移動可能範囲</summary>
        public const int MOVEBLE_BOTTOM = BOTTOM - 80;
        /// <summary>自機の移動可能範囲</summary>
        public const int MOVEBLE_LEFT = LEFT + 40;
        /// <summary>自機の移動可能範囲</summary>
        public const int MOVEBLE_RIGHT = RIGHT - 40;

        /// <summary>自機弾を削除する高さ</summary>
        public const int P_BULLET_DELETE_LINE = TOP + 20;

        /// <summary>敵機弾を削除するMARGIN</summary>
        public const int E_BULLET_DELETE_MARGIN = 20;
    }
}
