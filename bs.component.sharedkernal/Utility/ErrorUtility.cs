using System;
using System.Text;

namespace bs.component.sharedkernal.Utility
{
    public static class ErrorUtility
    {
        public static string BuildExceptionDetail(Exception ex)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Message: " + ex.Message);
            sb.AppendLine("Source: " + ex.Source);
            sb.AppendLine("TargetSite: " + ex.TargetSite);
            sb.AppendLine("StackTrace: " + ex.StackTrace);
            
            if (ex.InnerException != null)
            {
                sb.AppendLine("InnerException: ");
                BuildExceptionDetail(ex.InnerException);
            }
            
            return sb.ToString();
        }
    }
}
