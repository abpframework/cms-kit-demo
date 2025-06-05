using System.Reflection;
using System.Runtime.InteropServices;
using Volo.Abp.Modularity;

namespace CmsKitDemo;

public static class CmsKitDemoAssemblyInfo
{
    private readonly static Dictionary<string, AssemblyInformation> AssemblyInfoCache = new();
    private static string _dotNetVersionCache;

    private static AssemblyInformation GetAssemblyInformation(Assembly assembly)
    {
        var assemblyName = assembly.FullName;
        if (assemblyName == null)
        {
            throw new AbandonedMutexException("Assembly name cannot be null!");
        }

        if (AssemblyInfoCache.TryGetValue(assemblyName, out var information))
        {
            return information;
        }

        var productVersion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()!.InformationalVersion.Split('+')[0];
        var modificationDate = File.GetLastWriteTime(assembly.Location);

        var assemblyInfo = new AssemblyInformation(modificationDate,  productVersion);
        AssemblyInfoCache[assemblyName] = assemblyInfo;

        return assemblyInfo;
    }
        
    public static string GetDotNetVersion()
    {
        if (_dotNetVersionCache != null)
        {
            return _dotNetVersionCache;
        }

        try
        {
            var frameworkDescription = RuntimeInformation.FrameworkDescription;
            var versionArray = frameworkDescription.Split(' ');
            if (versionArray.Length == 2)
            {
                var version = versionArray[1];
                
                if (version.Contains('-'))
                {
                    var versionParts = version.Split('-');
                    if(versionParts.Length == 2)
                    {
                        var releaseLabels = versionParts[1].Split('.');
                        if (releaseLabels.Length > 2)
                        {
                            _dotNetVersionCache = versionArray[0] + " " + versionParts[0] + "-" + releaseLabels[0] + "." + releaseLabels[1];
                        }
                    }
                }
            }
                
            _dotNetVersionCache ??= frameworkDescription;
        }
        catch (Exception e)
        {
            _dotNetVersionCache = "";
        }
            
        return _dotNetVersionCache;
    }

    public static AssemblyInformation GetAssemblyInformationOrNull()
    {
        try
        {
            var abpAssembly = Assembly.GetAssembly(typeof(AbpModule));
            var abpAssemblyInfo = GetAssemblyInformation(abpAssembly);
            var cmsKitDemoAssembly = Assembly.GetAssembly(typeof(CmsKitDemoAssemblyInfo));
            var cmsKitDemoAssemblyInfo = GetAssemblyInformation(cmsKitDemoAssembly);
            return new AssemblyInformation(cmsKitDemoAssemblyInfo.ModificationDate, abpAssemblyInfo.ProductVersion);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
         
    public class AssemblyInformation
    {
        public DateTime ModificationDate { get; set; }
        public string ProductVersion { get; set; }

        public AssemblyInformation(DateTime modificationDate, string productVersion)
        {
            ModificationDate = modificationDate;
            ProductVersion = productVersion;
        }
    }
}