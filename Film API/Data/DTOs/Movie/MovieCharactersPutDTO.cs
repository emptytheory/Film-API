namespace Film_API.Data.DTOs.Movie
{
    // This class is not exposed to the user but is used to offload the mapping from ids to characters to AutoMapper.
    internal class MovieCharactersPutDTO
    {
        public int[] Characters;
    }
}
