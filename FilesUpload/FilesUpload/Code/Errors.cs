using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilesUpload.Code
{
    /// <summary>
    /// Errors enumeration
    /// </summary>
    public enum Errors : int
    {
        [StringValue("No file is uploaded.")]
        NO_FILE_UPLOADED = 1,

        [StringValue("Upload path is null or empty.")]
        PATH_EMPTY = 2,

        [StringValue("Upload directory not found.")]
        DIRECTORY_NOT_FOUND = 3,

        [StringValue("File extension is not allowed.")]
        EXTENSION_NON_ALLOWED = 4,

        [StringValue("File type is not allowed.")]
        MIME_TYPE_NON_ALLOWED = 5,

        [StringValue("Minimum file size required.")]
        MIN_SIZE_REQUIRED = 6,

        [StringValue("Maximum file size exceeded.")]
        MAX_SIZE_EXCEEDED = 7,

        [StringValue("Http exception occurred.")]
        HTTP_EXCEPTION = 8,

        [StringValue("Exception occurred.")]
        EXCEPTION = 9,

        [StringValue("Unknown error occurred.")]
        UNKNOWN = 10
    }
}