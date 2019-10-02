using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters;

namespace ModelAnalyzer
{
    class FilesManager
    {
        public string fileFilter = "Model file|*.mdl";

        readonly string keyValueSeparator = ":";
        readonly string parametersSeparator = "|\r\n";
        readonly string defaultModelFile = "default.mdl";
        UTF8Encoding encoding = new UTF8Encoding();

        public void WriteValuesToFile (Storage storage, string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            List<Parameter> parameters = storage.Parameters();

            FileStream stream = File.Create(filePath);

            foreach (Parameter parameter in parameters)
            {
                string key = parameter.GetType().ToString();
                stream.Write(encoding.GetBytes(key), 0, key.Length);
                stream.Write(encoding.GetBytes(keyValueSeparator), 0, keyValueSeparator.Length);
                string value = parameter.StringRepresentation();
                stream.Write(encoding.GetBytes(value), 0, value.Length);
                stream.Write(encoding.GetBytes(parametersSeparator), 0, parametersSeparator.Length);
            }

            stream.Close();
        }

        public void ReadValuesFromFile(Storage storage, string filePath)
        {
            string data = File.ReadAllText(filePath);
            string[] paramStrings = data.Split(parametersSeparator.ToCharArray());
            foreach (string paramString in paramStrings)
            {
                if (paramString.Length == 0)
                    continue;

                string[] keyValue = paramString.Split(keyValueSeparator.ToCharArray());
                string keyString = keyValue.First();
                string valueString = keyValue.Last();
                if (Type.GetType(keyString) is Type key)
                {
                    if (storage.HasParameterOfType(key))
                    {
                        Parameter parameter = storage.Parameter(key);
                        parameter.SetupByString(valueString);
                    }
                }
            }
        }

        public void ReadValuesFromDefault(Storage storage)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(new string[] { dir, defaultModelFile });

            if (File.Exists(filePath))
                ReadValuesFromFile(storage, filePath);
        }

        public void WriteValuesToDefault(Storage storage)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(new string[] { dir, defaultModelFile });

            WriteValuesToFile(storage, filePath);
        }
    }
}
