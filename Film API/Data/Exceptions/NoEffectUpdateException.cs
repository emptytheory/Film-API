namespace Film_API.Data.Exceptions
{
    public class NoEffectUpdateException : Exception
    {
        public NoEffectUpdateException(string type, int id)
            : base($"Update operation for {type} with Id: {id} had no effect.")
        {
        }
    }

}
