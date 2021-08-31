using GCSEmulator.Data.Models.Buckets;

namespace GCSEmulator.Dtos.Buckets
{
    /// <summary>
    ///  The bucket's website configuration, controlling how the service behaves when accessing bucket contents as a web site
    /// </summary>
    public class WebsiteDto
    {
        /// <summary>
        /// If the requested object path is missing, the service will ensure the path has a trailing '/', append this suffix,
        /// and attempt to retrieve the resulting object. This allows the creation of index.html objects to represent directory pages.
        /// </summary>
        public string MainPageSuffix { get; set; }

        /// <summary>
        /// If the requested object path is missing, and any mainPageSuffix object is missing, if applicable,
        /// the service will return the named object from this bucket as the content for a 404 Not Found result.
        /// </summary>
        public string NotFoundPage { get; set; }

        public static WebsiteDto Create(WebsiteSettings settings)
        {
            return new WebsiteDto
            {
                MainPageSuffix = settings.MainPageSuffix,
                NotFoundPage = settings.NotFoundPage
            };
        }
    }
}