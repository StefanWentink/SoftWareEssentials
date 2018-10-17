namespace SWE.Http.Models
{
    using SWE.Http.Constants;
    using SWE.Http.Interfaces;
    using SWE.Http.Utilities;
    using System;

    public class UriContainer : IUriContainer
    {
        public Uri Uri { get; set; }

        public string ContentType { get; set; }

        public string Format { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UriContainer"/> class.
        /// </summary>
        /// <param name="url"></param>
        public UriContainer(string url)
            : this(url, ExchangerConstants.Prefix)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UriContainer"/> class.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="prefix">Default <see cref="ExchangerConstants.Prefix"/></param>
        public UriContainer(string url, string prefix)
            : this(url, prefix, ExchangerConstants.ContentType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UriContainer"/> class.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="prefix">Default <see cref="ExchangerConstants.Prefix"/></param>
        /// <param name="contentType">Default <see cref="ExchangerConstants.ContentType"/></param>
        public UriContainer(string url, string prefix, string contentType)
            : this(url, prefix, contentType, ExchangerConstants.Format)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UriContainer"/> class.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="prefix">Default <see cref="ExchangerConstants.Prefix"/></param>
        /// <param name="contentType">Default <see cref="ExchangerConstants.ContentType"/></param>
        /// <param name="format">Default <see cref="ExchangerConstants.Format"/>.</param>
        public UriContainer(string url, string prefix, string contentType, string format)
        : this(new Uri(url), prefix, contentType, format)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UriContainer"/> class.
        /// </summary>
        /// <param name="uri"></param>
        public UriContainer(Uri uri)
            : this(uri, ExchangerConstants.Prefix)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UriContainer"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="prefix">Default <see cref="ExchangerConstants.Prefix"/></param>
        public UriContainer(Uri uri, string prefix)
            : this(uri, prefix, ExchangerConstants.ContentType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UriContainer"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="prefix">Default <see cref="ExchangerConstants.Prefix"/></param>
        /// <param name="contentType">Default <see cref="ExchangerConstants.ContentType"/></param>
        public UriContainer(Uri uri, string prefix, string contentType)
            : this(uri, prefix, contentType, ExchangerConstants.Format)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UriContainer"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="prefix">Default <see cref="ExchangerConstants.Prefix"/></param>
        /// <param name="contentType">Default <see cref="ExchangerConstants.ContentType"/></param>
        /// <param name="format">Default <see cref="ExchangerConstants.Format"/>.</param>
        public UriContainer(Uri uri, string prefix, string contentType, string format)
        {
            Format = UriUtilities.ConcatUriParts(prefix, format);
            ContentType = contentType;
            Uri = uri;
        }
    }
}