namespace IndexMarket.Application.Extantions;
public static class PhoneExtantionNumber
{
    public static string PhoneNumberCleaner(this string phoneNumber)
    {
        if (phoneNumber == null)
        {
            return phoneNumber;
        }

        phoneNumber = phoneNumber.Replace("+", string.Empty);
        phoneNumber = phoneNumber.Replace("-", string.Empty);
        phoneNumber = phoneNumber.Replace(" ", string.Empty);
        phoneNumber = phoneNumber.Replace("(", string.Empty);
        phoneNumber = phoneNumber.Replace(")", string.Empty);

        return phoneNumber;
    }
}
