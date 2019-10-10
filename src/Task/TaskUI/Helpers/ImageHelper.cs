using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TaskUI.Helpers
{
    public class ImagesHelper
    {
        public static string GetPhotosUrl(HttpPostedFileBase source, string path)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));


            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(source.FileName);
            source.SaveAs(path + fileName);
            var ThisFileUrl = "~/Photos/" + fileName;
            return ThisFileUrl;
        }

        public static void DeleteRange(IEnumerable<string> paths, string rootPath)
        {
            foreach (var photo in paths)
            {
                if (File.Exists(Path.Combine(rootPath, photo)))
                {
                    File.Delete(Path.Combine(rootPath, photo));
                }
            }
        }
    }
}