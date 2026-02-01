namespace SimpleECommerce.Domain.Auth
{
    public class Permission
    {
        private static readonly int Min = 1;

        public int Id { get; }

        public string Behavior { get; }

        public Permission(int id, string behavior)
        {
            if (id < Min)
            {
                throw new ArgumentOutOfRangeException($"IDは{Min}以上の値を設定しなければいけません");
            }

            ArgumentNullException.ThrowIfNullOrWhiteSpace(behavior);

            Id = id;
            Behavior = behavior;
        }
    }
}
