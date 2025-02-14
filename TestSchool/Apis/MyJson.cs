using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using Serilog;
using TestSchool.Model;

namespace TestSchool.Apis;

public class MyJson
{
    public class ItemModelTest
    {
        public string code { get; set; } = "";
    }

    public class ItemRespond
    {
        public bool listCheck { get; set; } = false;
        public bool objectCheck { get; set; } = false;
    }

    public ItemRespond Check()
    {
        try
        {

            using (DataContext context = new DataContext())
            {
                string dataJsonObject = File.ReadAllText(MyUtils.getPathModuleJson("JsonObject"));
                string dataJsonList = File.ReadAllText(MyUtils.getPathModuleJson("JsonList"));
                bool listCheck = false;
                bool objectCheck = false;
                //* Không thể deserialize dạng object sang dạng mảng
                //* Khi file JsonList.dat bị rỗng thì sẽ trả về null
                //* Khi file JsonList.data có data nhưng key trong json không ánh xạ đúng với property trong model thì có data nhưng thuộc tính của model sẽ được đặt theo mặc định.
                List<ItemModelTest>? listTest = JsonConvert.DeserializeObject<List<ItemModelTest>>(dataJsonList);
                if (listTest == null)
                {
                    listCheck = false;
                }
                else
                {
                    listCheck = true;
                }
                //* Không thể deserialize dạng mảng sang dạng object
                //* Khi file JsonObject.dat bị rỗng thì sẽ trả về null
                //* Khi file JsonObject.data có data nhưng key trong json không ánh xạ đúng với property trong model thì có data nhưng thuộc tính của model sẽ được đặt theo mặc định.
                ItemModelTest? objectTest = JsonConvert.DeserializeObject<ItemModelTest>(dataJsonObject);
                if (objectTest == null)
                {
                    objectCheck = false;
                }
                else
                {
                    objectCheck = true;
                }
                ItemRespond respond = new ItemRespond();
                respond.listCheck = listCheck;
                respond.objectCheck = objectCheck;
                return respond;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return new ItemRespond();
        }
    }
}
