﻿namespace Film_API.Data.DTOs.Character
{
    public class CharacterDetailDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Alias { get; set; }
        public string? Gender { get; set; }
        public string? Picture { get; set; }
        public int[] Movies { get; set; }

    }
}
