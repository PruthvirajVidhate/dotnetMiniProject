using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Miniproject.Pages.Mobile
{
    public class EditModel : PageModel
    {
        public MobileInfo mobileinfoE = new MobileInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=MobileDb;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select * from MobileCompany where id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id",id);  
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                // MobileInfo mobileInfo = new MobileInfo();
                                mobileinfoE.id = " " + reader.GetInt32(0); ;
                                mobileinfoE.brand = reader.GetString(1);
                                mobileinfoE.ram = reader.GetString(2);
                                mobileinfoE.storage = reader.GetString(3);
                                mobileinfoE.price = reader.GetString(4);

                               // mobileInfos.Add(mobileInfo);

                            }
                        }

                    }
                }

            }

            catch (Exception ex) {
              errorMessage= ex.Message; 
            }
        }

        public void OnPost() {
            mobileinfoE.id = Request.Form["id"];
            mobileinfoE.brand = Request.Form["brand"];
            mobileinfoE.ram = Request.Form["ram"];
            mobileinfoE.storage = Request.Form["storage"];
            mobileinfoE.price = Request.Form["price"];

            if (mobileinfoE.brand.Length == 0 ||
                mobileinfoE.ram.Length == 0 || mobileinfoE.storage.Length == 0 ||
               mobileinfoE.price.Length == 0)


            {
                errorMessage = "All Field Are Required";
                return;
            }


            try
            {
                String connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=MobileDb;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "update MobileCompany set Brand=@brand,Ram=@ram,Storage=@storage,Price=@price where id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                         command.Parameters.AddWithValue("@id", mobileinfoE.id);
                        command.Parameters.AddWithValue("@brand", mobileinfoE.brand);
                        command.Parameters.AddWithValue("@ram", mobileinfoE.ram);
                        command.Parameters.AddWithValue("@storage", mobileinfoE.storage);
                        command.Parameters.AddWithValue("@price", mobileinfoE.price);

                        command.ExecuteNonQuery();

                    }
                }

            }

            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }

           // successMessage = "New Mobile Added Correctly";
            Response.Redirect("/Mobile/Index");
        }
    }
}
