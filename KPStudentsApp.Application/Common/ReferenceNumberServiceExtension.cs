using KPStudentsApp.Application.Interfaces;

namespace KPStudentsApp.Application.Common
{
    public static class ReferenceNumberServiceExtension
    {
        public static async Task<string> GetReferenceNumber(this IReferenceNumberService referenceNumberService, string dimension, string format)
        {
            var range = await referenceNumberService.IncreamentNextValAsync(dimension?.ToUpperInvariant());
            return dimension + range.Item1.ToString(format ?? "");
        }
    }
}
