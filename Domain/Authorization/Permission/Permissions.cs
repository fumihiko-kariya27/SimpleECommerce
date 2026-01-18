using AspNetCoreGeneratedDocument;

namespace SimpleECommerce.Domain.Authorization.Permission
{
    public enum Permissions
    {
        // 商品の閲覧
        ViewProduct,

        // 商品の新規登録
        RegisterNewProdduct,

        // 商品の更新
        UpdateProduct,

        // 商品の削除
        DeleteProduct,

        // 新規注文
        NewOrder,

        // 注文の更新
        UpdateOrder,

        // 注文のキャンセル
        CancelOrder
    }
}
