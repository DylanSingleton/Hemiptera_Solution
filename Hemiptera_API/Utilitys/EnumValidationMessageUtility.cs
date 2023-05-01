namespace Hemiptera_API.Utilitys
{
    public static class EnumValidationMessageUtility
    {
        public static string GetEnumValidationMessage<TEnum>()
            where TEnum : Enum
        {
            var enumType = typeof(TEnum);
            var enumValues = Enum.GetValues(enumType)
                .Cast<TEnum>()
                .Select((value, index) => $"{index + 1}. {value}")
                .ToList();

            var message = $"Value must be one of the following: {string.Join(", ", enumValues)}";
            return message;
        }
    }
}