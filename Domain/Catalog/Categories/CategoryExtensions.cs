using System.ComponentModel.DataAnnotations;

namespace SimpleECommerce.Domain.Catalog.Categories
{
    internal static class CategoryExtensions
    {
        // アプリ内でのカテゴリ表示体系を定義する
        internal static string ToCode(this CategoryId category)
        {
            return ((int)category).ToString("D2");
        }

        internal static string GetName(this CategoryId category)
            => category.GetAttribute<DisplayAttribute>()?.Name ?? category.ToString();

        internal static string GetDescription(this CategoryId category)
            => category.GetAttribute<DisplayAttribute>()?.Description ?? category.ToString();

        // CategoryIdに指定された属性があった場合、その値を取得する
        private static T? GetAttribute<T>(this CategoryId category) where T : Attribute
        {
            return category
                .GetType()
                .GetField(category.ToString())?
                .GetCustomAttributes(typeof(T), false)
                .FirstOrDefault() as T;
        }
    }
}
