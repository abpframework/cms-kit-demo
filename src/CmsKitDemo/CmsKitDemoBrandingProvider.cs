using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace CmsKitDemo;

[Dependency(ReplaceServices = true)]
public class CmsKitDemoBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "CMS Kit Demo";
}
