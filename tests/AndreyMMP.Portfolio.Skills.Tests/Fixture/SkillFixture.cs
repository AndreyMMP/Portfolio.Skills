namespace AndreyMMP.Portfolio.Skills.Tests.Fixture
{
    public static class SkillFixture
    {
        private static Random random = new Random();

        public static string CreateString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}