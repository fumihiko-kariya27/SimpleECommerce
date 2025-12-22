using System.ComponentModel.DataAnnotations;

namespace SimpleECommerce.Domain.Catalog.Categories
{
    // カテゴリ毎のIDを定義する
    public enum CategoryId
    {
        [Display(Name = "未分類", Description = "カテゴリが未設定の商品")]
        None = 0,

        // 生活系
        [Display(Name = "食品", Description = "食料品・加工食品・生鮮食品など")]
        Food = 1,

        [Display(Name = "飲料", Description = "ジュース・お茶・コーヒーなどの飲料全般")]
        Beverage = 2,

        [Display(Name = "日用品", Description = "生活雑貨・消耗品・家庭用品など")]
        DailyGoods = 3,

        [Display(Name = "ヘルスケア", Description = "健康用品・衛生用品・サプリメントなど")]
        HealthCare = 4,

        // ファッション
        [Display(Name = "メンズファッション", Description = "男性向け衣類・ファッション小物")]
        FashionMen = 10,

        [Display(Name = "レディースファッション", Description = "女性向け衣類・ファッション小物")]
        FashionWomen = 11,

        [Display(Name = "キッズファッション", Description = "子供向け衣類・ファッション小物")]
        FashionKids = 12,

        [Display(Name = "シューズ", Description = "靴・サンダル・ブーツなど")]
        Shoes = 13,

        [Display(Name = "アクセサリー", Description = "時計・ジュエリー・小物類")]
        Accessories = 14,

        // 家電・ガジェット
        [Display(Name = "家電", Description = "家庭用電化製品全般")]
        HomeAppliances = 20,

        [Display(Name = "パソコン", Description = "デスクトップ・ノートPC・周辺機器")]
        PC = 21,

        [Display(Name = "スマートフォン", Description = "スマホ本体・アクセサリー")]
        Smartphone = 22,

        [Display(Name = "カメラ", Description = "デジタルカメラ・レンズ・撮影機材")]
        Camera = 23,

        [Display(Name = "オーディオ", Description = "イヤホン・スピーカー・音響機器")]
        Audio = 24,

        // ホビー・エンタメ
        [Display(Name = "ゲーム", Description = "ゲーム機・ソフト・周辺機器")]
        Games = 30,

        [Display(Name = "おもちゃ", Description = "玩具・フィギュア・知育玩具")]
        Toys = 31,

        [Display(Name = "書籍", Description = "本・雑誌・コミック")]
        Books = 32,

        [Display(Name = "音楽", Description = "CD・楽器・音楽関連商品")]
        Music = 33,

        [Display(Name = "映画", Description = "DVD・Blu-ray・映像作品")]
        Movies = 34,

        // 家具・インテリア
        [Display(Name = "家具", Description = "テーブル・椅子・収納家具など")]
        Furniture = 40,

        [Display(Name = "インテリア", Description = "雑貨・装飾品・照明など")]
        Interior = 41,

        [Display(Name = "キッチン用品", Description = "調理器具・食器・キッチン雑貨")]
        Kitchen = 42,

        // スポーツ・アウトドア
        [Display(Name = "スポーツ用品", Description = "スポーツウェア・用具・トレーニング用品")]
        Sports = 50,

        [Display(Name = "アウトドア", Description = "キャンプ用品・登山用品・レジャー用品")]
        Outdoor = 51,

        [Display(Name = "旅行用品", Description = "スーツケース・旅行小物・トラベルグッズ")]
        Travel = 52,

        // その他
        [Display(Name = "美容", Description = "化粧品・スキンケア・ヘアケア用品")]
        Beauty = 60,

        [Display(Name = "ペット用品", Description = "ペットフード・ケア用品・アクセサリー")]
        Pet = 61,

        [Display(Name = "自動車用品", Description = "カーアクセサリー・メンテナンス用品")]
        Automotive = 62,

        [Display(Name = "DIY・工具", Description = "工具・資材・DIY関連用品")]
        DIY = 63,
    }
}
