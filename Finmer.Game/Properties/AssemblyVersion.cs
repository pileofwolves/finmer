using System.Reflection;

[assembly: AssemblyVersion("1.0.1.0")]
[assembly: AssemblyFileVersion("1.0.1.0")]

[assembly: AssemblyCompany("Nuntis the Wolf")]
[assembly: AssemblyProduct("Finmer - Text Adventure")]
[assembly: AssemblyCopyright("(C) 2019-2023. Please don't repost without permission.")]

namespace Finmer
{

    /// <summary>
    /// Contains build version info as compile-time constants.
    /// </summary>
    internal static class CompileConstants
    {

        public const int k_VersionMajor = 1;
        public const int k_VersionMinor = 0;
        public const int k_VersionRevision = 1;
        public const string k_VersionString = "1.0.1";

    }

}
