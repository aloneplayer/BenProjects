using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using FilesUpload.Code;

namespace FilesUpload.Code
{
    public class Uploader
    {
        #region Private Properties

        private List<string> _allowedExtensions = new List<string>();
        private List<string> _allowedMimeTypes = new List<string>();

        private UploadError _uploadError = new UploadError();
        private PostedFile _postedFile = new PostedFile();
        private UploadedFile _uploadedFile = new UploadedFile();

        private List<PostedFile> _postedFiles = new List<PostedFile>();
        private List<UploadedFile> _uploadedFiles = new List<UploadedFile>();

        #endregion

        #region Public Property Accessors

        #region General Settings

        /// <summary>
        /// Gets or sets the path where file should
        /// be uploaded exculding the filename.
        /// </summary>
        public string UploadPath { get; set; }

        /// <summary>
        /// Gets or sets the name which should be used to save 
        /// the uploaded file with extension.
        /// </summary>
        public string UploadName { get; set; }

        /// <summary>
        /// Gets or sets the prefix to the filename.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the suffix to the filename.
        /// </summary>
        public string Suffix { get; set; }

        #endregion

        #region Conditional Settings

        /// <summary>
        /// Gets or sets the minimum size of the file in bytes.
        /// </summary>
        public int MinSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum size of the file in bytes.
        /// </summary>
        public int MaxSize { get; set; }

        /// <summary>
        /// Gets or sets whether a file is to overwrite or not.
        /// </summary>
        public bool IsOverwrite { get; set; }

        /// <summary>
        /// Gets or sets whether a file name is to encrypt or not.
        /// </summary>
        public bool IsEncryptName { get; set; }

        /// <summary>
        /// Gets or sets whether a file name is to lower or not.    
        /// </summary>
        public bool IsLowerName { get; set; }

        /// <summary>
        /// Gets or sets whether space is to be removed from a file name or not.
        /// </summary>
        public bool IsRemoveSpace { get; set; }

        /// <summary>
        /// Gets or sets the allowed file extensions with period.
        /// </summary>
        public List<string> AllowedExtensions
        {
            get { return _allowedExtensions; }
            set { _allowedExtensions = value; }
        }

        /// <summary>
        /// Gets or sets the allowed mime types.
        /// </summary>
        public List<string> AllowedMimeTypes
        {
            get { return _allowedMimeTypes; }
            set { _allowedMimeTypes = value; }
        }

        #endregion

        #region Object Properties

        /// <summary>
        /// Gets the object of UploadError class.
        /// </summary>
        public UploadError UploadError
        {
            get { return this._uploadError; }
            private set { this._uploadError = value; }
        }

        /// <summary>
        /// Gets the object of PostedFile class.
        /// </summary>
        public PostedFile PostedFile
        {
            get { return this._postedFile; }
            private set { this._postedFile = value; }
        }

        /// <summary>
        /// Gets the object of UploadedFile class.
        /// </summary>
        public UploadedFile UploadedFile
        {
            get { return this._uploadedFile; }
            private set { this._uploadedFile = value; }
        }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the HttpPostedFile object.
        /// </summary>
        /// <param name="file">Html file input name.</param>
        /// <returns>HttpPostedFile object or null.</returns>
        public HttpPostedFile GetHttpPostedFile(string file)
        {
            HttpPostedFile postedFile = null;

            try
            {
                postedFile = HttpContext.Current.Request.Files[file] as HttpPostedFile;
            }
            catch (HttpException ex)
            {
                this.SetError((int)Errors.HTTP_EXCEPTION, (string)ex.Message);
            }
            catch (Exception ex)
            {
                this.SetError((int)Errors.EXCEPTION, (string)ex.Message);
            }

            return postedFile;
        }

        /// <summary>
        /// Uploads the posted file.
        /// </summary>
        /// <param name="file">Html file input name.</param>
        /// <returns>True if upload succeeds, otherwise false.</returns>
        public bool DoUpload(string file)
        {
            HttpPostedFile postedFile = this.GetHttpPostedFile(file);

            if (postedFile != null)
            {
                return this.DoUpload(postedFile);
            }

            return false;
        }

        /// <summary>
        /// Uploads the posted file.
        /// </summary>
        /// <param name="postedFile">HttpPostedFile object.</param>
        /// <returns>True if upload succeeds, otherwise false.</returns>
        public bool DoUpload(HttpPostedFile postedFile)
        {
            if (postedFile == null)
            {
                this.SetError(Errors.NO_FILE_UPLOADED);
                return false;
            }

            // Set Posted File Info
            this.SetPostedFileInfo(postedFile);

            // Check validity
            if (!this.IsValidFile(postedFile))
            {
                return false;
            }

            // Now do the actual upload...
            try
            {
                string saveFile = Path.Combine(this.UploadPath, this.PrepareFileName(Path.GetFileName(postedFile.FileName)));
                postedFile.SaveAs(saveFile);

                // Set the uploaded file info
                this.SetUploadedFileInfo(saveFile, postedFile);

                return true;
            }
            catch (Exception ex)
            {
                this.SetError((int)Errors.EXCEPTION, ex.Message);
                return false;
            }
        }

