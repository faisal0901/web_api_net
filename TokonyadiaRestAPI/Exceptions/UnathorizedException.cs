namespace TokonyadiaRestAPI.Exception;

public class UnathorizedException:System.Exception
{
   public UnathorizedException()
   {
      
   }
   public UnathorizedException(string? message):base(message)
   {
      
   }
}