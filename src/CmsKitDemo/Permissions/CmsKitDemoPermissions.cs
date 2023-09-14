namespace CmsKitDemo.Permissions
{
    public static class CmsKitDemoPermissions
    {
        public const string GroupName = "CmsKitDemo";

        public static class GalleryImage
        {
            public const string Root = GroupName + ".GalleryImage";

            public const string Management = Root + ".Management";
            public const string Create = Root + ".Create";
            public const string Update = Root + ".Update";
            public const string Delete = Root + ".Delete";
        }
    }
}
