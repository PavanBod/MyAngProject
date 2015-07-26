using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIT.Common
{
    public static class ExtensionMethods
    {
        public static bool IsNullOrEmpty(this object objectToCheck)
        {
            return
                objectToCheck == null ||
                String.IsNullOrEmpty(objectToCheck.ToString()) ||
                String.IsNullOrWhiteSpace(objectToCheck.ToString());
        }

        public static string GetStringFromByteArray(this byte[] byteArrayString)
        {
            return System.Text.Encoding.UTF8.GetString(byteArrayString);
        }
    }
}
