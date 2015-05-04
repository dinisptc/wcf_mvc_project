using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTIC_IT.Libraries;

namespace MvcTIC_IT.Controllers
{
        public class ThumbImoveisResultES : ActionResult
        {


            public ThumbImoveisResultES(string virtualPath)
            {
                this.VirtualPath = virtualPath;
            }

            public string VirtualPath { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                context.HttpContext.Response.ContentType = "image/bmp";
                settings set = new settings();
                string imagesDir = set.logoES;
                string fullFileName = imagesDir + VirtualPath;
                //string fullFileName =
                //    context.HttpContext.Server.MapPath("~/imagens/" + VirtualPath);
                using (System.Drawing.Image photoImg = System.Drawing.Image.FromFile(fullFileName))
                {
                    using (System.Drawing.Image thumbPhoto = photoImg.GetThumbnailImage(200, 200, null, new System.IntPtr()))
                    {
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                        {
                            thumbPhoto.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            context.HttpContext.Response.BinaryWrite(ms.ToArray());
                            context.HttpContext.Response.End();
                        }
                    }
                }
            }
  
    }
}