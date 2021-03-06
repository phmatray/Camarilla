﻿namespace Camarilla.RestApi.Controllers.ControllerModels
{
    public class CreatePersonaBindingModel
    {
        public string Pseudo { get; set; }
        public string Name { get; set; }
        public string Background { get; set; }
        //public int Generation { get; set; }
        //public int ExperienceActual { get; set; }
        //public int ExperienceRemaining { get; set; }
        //public int Nights { get; set; }
        //public int Willingness { get; set; }
        //public int Humanity { get; set; }
        public int ClanId { get; set; }
        public int RaceId { get; set; }
        public string Username { get; set; }
    }

    public class UpdatePersonaBindingModel
    {
        public string Pseudo { get; set; }
        public string Name { get; set; }
        public string Background { get; set; }
        public int? Generation { get; set; }
        public int? ExperienceActual { get; set; }
        public int? ExperienceRemaining { get; set; }
        public int? Nights { get; set; }
        public int? Willingness { get; set; }
        public int? Humanity { get; set; }
    }
}