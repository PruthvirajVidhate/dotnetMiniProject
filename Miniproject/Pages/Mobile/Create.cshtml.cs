using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Miniproject.Pages.Mobile
{
    public class CreateModel : PageModel
    {
        public MobileInfo mobileinfo = new MobileInfo();
       public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
           
            mobileinfoC.brand = Request.Form["brand"];
            mobileinfoC.ram = Request.Form["ram"];
            mobileinfoC.storage = Request.Form["storage"];
            mobileinfoC.price = Request.Form["price"];


            if ( mobileinfoC.brand.Length == 0 ||
                mobileinfoC.ram.Length == 0  || mobileinfoC.storage.Length == 0 ||
               mobileinfoC.price.Length == 0 )
                

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
                    String sql = "insert into MobileCompany (Brand,Ram,Storage,Price) values(@brand,@ram,@storage,@price)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                      //  command.Parameters.AddWithValue("@id", mobileinfoC.id);
                        command.Parameters.AddWithValue("@brand", mobileinfoC.brand);
                        command.Parameters.AddWithValue("@ram", mobileinfoC.ram);
                        command.Parameters.AddWithValue("@storage", mobileinfoC.storage);
                        command.Parameters.AddWithValue("@price", mobileinfoC.price);

                        command.ExecuteNonQuery();

                    }
                }

            }
            catch ( Exception ex )
            {
                errorMessage = ex.Message;
                return;
            }

            successMessage = "New Mobile Added Correctly";
            Response.Redirect("/Mobile/Index");  
        }
    }
}
