using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestSoundcloudApi.HelpfullServices
{
    internal static class ModelValidator
    {
        internal static bool ModelIsValid<T>(T Model)
        {
            if (Model is null) return false;
            return Model!.GetType().GetProperties().Where(val => val == null).FirstOrDefault() == null;
        }
    }
}
