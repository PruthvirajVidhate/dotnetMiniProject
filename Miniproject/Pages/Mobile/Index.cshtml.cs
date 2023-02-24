using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace Miniproject.Pages.Mobile
{
    public class IndexModel : PageModel
    {
       public List<MobileInfo> mobileInfos= new List<MobileInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=MobileDb;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                { 
                    connection.Open();
                    String sql = "select * from MobileCompany";

                    using(SqlCommand command = new SqlCommand(sql,connection)) { 
                      
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                MobileInfo mobileInfo = new MobileInfo();
                               // mobileInfo.id =" "+ reader.GetInt32(0);
                                mobileInfo.brand = reader.GetString(1);
                                mobileInfo.ram = reader.GetString(2);
                                mobileInfo.storage= reader.GetString(3);
                                mobileInfo.price = reader.GetString(4);

                                mobileInfos.Add(mobileInfo);

                            }
                        }
                    
                    }
                }

            }   
            catch(Exception ex)
            
            {
                Console.WriteLine(ex.ToString());
            }     
            
        }
    }


    public class MobileInfo
    {
      
        public string id;
       
        public string brand;
        
        public string ram;
      
        public string  storage;
       
        public string  price;


    }
}
