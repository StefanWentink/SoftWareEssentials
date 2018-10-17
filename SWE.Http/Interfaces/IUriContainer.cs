namespace SWE.Http.Interfaces
{
    using System;

    public interface IUriContainer
    {
        Uri Uri { get; set; }

        string ContentType { get; set; }

        string Format { get; set; }
    }
}