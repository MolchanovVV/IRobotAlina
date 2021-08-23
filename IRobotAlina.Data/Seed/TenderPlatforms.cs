using System.Collections.Generic;

namespace IRobotAlina.Data.Seed
{
    public class TenderPlatforms
    {
        public const string ZakupkiName = "Zakupki";
        public const string ZakupkiId = "58fbc48e-b203-42b0-8d08-caf75a1a4ed1";

        public static IEnumerable<(string Id, string Name)> All()
        {
            yield return (ZakupkiId, ZakupkiName);
        }
    }
}
