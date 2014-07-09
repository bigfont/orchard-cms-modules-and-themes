using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

public class Bootstrap
{ 
    // note: html css classes are case sensitive
    public static string Thumbnail = "thumbnail";
    public static string Clearfix = "clearfix";
    public static string ImgResponsive = "img-responsive";
    public static string ImgThumbnail = "img-thumbnail";
    public static string Row = "row";

    public static string Column(int xs, int sm, int md, int lg)
    {
        return string.Format("col-xs-{0} col-sm{1} col-md-{2} col-lg-{3}", xs, sm, md, lg);    
    }
}
