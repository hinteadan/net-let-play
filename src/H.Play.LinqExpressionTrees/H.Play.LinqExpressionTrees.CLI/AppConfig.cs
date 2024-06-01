using H.Necessaire;
using H.Necessaire.Runtime;
using System;
using System.IO;
using System.Reflection;

namespace H.Play.LinqExpressionTrees.CLI
{
    internal static class AppConfig
    {
        const string srcFolderRelativePath = "/Src/H.Play.LinqExpressionTrees.CLI/";

        public static ImAnApiWireup WithDefaultRuntimeConfig(this ImAnApiWireup wireup)
        {
            return
                wireup
                .With(x => x.Register<RuntimeConfig>(() => new RuntimeConfig
                {
                    Values = new[] {
                        "NuSpecRootFolderPath".ConfigWith(GetCodebaseFolderPath()),
                    },
                }));
            ;
        }

        private static string ReadConnectionStringFromFile(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
                return null;

            string result = null;

            new Action(() =>
            {
                result = File.ReadAllText(fileInfo.FullName);
            })
            .TryOrFailWithGrace(onFail: ex => result = null);

            return result;
        }

        private static string GetCodebaseFolderPath()
        {
            string codeBase = Assembly.GetExecutingAssembly()?.Location ?? string.Empty;
            UriBuilder uri = new UriBuilder(codeBase);
            string dllPath = Uri.UnescapeDataString(uri.Path);
            int srcFolderIndex = dllPath.ToLowerInvariant().IndexOf(srcFolderRelativePath, StringComparison.InvariantCultureIgnoreCase);
            if (srcFolderIndex < 0)
                return string.Empty;
            string srcFolderPath = Path.GetDirectoryName(dllPath.Substring(0, srcFolderIndex + srcFolderRelativePath.Length)) ?? string.Empty;
            return srcFolderPath;
        }
    }
}