        #endregion

        #region Private Methods

        #region IsValid Methods
        /// <summary>
        /// Check if file and file settings all are valid.
        /// </summary>
        /// <param name="postedFile">Posted file.</param>
        /// <returns>Boolean</returns>
        private bool IsValidFile(HttpPostedFile postedFile)
        {
            // If upload path is not set or invalid
            if (!this.IsValidUploadPath(postedFile))
                return false;

            // If the file size is shorter
            if (this.MinSize > 0 && postedFile.ContentLength < this.MinSize)
            {
                this.SetError(Errors.MIN_SIZE_REQUIRED);
                return false;
            }

            // If the file size is greater
            if (this.MaxSize != 0 && postedFile.ContentLength > this.MaxSize)
            {
                this.SetError(Errors.MAX_SIZE_EXCEEDED);
                return false;
            }

            // If the file extension is not allowed
            if (this.AllowedExtensions.Count > 0 && !this.IsValidFileExtension(postedFile))
            {
                this.SetError(Errors.EXTENSION_NON_ALLOWED);
                return false;
            }

            // If the file mime type is not allowed
            if (this.AllowedMimeTypes.Count > 0 && !this.IsValidFileMimeType(postedFile))
            {
                this.SetError(Errors.MIME_TYPE_NON_ALLOWED);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate file uploaded path.
        /// </summary>
        /// <param name="postedFile">Posted file.</param>
        /// <returns>boolean.</returns>
        private bool IsValidUploadPath(HttpPostedFile postedFile)
        {
            // If upload path is null or empty
            if (String.IsNullOrEmpty(this.UploadPath))
            {
                this.SetError(Errors.PATH_EMPTY);
                return false;
            }

            // If upload path does not exist
            if (!Directory.Exists(this.UploadPath))
            {
                this.SetError(Errors.DIRECTORY_NOT_FOUND);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate file extension with allowed extensions.
        /// </summary>
        /// <param name="postedFile">postedFile object, HttpPostedFile.</param>
        /// <returns>boolean</returns>
        private bool IsValidFileExtension(HttpPostedFile postedFile)
        {
            //if (this.AllowedExtensions.Contains(this.GetFileExtension(postedFile.FileName)))
            if (this.AllowedExtensions.Exists(
                    ext => ext.Equals(
                        this.GetFileExtension(postedFile.FileName), StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Validate file mime type with allowed mime types.
        /// </summary>
        /// <param name="postedFile">Posted file.</param>
        /// <returns>boolean</returns>
        private bool IsValidFileMimeType(HttpPostedFile postedFile)
        {
            //if (this.AllowedMimeTypes.Contains(postedFile.ContentType))
            if (this.AllowedMimeTypes.Exists(
                    mime => mime.Equals(
                        postedFile.ContentType, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
            else
                return false;
        }
        #endregion

        /// <summary>
        /// Gets or sets whether the posted file is image or not from MIME type.
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        private bool IsImage(HttpPostedFile postedFile)
        {
            string mime = postedFile.ContentType.Substring(0, postedFile.ContentType.LastIndexOf("/"));

            return mime.Equals("image");
        }

        /// <summary>
        /// Gets or sets the error code and message.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        private void SetError(int code, string message)
        {
            this.UploadError.Code = code;
            this.UploadError.Message = message;
        }

        /// <summary>
        /// Gets or sets the error code and message.
        /// </summary>
        /// <param name="error"></param>
        private void SetError(Errors error)
        {
            this.UploadError.Code = (int)error;
            this.UploadError.Message = error.GetStringValue();
        }

        /// <summary>
        /// Sets the posted file information.
        /// </summary>
        /// <param name="postedFile">HttpPostedFile object</param>
        private void SetPostedFileInfo(HttpPostedFile postedFile)
        {
            this.PostedFile.FileName = Path.GetFileName(postedFile.FileName);
            this.PostedFile.RawName = Path.GetFileNameWithoutExtension(postedFile.FileName);
            this.PostedFile.FileExtension = Path.GetExtension(postedFile.FileName);
            this.PostedFile.MimeType = postedFile.ContentType;
            this.PostedFile.FullPath = Path.GetFullPath(postedFile.FileName);
            this.PostedFile.FilePath = Path.GetDirectoryName(postedFile.FileName);
            this.PostedFile.FileSize = postedFile.ContentLength;
            this.PostedFile.IsImage = this.IsImage(postedFile);
        }

        /// <summary>
        /// Sets the uploaded file information.
        /// </summary>
        /// <param name="saveFile">Save file path within file name</param>
        /// <param name="postedFile">HttpPostedFile object</param>
        private void SetUploadedFileInfo(string saveFile, HttpPostedFile postedFile)
        {
            this.UploadedFile.FileName = Path.GetFileName(saveFile);
            this.UploadedFile.RawName = Path.GetFileNameWithoutExtension(saveFile);
            this.UploadedFile.FileExtension = Path.GetExtension(saveFile);
            this.UploadedFile.MimeType = postedFile.ContentType;
            this.UploadedFile.FullPath = Path.GetFullPath(saveFile);
            this.UploadedFile.FilePath = Path.GetDirectoryName(saveFile);
            this.UploadedFile.FileSize = (int)new FileInfo(saveFile).Length;
            this.UploadedFile.IsImage = this.IsImage(postedFile);
        }

        /// <summary>
        /// Prepare the file name to upload.
        /// </summary>
        /// <param name="filename">Filename with extension</param>
        /// <returns>File name to upload, String</returns>
        private string PrepareFileName(string filename)
        {
            // If file name is given then
            if (!String.IsNullOrEmpty(this.UploadName))
            {
                filename = this.UploadName;
            }

            string rawname = this.GetFileNameWithoutExtension(filename);
            string extension = this.GetFileExtension(filename);

            string saveFilename = "";

            // Prefix
            if (!String.IsNullOrEmpty(this.Prefix))
            {
                saveFilename = this.Prefix;
            }

            // Raw name
            saveFilename += rawname;

            // Suffix
            if (!String.IsNullOrEmpty(this.Suffix))
            {
                saveFilename += this.Suffix;
            }

            // Make lower
            if (this.IsLowerName)
            {
                saveFilename = saveFilename.ToLower();
            }

            // Remove space
            if (this.IsRemoveSpace)
            {
                saveFilename = saveFilename.Replace(" ", "");
            }

            // Encrypt name (MD5)
            if (this.IsEncryptName)
            {
                MD5 md5 = new MD5CryptoServiceProvider();

                // Conver the original string to bytes; then create the hash
                Byte[] originalBytes = Encoding.Default.GetBytes(saveFilename);
                Byte[] encodedBytes = md5.ComputeHash(originalBytes);

                // Bytes to string            
                saveFilename = Regex.Replace(BitConverter.ToString(encodedBytes), "-", "").ToLower();
            }

            // Not overwrite
            if (!this.IsOverwrite)
            {
                if (File.Exists(Path.Combine(this.UploadPath, (saveFilename + extension))))
                {
                    saveFilename += "_" + DateTime.Now.Ticks;
                }
            }

            // Extension
            saveFilename += extension;

            return saveFilename;
        }

        /// <summary>
        /// Get the file extension from file name with period.
        /// </summary>
        /// <param name="fileName">Filename to get extension</param>
        /// <returns>File extension with period</returns>
        private string GetFileExtension(string fileName)
        {
            string extension = "." + this.GetFileExtensionWithoutPeriod(fileName);

            return extension;
        }

        /// <summary>
        /// Get the file extension from file name without period
        /// </summary>
        /// <param name="fileName">Filename to get extension</param>
        /// <returns>File extension without period</returns>
        private string GetFileExtensionWithoutPeriod(string fileName)
        {
            string extension = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();

            return extension;
        }

        private string GetFileNameWithoutExtension(string fileName)
        {
            string name = fileName.Substring(0, fileName.LastIndexOf("."));

            return name;
        }


        #endregion
    }

   

    /// <summary>
    /// UploadError class which holds error code and error message
    /// while trying to upload.
    /// </summary>
    public class UploadError
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// PostedFile class which holds different information of posted file.
    /// </summary>
    public class PostedFile
    {
        #region Posted File Properties

        /// <summary>
        /// Gets or sets the file name with extension.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file name excluding extension.
        /// </summary>
        public string RawName { get; set; }

        /// <summary>
        /// Gets or sets the file extension with period.
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Gets or sets the file MIME type.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the absolute path including the file name.
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// Gets or sets the absolute path to the file excluding the file name.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the size of the file in bytes.
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// Gets or sets whether the file is image or not.
        /// </summary>
        public bool IsImage { get; set; }

        #endregion
    }

    /// <summary>
    /// UploadedFile class which holds different information of uploaded file.
    /// </summary>
    public class UploadedFile
    {
        #region Uploaded File Properties

        /// <summary>
        /// Gets or sets the file name with extension.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file name excluding extension.
        /// </summary>
        public string RawName { get; set; }

        /// <summary>
        /// Gets or sets the file extension with period.
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Gets or sets the file MIME type.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the absolute path including the file name.
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// Gets or sets the absolute path to the file excluding the file name.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the size of the file in bytes.
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// Gets or sets whether the file is image or not.
        /// </summary>
        public bool IsImage { get; set; }

        #endregion
    }
}