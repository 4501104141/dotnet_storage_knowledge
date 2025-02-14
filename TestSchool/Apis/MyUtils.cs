namespace TestSchool.Apis;

public class MyUtils
{
    public static string getPathModuleJson(string test)
    {
        string folder_app = ".";
        string path_datas = string.Format("{0}{1}JsonModel", folder_app, Path.DirectorySeparatorChar);
        string filename = string.Format("{0}.dat", test);
        string filepath = string.Format("{0}/{1}", path_datas, filename);
        return filepath;
    }
}
