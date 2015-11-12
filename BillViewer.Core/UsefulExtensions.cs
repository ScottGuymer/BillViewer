namespace BillViewer.Core
{
    using System;
    using System.Linq;

    using BillViewer.Core.Models.Bill;

    public static class UsefulExtensions
    {
        public static Subscription PackageName(this Bill theBill, PackageType packageType)
        {
            return theBill?.Package.Subscriptions.FirstOrDefault(x => x.Type == packageType.ToString());
        }

        public static T Parse<T>(this string input)
        {
            return (T)Enum.Parse(typeof(T), input);
        }
    }
}