﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Shared.VO
{
    public class SocialNetwork
    {
        public string? Name { get; }
        public string? Url { get; }

        private SocialNetwork(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public static Result<SocialNetwork, Error> Create(string name, string url)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Errors.General.InvalidValue("Name");

            if(string.IsNullOrWhiteSpace(url))
                return Errors.General.InvalidValue("Url");

            return new SocialNetwork(name, url);
        }
    }
}