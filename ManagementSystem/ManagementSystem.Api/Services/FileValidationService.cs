using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.Dto;
using ManagementSystem.Api.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Services
{
    public class FileValidationService : IFileValidationService
    {
        private readonly ILogger<FileValidationService> _logger;

        public FileValidationService(ILogger<FileValidationService> logger)
        {
            _logger = logger;
        }

        public FileValidationResultDto Validate(IFormFile file)
        {
            var result = new FileValidationResultDto();

            try
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    file.CopyTo(ms);
                    result = Validate(ms.ToArray());
                    if (!result.IsValid)
                    {
                        return result;
                    }
                    ms.Dispose();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying validate file");
            }

            return result;
        }

        public FileValidationResultDto Validate(byte[] bytes)
        {
            var result = new FileValidationResultDto();
            try
            {
                if (bytes.Length >= 2097152)
                {
                    //Bigger than 2mb
                    return result;
                }
                var type = MimeTypeFrom(bytes);
                result.FileType = MIMETypesDictionary[type];
                if (result.FileType != FileTypes.Unknown)
                {
                    result.IsValid = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying validate file");
            }

            return result;
        }


        [DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
        static extern int FindMimeFromData(IntPtr pBC,
        [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.I1, SizeParamIndex=3)]
        byte[] pBuffer,
        int cbSize,
        [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed,
        int dwMimeFlags,
        out IntPtr ppwzMimeOut,
        int dwReserved);


        private string MimeTypeFrom(byte[] dataBytes)
        {
            if (dataBytes == null)
                throw new ArgumentNullException("dataBytes");
            string mimeRet = String.Empty;
            IntPtr suggestPtr = IntPtr.Zero, filePtr = IntPtr.Zero, outPtr = IntPtr.Zero;

            int ret = FindMimeFromData(IntPtr.Zero, null, dataBytes, dataBytes.Length, null, 0, out outPtr, 0);
            if (ret == 0 && outPtr != IntPtr.Zero)
            {
                //todo: this leaks memory outPtr must be freed
                return Marshal.PtrToStringUni(outPtr);
            }
            return mimeRet;
        }

        private static readonly Dictionary<string, FileTypes> MIMETypesDictionary = new Dictionary<string, FileTypes>
  {
        {"application/postscript", FileTypes.Unknown },
    {"audio/x-aiff", FileTypes.Unknown },
    {"text/plain", FileTypes.Txt },
    {"application/atom+xml", FileTypes.Unknown },
    {"audio/basic", FileTypes.Unknown },
    {"video/x-msvideo", FileTypes.Unknown },
    {"application/x-bcpio", FileTypes.Unknown },
    {"application/octet-stream", FileTypes.Unknown },
    {"image/bmp", FileTypes.Bmp },
    {"application/x-netcdf", FileTypes.Unknown },
    {"image/cgm", FileTypes.Unknown },
    {"application/x-cpio", FileTypes.Unknown },
    {"application/mac-compactpro", FileTypes.Unknown },
    {"application/x-csh", FileTypes.Unknown },
    {"text/css", FileTypes.Unknown },
    {"application/x-director", FileTypes.Unknown },
    {"video/x-dv", FileTypes.Unknown },
    {"image/vnd.djvu", FileTypes.Unknown },
    {"application/msword", FileTypes.Unknown },
    {"application/vnd.openxmlformats-officedocument.wordprocessingml.document", FileTypes.Unknown},
    {"application/vnd.openxmlformats-officedocument.wordprocessingml.template", FileTypes.Unknown },
    {"application/vnd.ms-word.document.macroEnabled.12", FileTypes.Unknown},
    {"application/vnd.ms-word.template.macroEnabled.12", FileTypes.Unknown},
    {"application/xml-dtd", FileTypes.Unknown },
    {"application/x-dvi", FileTypes.Unknown },
    {"text/x-setext", FileTypes.Unknown },
    {"application/andrew-inset", FileTypes.Unknown },
    {"image/gif", FileTypes.Gif },
    {"application/srgs", FileTypes.Unknown },
    {"application/srgs+xml", FileTypes.Unknown },
    {"application/x-gtar", FileTypes.Unknown },
    {"application/x-hdf", FileTypes.Unknown },
    {"application/mac-binhex40", FileTypes.Unknown },
    {"text/html", FileTypes.Html },
    {"x-conference/x-cooltalk", FileTypes.Unknown },
    {"image/x-icon", FileTypes.Unknown },
    {"text/calendar", FileTypes.Unknown },
    {"image/ief", FileTypes.Unknown },
    {"model/iges", FileTypes.Unknown },
    {"application/x-java-jnlp-file", FileTypes.Unknown },
    {"image/jp2", FileTypes.Unknown },
    {"image/jpeg", FileTypes.Jpeg },
    {"image/pjpeg", FileTypes.Jpeg },
    {"application/x-javascript", FileTypes.Unknown },
    {"audio/midi", FileTypes.Unknown },
    {"application/x-latex", FileTypes.Unknown },
    {"audio/x-mpegurl", FileTypes.Unknown },
    {"audio/mp4a-latm", FileTypes.Unknown },
    {"video/vnd.mpegurl", FileTypes.Unknown },
    {"video/x-m4v", FileTypes.Unknown },
    {"image/x-macpaint", FileTypes.Unknown },
    {"application/x-troff-man", FileTypes.Unknown },
    {"application/mathml+xml", FileTypes.Unknown },
    {"application/x-troff-me", FileTypes.Unknown },
    {"model/mesh", FileTypes.Unknown },
    {"application/vnd.mif", FileTypes.Unknown },
    {"video/quicktime", FileTypes.Unknown },
    {"video/x-sgi-movie", FileTypes.Unknown },
    {"audio/mpeg", FileTypes.Unknown },
    {"video/mp4", FileTypes.Unknown },
    {"video/mpeg", FileTypes.Unknown },
    {"application/x-troff-ms", FileTypes.Unknown },
    {"application/oda", FileTypes.Unknown },
    {"application/ogg", FileTypes.Unknown },
    {"image/x-portable-bitmap", FileTypes.Unknown },
    {"image/pict", FileTypes.Unknown },
    {"chemical/x-pdb", FileTypes.Unknown },
    {"application/pdf", FileTypes.Pdf },
    {"image/x-portable-graymap", FileTypes.Unknown },
    {"application/x-chess-pgn", FileTypes.Unknown },
    {"image/png", FileTypes.Png },
    {"image/x-png" , FileTypes.Png },
    {"image/x-portable-anymap", FileTypes.Unknown },
    {"image/x-portable-pixmap", FileTypes.Unknown },
    {"application/vnd.ms-powerpoint", FileTypes.Unknown },
    {"application/vnd.openxmlformats-officedocument.presentationml.presentation", FileTypes.Unknown},
    {"application/vnd.openxmlformats-officedocument.presentationml.template", FileTypes.Unknown},
    {"application/vnd.openxmlformats-officedocument.presentationml.slideshow", FileTypes.Unknown},
    {"application/vnd.ms-powerpoint.addin.macroEnabled.12", FileTypes.Unknown},
    {"application/vnd.ms-powerpoint.presentation.macroEnabled.12", FileTypes.Unknown},
    {"application/vnd.ms-powerpoint.template.macroEnabled.12", FileTypes.Unknown},
    {"application/vnd.ms-powerpoint.slideshow.macroEnabled.12", FileTypes.Unknown},
    {"image/x-quicktime", FileTypes.Unknown },
    {"audio/x-pn-realaudio", FileTypes.Unknown },
    {"image/x-cmu-raster", FileTypes.Unknown },
    {"application/rdf+xml", FileTypes.Unknown },
    {"image/x-rgb", FileTypes.Unknown },
    {"application/vnd.rn-realmedia", FileTypes.Unknown },
    {"application/x-troff", FileTypes.Unknown },
    {"text/rtf", FileTypes.Unknown },
    {"text/richtext", FileTypes.Unknown },
    {"text/sgml", FileTypes.Unknown },
    {"application/x-sh", FileTypes.Unknown },
    {"application/x-shar", FileTypes.Unknown },
    {"application/x-stuffit", FileTypes.Unknown },
    {"application/x-koan", FileTypes.Unknown },
    {"application/smil", FileTypes.Unknown },
    {"application/x-futuresplash", FileTypes.Unknown },
    {"application/x-wais-source", FileTypes.Unknown },
    {"application/x-sv4cpio", FileTypes.Unknown },
    {"application/x-sv4crc", FileTypes.Unknown },
    {"image/svg+xml", FileTypes.Unknown },
    {"application/x-shockwave-flash", FileTypes.Unknown },
    {"application/x-tar", FileTypes.Unknown },
    {"application/x-tcl", FileTypes.Unknown },
    {"application/x-tex", FileTypes.Unknown },
    {"application/x-texinfo", FileTypes.Unknown },
    {"image/tiff", FileTypes.Unknown },
    {"text/tab-separated-values", FileTypes.Unknown },
    {"application/x-ustar", FileTypes.Unknown },
    {"application/x-cdlink", FileTypes.Unknown },
    {"model/vrml", FileTypes.Unknown },
    {"application/voicexml+xml", FileTypes.Unknown },
    {"audio/x-wav", FileTypes.Unknown },
    {"image/vnd.wap.wbmp", FileTypes.Unknown },
    {"application/vnd.wap.wbxml", FileTypes.Unknown },
    {"text/vnd.wap.wml", FileTypes.Unknown },
    {"application/vnd.wap.wmlc", FileTypes.Unknown },
    {"text/vnd.wap.wmlscript", FileTypes.Unknown },
    {"application/vnd.wap.wmlscriptc", FileTypes.Unknown },
    {"image/x-xbitmap", FileTypes.Unknown },
    {"application/xhtml+xml", FileTypes.Unknown },
    {"application/vnd.ms-excel", FileTypes.Unknown },
    {"application/xml", FileTypes.Unknown },
    {"image/x-xpixmap", FileTypes.Unknown },
    {"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileTypes.Unknown},
    {"application/vnd.openxmlformats-officedocument.spreadsheetml.template", FileTypes.Unknown},
    {"application/vnd.ms-excel.sheet.macroEnabled.12", FileTypes.Unknown},
    {"application/vnd.ms-excel.template.macroEnabled.12", FileTypes.Unknown},
    {"application/vnd.ms-excel.addin.macroEnabled.12", FileTypes.Unknown},
    {"application/vnd.ms-excel.sheet.binary.macroEnabled.12", FileTypes.Unknown},
    {"application/xslt+xml", FileTypes.Unknown },
    {"application/vnd.mozilla.xul+xml", FileTypes.Unknown },
    {"image/x-xwindowdump", FileTypes.Unknown },
    {"chemical/x-xyz", FileTypes.Unknown },
    {"application/zip", FileTypes.Unknown }
  };
    }
}
