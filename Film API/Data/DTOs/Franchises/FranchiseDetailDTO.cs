namespace Film_API.Data.DTOs.Franchises
{
    public class FranchiseDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int[] Movies { get; set; }
    }
}
