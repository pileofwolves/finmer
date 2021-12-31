using System.Reflection;

[assembly: AssemblyVersion("0.6.0.0")]
[assembly: AssemblyFileVersion("0.6.0.0")]

[assembly: AssemblyCompany("Nuntis the Wolf")]
[assembly: AssemblyProduct("Finmer - Text Adventure")]
[assembly: AssemblyCopyright("(C) 2019-2022. Please don't repost without permission.")]

namespace Finmer
{

    /// <summary>
    /// Contains build versioning info as compile-time constants.
    /// </summary>
    internal static class CompileConstants
    {

        public const int k_VersionMajor = 0;
        public const int k_VersionMinor = 6;
        public const int k_VersionRevision = 0;
        public const string k_VersionString = "0.6-dev";

    }

}
