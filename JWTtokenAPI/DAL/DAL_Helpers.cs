namespace JWTtokenAPI.DAL
{
    public class DAL_Helpers { 
        public static String ConString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyConnectionString");
    
    }

    
}
