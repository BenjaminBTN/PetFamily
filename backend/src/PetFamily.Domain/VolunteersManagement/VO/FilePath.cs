﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;
using System.Linq;

namespace PetFamily.Domain.VolunteersManagement.VO
{
    public class FilePath
    {
        public string Value { get; }


        private FilePath(string path)
        {
            Value = path;
        }


        public static Result<FilePath, Error> Create(Guid name, string ext)
        {
            // validation

            var fullName = name + ext;

            return new FilePath(fullName);
        }
        
        public static Result<FilePath, Error> Create(string fullName)
        {
            // validation

            return new FilePath(fullName);
        }
    }
}
