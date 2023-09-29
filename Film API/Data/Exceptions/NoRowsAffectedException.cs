namespace Film_API.Data.Exceptions
{
    public class NoRowsAffectedException : Exception
    {
        public NoRowsAffectedException(string type, int id)
            : base($"Update operation for {type} with Id: {id} had no effect.")
        {
        }
    }

}
