using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryWMIProvider.Helpers
{
    class RegistryGeneric
    {

        /// <summary>
        /// Read the value of a key from registry
        /// </summary>
        /// <typeparam name="T">type of value to convert to</typeparam>
        /// <param name="hive">Starting hive</param>
        /// <param name="key">Key to search for</param>
        /// <param name="value">value to retrieve</param>
        /// <param name="kind">Kind of value as registry type</param>
        /// <param name="data>"target type variable</param>
        /// <returns>true if value exists and can be read</returns>
        public static bool GetRegistryValue<T>(RegistryHive hive, string key, string value, RegistryValueKind kind, out T data,Action<RegistryHive,string,string,bool,Exception> callbackError=null)
        {
            bool success = false;
            data = default;

            try
            {
                using (RegistryKey baseKey = RegistryKey.OpenRemoteBaseKey(hive, String.Empty))
                {
                    if (baseKey != null)
                    {
                        using (RegistryKey registryKey = baseKey.OpenSubKey(key, RegistryKeyPermissionCheck.ReadSubTree))
                        {
                            if (null != registryKey)
                            {

                                RegistryValueKind CurrentKind = registryKey.GetValueKind(value);
                                if (CurrentKind == kind) // is the type OK?
                                {
                                    object regValue = registryKey.GetValue(value, null);
                                    if (regValue != null)
                                    {
                                        data = (T)Convert.ChangeType(regValue, typeof(T), CultureInfo.InvariantCulture);
                                        success = true;
                                    }
                                }
                            }
                        }
                    }
                }
            } catch (Exception e)
            {
                callbackError?.Invoke(hive, key, value, success, e);
            }
            
            return success;
        }

    }
}
